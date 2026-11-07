#nullable enable
using FM.Pitchside.Core.VirtualMemory.Rtti;
using System.Diagnostics;

namespace FM.Pitchside.Core.Defines.Versions
{
    /// <summary>One human-controlled manager in the loaded save, with the club they manage.</summary>
    public sealed class HumanManagerInfo
    {
        /// <summary>The CLUB_HUMAN_MANAGER_DETAILS object (links the human to a team).</summary>
        public long DetailsAddress { get; set; }

        public long TeamAddress { get; set; }
        public long ClubAddress { get; set; }
        public string ClubName { get; set; } = "";

        /// <summary>The manager's person record (db::HUMAN_NON_PLAYER), or 0 if it could not be matched.</summary>
        public long RecordAddress { get; set; }

        public string FirstName { get; set; } = "";
        public string SecondName { get; set; } = "";
        public string CommonName { get; set; } = "";
        public string FullName { get; set; } = "";

        public string DisplayName =>
            CommonName.Length > 0 ? CommonName :
            (FirstName + " " + SecondName).Trim().Length > 0 ? (FirstName + " " + SecondName).Trim() :
            FullName;

        public override string ToString() => $"{DisplayName} - {ClubName}";
    }

    /// <summary>One player read from the FM27 save.</summary>
    public sealed class Fm27PlayerInfo
    {
        /// <summary>Address of the player's db::ACTUAL_PLAYER sub-object. This is the base the legacy Player entity uses.</summary>
        public long Address { get; set; }

        /// <summary>The game's person unique id.</summary>
        public uint Uid { get; set; }

        public string FullName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string SecondName { get; set; } = "";
        public string CommonName { get; set; } = "";
        public string Nation { get; set; } = "";

        public int BirthYear { get; set; }

        /// <summary>1-based day of the year, as stored by the game.</summary>
        public int BirthDayOfYear { get; set; }

        /// <summary>Exact current ability (1-200).</summary>
        public int CurrentAbility { get; set; }

        /// <summary>Exact potential ability, decoded from the raw field; see <see cref="PotentialAbilityRaw"/>.</summary>
        public int PotentialAbility { get; set; }

        /// <summary>The raw 16-bit field. Negative values are the game's encoded form of an unrevealed potential.</summary>
        public short PotentialAbilityRaw { get; set; }

        public int HeightCm { get; set; }

        /// <summary>
        /// Position proficiency, 0-20 each, in <see cref="Fm27Names.Positions"/> order
        /// (GK SW DL DC DR DM ML MC MR AML AMC AMR ST WBL WBR). 20 is natural.
        /// </summary>
        public byte[] PositionRatings { get; set; } = new byte[Fm27Names.Positions.Length];

        /// <summary>
        /// The player's top roles as stored by the game: entries 0-2 are in-possession role ids and
        /// 3-5 are out-of-possession role ids. The 3+3 split is verified (each half indexes its own name
        /// table coherently across ~124k players). That each trio is ordered best-first is NOT verified: it is
        /// how FM Moleman treats the field, and the ids within a trio are clearly ordered by something, but
        /// it hasn't been compared with the game's own role ratings. An id of 64 or more (or one with no
        /// name) means "none". These are ids only; the game's per-role star ratings are not stored.
        /// </summary>
        public byte[] TopRoleIds { get; set; } = new byte[6];

        /// <summary>The player's contract record (0 if none, e.g. a free agent).</summary>
        public long ContractAddress { get; set; }

        /// <summary>The club that holds the contract (the owner), full name; empty if the player has no club.</summary>
        public string ClubName { get; set; } = "";

        public string ClubShortName { get; set; } = "";

        /// <summary>Address of the club object that holds the contract (0 if none).</summary>
        public long ClubAddress { get; set; }

        /// <summary>Address of the team the player is currently registered with (0 if none / not a recognised team).</summary>
        public long CurrentTeamAddress { get; set; }

        /// <summary>The current team's squad type (0 first team, 1 reserves, 10 U21, 12 U18, ...), or -1 if unknown.</summary>
        public int SquadType { get; set; } = -1;

        /// <summary>The current team's squad name ("First team", "U21", ...); empty if unknown.</summary>
        public string Squad => SquadType < 0 ? "" : Fm27Names.SquadName(SquadType);

        /// <summary>Address of the club object the current team belongs to (0 if unknown).</summary>
        public long CurrentClubAddress { get; set; }

        /// <summary>Name of the club the current team belongs to; empty if unknown. Equals <see cref="ClubName"/> unless on loan.</summary>
        public string CurrentClubName { get; set; } = "";

        /// <summary>
        /// Fact: the team the player is registered with belongs to a different club than the one holding the
        /// contract. True for loans AND for B/affiliate teams (Barcelona players at Barcelona Atlètic), because the
        /// game records both the same way - the contract names the parent club either way.
        /// </summary>
        public bool PlaysForOtherClub { get; set; }

        /// <summary>
        /// Heuristic: <see cref="PlaysForOtherClub"/> and the destination is not an affiliate of the owner. A club
        /// counts as the owner's affiliate when it has at least <see cref="Fm27Game.AffiliateMinPlayers"/> registered
        /// players and at least half of them belong to that one owner. Loans from several clubs into one club, and
        /// one-off loans into a normal squad, are not mistaken for affiliates.
        /// </summary>
        public bool IsOnLoan { get; set; }

        /// <summary>True for a registration with an affiliate (B / reserve club) of the owner; see <see cref="IsOnLoan"/>.</summary>
        public bool IsWithAffiliateClub { get; set; }

        /// <summary>The club the player is currently playing for on loan; empty when not on loan.</summary>
        public string LoanClubName { get; set; } = "";

        /// <summary>The game treats a player as a goalkeeper when GK proficiency is 10 or more.</summary>
        public bool IsGoalkeeper => PositionRatings[0] >= 10;

        public int BestPositionRating => PositionRatings.Max();

        public int GetPositionRating(string position)
        {
            int index = Array.IndexOf(Fm27Names.Positions, position.ToUpperInvariant());
            if (index < 0)
            {
                throw new ArgumentException("Unknown position '" + position + "'. Expected one of: " + string.Join(", ", Fm27Names.Positions), nameof(position));
            }

            return PositionRatings[index];
        }

        /// <summary>Positions rated at least <paramref name="minimum"/>, best first.</summary>
        public IReadOnlyList<string> PositionsAtLeast(int minimum)
        {
            return Enumerable.Range(0, PositionRatings.Length)
                .Where(i => PositionRatings[i] >= minimum)
                .OrderByDescending(i => PositionRatings[i])
                .ThenBy(i => i)
                .Select(i => Fm27Names.Positions[i])
                .ToList();
        }

        /// <summary>The positions this player is best at (within 2 of their top rating, and at least 15).</summary>
        public IReadOnlyList<string> BestPositions
        {
            get
            {
                int best = BestPositionRating;
                return PositionsAtLeast(Math.Max(15, best - 2));
            }
        }

        /// <summary>
        /// The first of the stored in-possession trio, or null if the game stored none. Believed to be the
        /// player's best role with the ball, but that ordering is unverified - see <see cref="TopRoleIds"/>.
        /// This is the role itself, not a star rating.
        /// </summary>
        public string? BestInPossessionRole => RoleName(Fm27Names.RolesInPossession, TopRoleIds[0]);

        /// <summary>The first of the stored out-of-possession trio, or null. Believed to be the best role without the ball; ordering unverified.</summary>
        public string? BestOutOfPossessionRole => RoleName(Fm27Names.RolesOutOfPossession, TopRoleIds[3]);

        private static string? RoleName(string?[] table, byte id)
        {
            return id < table.Length ? table[id] : null;
        }

        /// <summary>Names of the stored top in-possession roles, in stored order (believed best first; unverified).</summary>
        public IReadOnlyList<string> InPossessionRoles => RoleNames(Fm27Names.RolesInPossession, 0);

        /// <summary>Names of the stored top out-of-possession roles, in stored order (believed best first; unverified).</summary>
        public IReadOnlyList<string> OutOfPossessionRoles => RoleNames(Fm27Names.RolesOutOfPossession, 3);

        private IReadOnlyList<string> RoleNames(string?[] table, int first)
        {
            var names = new List<string>(3);
            for (int i = first; i < first + 3; i++)
            {
                byte id = TopRoleIds[i];
                if (id < table.Length && table[id] != null)
                {
                    names.Add(table[id]!);
                }
            }

            return names;
        }

        public string DisplayName =>
            CommonName.Length > 0 ? CommonName :
            (FirstName + " " + SecondName).Trim().Length > 0 ? (FirstName + " " + SecondName).Trim() :
            FullName;

        /// <summary>Birth date from the stored year + day-of-year, or null if the pair isn't a valid date.</summary>
        public DateTime? BirthDate
        {
            get
            {
                if (BirthYear < 1 || BirthYear > 9999 || BirthDayOfYear < 1)
                {
                    return null;
                }

                int daysInYear = DateTime.IsLeapYear(BirthYear) ? 366 : 365;
                return BirthDayOfYear > daysInYear ? (DateTime?)null : new DateTime(BirthYear, 1, 1).AddDays(BirthDayOfYear - 1);
            }
        }

        public override string ToString() => $"{DisplayName} (uid {Uid}, CA {CurrentAbility}, PA {PotentialAbility})";
    }

    /// <summary>One non-player person read from the FM27 save: a manager (human or AI) or backroom staff member.</summary>
    public sealed class Fm27StaffInfo
    {
        /// <summary>Address of the person's db::ACTUAL_NON_PLAYER "id" sub-object.</summary>
        public long Address { get; set; }

        public string FullName { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string SecondName { get; set; } = "";
        public string CommonName { get; set; } = "";

        public string DisplayName =>
            CommonName.Length > 0 ? CommonName :
            (FirstName + " " + SecondName).Trim().Length > 0 ? (FirstName + " " + SecondName).Trim() :
            FullName;

        /// <summary>The club that holds the contract (0 if none, e.g. unemployed).</summary>
        public long ClubAddress { get; set; }

        public string ClubName { get; set; } = "";

        public override string ToString() => $"{DisplayName} - {ClubName}";
    }

    /// <summary>A team's registered players: one club's first team, reserves, U21 side and so on.</summary>
    public sealed class Fm27SquadInfo
    {
        public long TeamAddress { get; set; }
        public long ClubAddress { get; set; }
        public string ClubName { get; set; } = "";
        public int SquadType { get; set; }
        public string Squad => Fm27Names.SquadName(SquadType);
        public List<Fm27PlayerInfo> Players { get; set; } = new List<Fm27PlayerInfo>();

        public override string ToString() => $"{ClubName} {Squad} ({Players.Count} players)";
    }

    /// <summary>
    /// FM27 (Unity, native game_plugin.dll) located entirely through MSVC RTTI instead of a static
    /// root table. Class names are resolved to vtable RVAs in the live module; objects are found by
    /// scanning the heap for those vtable pointers. Nothing here is an absolute address or a
    /// per-build RVA, so it keeps working across FM27 patches as long as the classes keep their
    /// names and the field layouts below stay put.
    /// </summary>
    public sealed class Fm27Game
    {
        /// <summary>FM27 ships on the Unity 6000.3 line (FM26 was 6000.0), which is how the two are told apart.</summary>
        public const string SupportedUnityLine = "6000.3.";

        public const string PluginModuleName = "game_plugin.dll";

        /// <summary>
        /// Field layout, in bytes. Verified live against FM27 (Unity 6000.3.18f1) with a Manchester
        /// United save. The manager/club chain was read by hand from the running game; the player
        /// block offsets follow FM Moleman's FM26/FM27 layout (same block, same offsets) and were
        /// checked against real players (see Fm27Game.FindPlayers).
        /// </summary>
        public static class Layout
        {
            // CLUB_HUMAN_MANAGER_DETAILS (object base)
            public const long DetailsTeam = 0x40;

            // TEAM / CLUB / strings
            public const long TeamClub = 0x30;
            public const long ClubName = 0xC0;

            // db::HUMAN_NON_PLAYER (object base = vtable hit - lowest sub-object offset)
            public const long RecordUid = 0x450;
            public const long RecordContract = 0x4E8;
            public const long RecordFullName = 0x480;
            public const long RecordFirstName = 0x490;
            public const long RecordSecondName = 0x498;
            public const long RecordCommonName = 0x4A0;

            // Contract
            public const long ContractTeam = 0x10;

            // db::ACTUAL_NON_PLAYER - the real master staff/manager table (every backroom person and
            // manager in the world, live for all ~54k of them at once - unlike HUMAN_NON_PLAYER above,
            // which only ever covers the record(s) linked to a human account). A non-player is two
            // vtable-bearing sub-objects in one allocation, mirroring the player block:
            //   db::NON_PLAYING_DATA   (sub-object +0)      at block start
            //   db::ACTUAL_NON_PLAYER  (sub-object +256)    at block + NonPlayerIdFromBlock  <- "id" vtable
            //   db::ACTUAL_NON_PLAYER  (sub-object +296)    at block + NonPlayerPersonFromBlock
            // Unlike players, FromBlock equals the raw sub-object number here - no extra base offset.
            // Verified live against a real record ("René Søgaard Andersen") found via the id vtable scan.
            public const int NonPlayerIdSubObject = 256;
            public const int NonPlayerPersonSubObject = 296;
            public const int NonPlayerIdFromBlock = 256;
            public const int NonPlayerPersonFromBlock = 296;

            public const int NonPlayerBlockLength = 0x240;   // 576 - confirmed: the next record starts here.
            public const int NonPlayerFullName = 0x140;      // verified: decoded a real UTF-8 name.
            public const int NonPlayerFirstName = 0x150;     // name-entry container, by analogy with Player/HUMAN_NON_PLAYER.
            public const int NonPlayerSecondName = 0x158;    // name-entry container, by analogy.
            public const int NonPlayerCommonName = 0x160;    // by analogy; null in every sample seen so far.
            public const int NonPlayerNation = 0x168;        // verified: resolves to a db::NATION object.
            public const int NonPlayerContract = 0x1A8;      // verified: resolves to a db::FULL_CONTRACT object.

            // NOT found despite a real search: Job/role (manager, coach, physio, scout, ...). db::JOB has
            // no live instances of its own (see Fm27Game.FindStaff doc comment), so it must be a plain
            // integer somewhere in this block or the contract - none of the unlabeled fields checked so
            // far (in either place) could be confirmed against a known real job without a second named
            // reference point to compare against.

            // Players. A player is three vtable-bearing sub-objects laid out in one allocation:
            //   db::PLAYING_DATA   (sub-object +0)      at block start
            //   db::ACTUAL_PLAYER  (sub-object +648)    at block + PlayerIdFromBlock   <- the "id" vtable
            //   db::ACTUAL_PLAYER  (sub-object +688)    at block + PlayerPersonFromBlock
            public const int PlayerIdSubObject = 648;
            public const int PlayerPersonSubObject = 688;
            public const int PlayerIdFromBlock = 320;
            public const int PlayerPersonFromBlock = 360;

            // Offsets inside the player block (block start = the PLAYING_DATA vtable).
            public const int PlayerBlockLength = 520;
            public const int PlayerPositions = 8;          // 15 bytes, one proficiency (0-20) per position
            public const int PlayerTopRoles = 305;         // 6 bytes: 3 in-possession then 3 out-of-possession role ids
            public const int PlayerHeight = 230;
            public const int PlayerCurrentAbility = 284;
            public const int PlayerPotentialAbility = 286;
            public const int PlayerUid = 332;
            public const int PlayerFullName = 384;
            public const int PlayerFirstName = 400;
            public const int PlayerSecondName = 408;
            public const int PlayerCommonName = 416;
            public const int PlayerNation = 424;
            public const int PlayerBirthDay = 456;
            public const int PlayerBirthYear = 458;

            // Nation record
            public const long NationName = 24;

            // Club membership. Relative to the player block unless noted.
            public const long PlayerCurrentTeam = -24;     // pointer to the TEAM the player is registered with now
            public const long PlayerContract = 488;        // pointer to the contract record
            public const long ContractTeamPrimary = 24;    // contract: team pointer preferred for the owner club
            public const long ContractTeamFallback = 16;   // contract: used when the primary resolves to no club
            public const long TeamType = 40;               // byte: squad type (first team, reserves, U21, ...)
            public const long ClubShortName = 0xC8;        // club: short name string pointer
        }

        private sealed class HeapScan
        {
            public readonly List<long> Details = new List<long>();
            public readonly List<long> Records = new List<long>();
            public readonly List<long> Players = new List<long>();
            public readonly List<long> NonPlayers = new List<long>();
        }

        private HeapScan? scan;

        private Fm27Game(long moduleBase, long moduleSize, long detailsVtableRva, long humanNonPlayerVtableRva,
            int humanNonPlayerSubObjectOffset, long playerAttrsVtableRva, long playerIdVtableRva, long playerPersonVtableRva,
            long nonPlayerAttrsVtableRva, long nonPlayerIdVtableRva, long nonPlayerPersonVtableRva,
            long teamVtableRva)
        {
            TeamVtableRva = teamVtableRva;
            ModuleBase = moduleBase;
            ModuleSize = moduleSize;
            HumanManagerDetailsVtableRva = detailsVtableRva;
            HumanNonPlayerVtableRva = humanNonPlayerVtableRva;
            HumanNonPlayerSubObjectOffset = humanNonPlayerSubObjectOffset;
            PlayerAttrsVtableRva = playerAttrsVtableRva;
            PlayerIdVtableRva = playerIdVtableRva;
            PlayerPersonVtableRva = playerPersonVtableRva;
            NonPlayerAttrsVtableRva = nonPlayerAttrsVtableRva;
            NonPlayerIdVtableRva = nonPlayerIdVtableRva;
            NonPlayerPersonVtableRva = nonPlayerPersonVtableRva;
        }

        public long ModuleBase { get; }
        public long ModuleSize { get; }

        /// <summary>RVA of the CLUB_HUMAN_MANAGER_DETAILS vtable in the running build.</summary>
        public long HumanManagerDetailsVtableRva { get; }

        /// <summary>RVA of the db::HUMAN_NON_PLAYER vtable with the lowest sub-object offset.</summary>
        public long HumanNonPlayerVtableRva { get; }

        /// <summary>Where that vtable sits inside a HUMAN_NON_PLAYER object (object base = hit - this).</summary>
        public int HumanNonPlayerSubObjectOffset { get; }

        /// <summary>RVAs of the three player vtables, or 0 when the running build doesn't expose them.</summary>
        public long PlayerAttrsVtableRva { get; }

        public long PlayerIdVtableRva { get; }
        public long PlayerPersonVtableRva { get; }

        /// <summary>RVAs of the three db::ACTUAL_NON_PLAYER/NON_PLAYING_DATA vtables (the staff/manager
        /// master table), or 0 when the running build doesn't expose them.</summary>
        public long NonPlayerAttrsVtableRva { get; }

        public long NonPlayerIdVtableRva { get; }
        public long NonPlayerPersonVtableRva { get; }

        /// <summary>
        /// RVA of the db::TEAM vtable (0 if it didn't resolve). Used to tell real team objects from stray
        /// pointers; without it squads and loans are unavailable but clubs from contracts still work.
        /// </summary>
        public long TeamVtableRva { get; }

        /// <summary>True when the player classes resolved; <see cref="FindPlayers"/> needs this.</summary>
        public bool PlayersAvailable => PlayerAttrsVtableRva != 0 && PlayerIdVtableRva != 0 && PlayerPersonVtableRva != 0;

        /// <summary>True when the non-player classes resolved; <see cref="FindStaff"/> needs this.</summary>
        public bool NonPlayersAvailable => NonPlayerAttrsVtableRva != 0 && NonPlayerIdVtableRva != 0 && NonPlayerPersonVtableRva != 0;

        /// <summary>
        /// Resolves the classes this model needs from the live game_plugin.dll. Needs only the game
        /// to have started (RTTI is static); a loaded save is not required.
        /// </summary>
        public static bool TryCreate(FMProcess process, out Fm27Game? game, out string? error)
        {
            game = null;
            error = null;

            string unityVersion = process.VersionDescription ?? "";
            if (!unityVersion.StartsWith(SupportedUnityLine, StringComparison.Ordinal))
            {
                error = $"Engine version '{unityVersion}' is not on the Unity {SupportedUnityLine}x line used by FM27.";
                return false;
            }

            long moduleBase;
            long moduleSize;
            try
            {
                ProcessModule? plugin = null;
                foreach (ProcessModule module in process.Process.Modules)
                {
                    if (string.Equals(module.ModuleName, PluginModuleName, StringComparison.OrdinalIgnoreCase))
                    {
                        plugin = module;
                        break;
                    }
                }

                if (plugin == null)
                {
                    error = PluginModuleName + " is not loaded yet.";
                    return false;
                }

                moduleBase = plugin.BaseAddress.ToInt64();
                moduleSize = plugin.ModuleMemorySize;
            }
            catch (Exception ex)
            {
                error = "Could not enumerate the game's modules: " + ex.Message;
                return false;
            }

            RttiImage image = RttiImage.FromLiveModule(moduleBase, moduleSize);

            var details = image.FindVtables("CLUB_HUMAN_MANAGER_DETAILS");
            if (details.Count == 0)
            {
                error = "RTTI class CLUB_HUMAN_MANAGER_DETAILS was not found in " + PluginModuleName + ".";
                return false;
            }

            var records = image.FindVtables("HUMAN_NON_PLAYER@db");
            if (records.Count == 0)
            {
                error = "RTTI class db::HUMAN_NON_PLAYER was not found in " + PluginModuleName + ".";
                return false;
            }

            // Players are optional: if a patch renames or re-lays-out these, managers still work and
            // PlayersAvailable simply reads false. Each part must resolve to exactly one vtable.
            long attrs = SingleVtable(image, "PLAYING_DATA@db", 0);
            long id = SingleVtable(image, "ACTUAL_PLAYER@db", Layout.PlayerIdSubObject);
            long person = SingleVtable(image, "ACTUAL_PLAYER@db", Layout.PlayerPersonSubObject);
            if (attrs == 0 || id == 0 || person == 0)
            {
                attrs = id = person = 0;
            }

            // Non-players (the staff/manager master table) are optional the same way.
            long npAttrs = SingleVtable(image, "NON_PLAYING_DATA@db", 0);
            long npId = SingleVtable(image, "ACTUAL_NON_PLAYER@db", Layout.NonPlayerIdSubObject);
            long npPerson = SingleVtable(image, "ACTUAL_NON_PLAYER@db", Layout.NonPlayerPersonSubObject);
            if (npAttrs == 0 || npId == 0 || npPerson == 0)
            {
                npAttrs = npId = npPerson = 0;
            }

            // FindVtables is ordered by sub-object offset; the primary vtable is the lowest one.
            long team = SingleVtable(image, "TEAM@db", 0);

            game = new Fm27Game(moduleBase, moduleSize, details[0].VtableRva, records[0].VtableRva, records[0].SubObjectOffset,
                attrs, id, person, npAttrs, npId, npPerson, team);
            return true;
        }

        private static long SingleVtable(RttiImage image, string className, int subObjectOffset)
        {
            var matches = image.FindVtables(className).Where(v => v.SubObjectOffset == subObjectOffset).ToList();
            return matches.Count == 1 ? matches[0].VtableRva : 0;
        }

        /// <summary>
        /// Scans the whole process heap once for managers' and players' vtables and remembers the
        /// object addresses. Several seconds. <see cref="FindHumanManagers"/> and <see cref="FindPlayers"/>
        /// reuse it; call this again after loading a different save or after in-game time has passed.
        /// </summary>
        public void Refresh()
        {
            long detailsVtable = ModuleBase + HumanManagerDetailsVtableRva;
            long recordVtable = ModuleBase + HumanNonPlayerVtableRva;
            long playerIdVtable = PlayersAvailable ? ModuleBase + PlayerIdVtableRva : 0;
            long playerAttrsVtable = PlayersAvailable ? ModuleBase + PlayerAttrsVtableRva : 0;
            long playerPersonVtable = PlayersAvailable ? ModuleBase + PlayerPersonVtableRva : 0;
            long nonPlayerIdVtable = NonPlayersAvailable ? ModuleBase + NonPlayerIdVtableRva : 0;
            long nonPlayerAttrsVtable = NonPlayersAvailable ? ModuleBase + NonPlayerAttrsVtableRva : 0;
            long nonPlayerPersonVtable = NonPlayersAvailable ? ModuleBase + NonPlayerPersonVtableRva : 0;

            var targets = new List<long> { detailsVtable, recordVtable };
            if (PlayersAvailable)
            {
                targets.Add(playerIdVtable);
            }

            if (NonPlayersAvailable)
            {
                targets.Add(nonPlayerIdVtable);
            }

            var result = new HeapScan();
            ProcessHeap.ScanQwords(targets, (address, value) =>
            {
                if (value == detailsVtable)
                {
                    result.Details.Add(address);
                }
                else if (value == recordVtable)
                {
                    result.Records.Add(address - HumanNonPlayerSubObjectOffset);
                }
                else if (value == nonPlayerIdVtable)
                {
                    result.NonPlayers.Add(address);
                }
                else
                {
                    result.Players.Add(address);
                }
            });

            if (PlayersAvailable)
            {
                // Keep only real player blocks: the sibling vtables must sit at their fixed distances.
                // Stale copies and look-alike structures fail this and are dropped.
                result.Players.RemoveAll(hit =>
                    ProcessHeap.ReadPointer(hit - Layout.PlayerIdFromBlock) != playerAttrsVtable
                    || ProcessHeap.ReadPointer(hit - Layout.PlayerIdFromBlock + Layout.PlayerPersonFromBlock) != playerPersonVtable);
            }

            if (NonPlayersAvailable)
            {
                // Same sibling-vtable check as players, for the same reason. Unlike the player block,
                // NON_PLAYING_DATA's own vtable sits at block start +0x8, not +0x0 (block start +0x0 holds
                // a different, unlabelled module pointer) - verified live against a real record.
                result.NonPlayers.RemoveAll(hit =>
                    ProcessHeap.ReadPointer(hit - Layout.NonPlayerIdFromBlock + 0x8) != nonPlayerAttrsVtable
                    || ProcessHeap.ReadPointer(hit - Layout.NonPlayerIdFromBlock + Layout.NonPlayerPersonFromBlock) != nonPlayerPersonVtable);
            }

            scan = result;
        }

        private HeapScan EnsureScan()
        {
            if (scan == null)
            {
                Refresh();
            }

            return scan!;
        }

        /// <summary>
        /// Finds every human manager in the loaded save. Uses the cached heap scan (scanning first if
        /// needed); returns an empty list if no save is loaded.
        /// </summary>
        public IReadOnlyList<HumanManagerInfo> FindHumanManagers()
        {
            HeapScan heap = EnsureScan();
            var result = new List<HumanManagerInfo>();
            var seenTeams = new HashSet<long>();

            foreach (long details in heap.Details)
            {
                long team = ProcessHeap.ReadPointer(details + Layout.DetailsTeam);
                long club = team == 0 ? 0 : ProcessHeap.ReadPointer(team + Layout.TeamClub);
                string? clubName = club == 0
                    ? null
                    : ProcessHeap.ReadLengthPrefixedString(ProcessHeap.ReadPointer(club + Layout.ClubName));

                // A stale or half-initialised object won't have a readable team/club/name chain.
                if (clubName == null || !seenTeams.Add(team))
                {
                    continue;
                }

                var info = new HumanManagerInfo
                {
                    DetailsAddress = details,
                    TeamAddress = team,
                    ClubAddress = club,
                    ClubName = clubName,
                };

                // The manager's own record is the HUMAN_NON_PLAYER whose contract is with this team.
                foreach (long record in heap.Records)
                {
                    long contract = ProcessHeap.ReadPointer(record + Layout.RecordContract);
                    if (contract == 0 || ProcessHeap.ReadPointer(contract + Layout.ContractTeam) != team)
                    {
                        continue;
                    }

                    info.RecordAddress = record;
                    info.FullName = ProcessHeap.ReadLengthPrefixedString(ProcessHeap.ReadPointer(record + Layout.RecordFullName)) ?? "";
                    info.FirstName = ProcessHeap.ReadNameEntry(ProcessHeap.ReadPointer(record + Layout.RecordFirstName)) ?? "";
                    info.SecondName = ProcessHeap.ReadNameEntry(ProcessHeap.ReadPointer(record + Layout.RecordSecondName)) ?? "";
                    info.CommonName = ProcessHeap.ReadNameEntry(ProcessHeap.ReadPointer(record + Layout.RecordCommonName)) ?? "";
                    break;
                }

                result.Add(info);
            }

            return result;
        }

        /// <summary>
        /// Finds every backroom staff member and manager (human or AI) in the save, via db::ACTUAL_NON_PLAYER
        /// - the real master staff table, eagerly live for all ~54k of them at once (confirmed via a live
        /// Manchester United save), unlike db::HUMAN_NON_PLAYER (see <see cref="FindHumanManagers"/>'s
        /// RecordAddress), which turned out to hold only the record(s) linked to a human account, not staff
        /// in general - that was the wrong class for this, caught by checking the live instance count
        /// independently (`rtti-instances`) before trusting the result. Uses the cached heap scan (scanning
        /// first if needed).
        ///
        /// Job/role (manager, coach, physio, scout, ...) is still NOT decoded. db::JOB has no live instances
        /// of its own, so it's a plain integer somewhere rather than an object reference, but which field
        /// wasn't identified: neither the non-player block nor its db::FULL_CONTRACT held an obvious
        /// candidate when checked against a real record, and a live-identified physio (by name and ID from
        /// the game's own UI) could not be re-located via a direct pointer trace from their name text (FM27
        /// seems to keep an indexed name-lookup blob separate from the live per-person string objects,
        /// unlike the direct 1-2 hop chase that worked for the human manager). Add Job once a field is
        /// confirmed against a known real job; until then every result's role is simply unknown.
        /// </summary>
        public IReadOnlyList<Fm27StaffInfo> FindStaff()
        {
            if (!NonPlayersAvailable)
            {
                return new List<Fm27StaffInfo>();
            }

            HeapScan heap = EnsureScan();
            var result = new List<Fm27StaffInfo>();

            foreach (long address in heap.NonPlayers)
            {
                long blockStart = address - Layout.NonPlayerIdFromBlock;
                long contract = ProcessHeap.ReadPointer(blockStart + Layout.NonPlayerContract);
                long team = contract == 0 ? 0 : ProcessHeap.ReadPointer(contract + Layout.ContractTeam);
                long club = team == 0 ? 0 : ProcessHeap.ReadPointer(team + Layout.TeamClub);
                string clubName = club == 0
                    ? ""
                    : ProcessHeap.ReadLengthPrefixedString(ProcessHeap.ReadPointer(club + Layout.ClubName)) ?? "";

                result.Add(new Fm27StaffInfo
                {
                    Address = address,
                    FullName = ProcessHeap.ReadLengthPrefixedString(ProcessHeap.ReadPointer(blockStart + Layout.NonPlayerFullName)) ?? "",
                    FirstName = ProcessHeap.ReadNameEntry(ProcessHeap.ReadPointer(blockStart + Layout.NonPlayerFirstName)) ?? "",
                    SecondName = ProcessHeap.ReadNameEntry(ProcessHeap.ReadPointer(blockStart + Layout.NonPlayerSecondName)) ?? "",
                    CommonName = ProcessHeap.ReadNameEntry(ProcessHeap.ReadPointer(blockStart + Layout.NonPlayerCommonName)) ?? "",
                    ClubAddress = club,
                    ClubName = clubName,
                });
            }

            return result;
        }

        /// <summary>
        /// Addresses of every player in the save (the db::ACTUAL_PLAYER "id" sub-object, which is
        /// the base the legacy Player entity reads from). Empty when players aren't available.
        /// </summary>
        public IReadOnlyList<long> FindPlayerAddresses()
        {
            return PlayersAvailable ? EnsureScan().Players : new List<long>();
        }

        private sealed class TeamEntry
        {
            /// <summary>True only for a real db::TEAM object (vtable checked); needs TeamVtableRva.</summary>
            public bool Valid;

            public int Type = -1;
            public long Club;
            public string ClubName = "";
            public string ClubShortName = "";
        }

        /// <summary>Reads every player in the save. Names and teams are cached because they repeat across
        /// thousands of players. A parallel version of this loop (Parallel.ForEach over the addresses,
        /// thread-safe caches) was tried and measured roughly on par with this sequential one, not reliably
        /// faster, once compared over several repeated runs rather than a single noisy sample - so left
        /// sequential for the simpler, deterministic-order code.</summary>
        public IReadOnlyList<Fm27PlayerInfo> FindPlayers()
        {
            var players = new List<Fm27PlayerInfo>();
            var fullNames = new Dictionary<long, string>();
            var nameEntries = new Dictionary<long, string>();
            var nations = new Dictionary<long, string>();
            var teams = new Dictionary<long, TeamEntry>();
            var noTeam = new TeamEntry();
            long teamVtable = TeamVtableRva == 0 ? 0 : ModuleBase + TeamVtableRva;

            string Cached(Dictionary<long, string> cache, long key, Func<long, string?> read)
            {
                if (key == 0)
                {
                    return "";
                }

                if (!cache.TryGetValue(key, out string? value))
                {
                    value = read(key) ?? "";
                    cache[key] = value;
                }

                return value;
            }

            // A team pointer -> its squad type and club. Valid only when the object really is a db::TEAM.
            TeamEntry Team(long pointer)
            {
                if (pointer == 0)
                {
                    return noTeam;
                }

                if (teams.TryGetValue(pointer, out TeamEntry? known))
                {
                    return known;
                }

                var entry = new TeamEntry();
                // 56 bytes: the club pointer at +0x30 is 8 bytes wide, so it ends at 0x38.
                byte[]? head = ProcessHeap.TryRead(pointer, 56);
                if (head != null)
                {
                    entry.Valid = teamVtable != 0 && BitConverter.ToInt64(head, 0) == teamVtable;
                    entry.Type = head[(int)Layout.TeamType];
                    entry.Club = BitConverter.ToInt64(head, (int)Layout.TeamClub);
                }

                if (entry.Club != 0)
                {
                    entry.ClubName = ProcessHeap.ReadLengthPrefixedString(ProcessHeap.ReadPointer(entry.Club + Layout.ClubName)) ?? "";
                    entry.ClubShortName = ProcessHeap.ReadLengthPrefixedString(ProcessHeap.ReadPointer(entry.Club + Layout.ClubShortName)) ?? "";
                }

                teams[pointer] = entry;
                return entry;
            }

            foreach (long address in FindPlayerAddresses())
            {
                long blockStart = address - Layout.PlayerIdFromBlock;
                byte[]? block = ProcessHeap.TryRead(blockStart, Layout.PlayerBlockLength);
                if (block == null)
                {
                    continue;
                }

                short potentialRaw = BitConverter.ToInt16(block, Layout.PlayerPotentialAbility);
                var info = new Fm27PlayerInfo
                {
                    Address = address,
                    Uid = BitConverter.ToUInt32(block, Layout.PlayerUid),
                    CurrentAbility = BitConverter.ToUInt16(block, Layout.PlayerCurrentAbility),
                    PotentialAbilityRaw = potentialRaw,
                    PotentialAbility = DecodePotential(potentialRaw),
                    HeightCm = BitConverter.ToUInt16(block, Layout.PlayerHeight),
                    BirthDayOfYear = BitConverter.ToUInt16(block, Layout.PlayerBirthDay),
                    BirthYear = BitConverter.ToUInt16(block, Layout.PlayerBirthYear),
                    PositionRatings = block.Skip(Layout.PlayerPositions).Take(Fm27Names.Positions.Length).ToArray(),
                    TopRoleIds = block.Skip(Layout.PlayerTopRoles).Take(6).ToArray(),
                    FullName = Cached(fullNames, BitConverter.ToInt64(block, Layout.PlayerFullName), p => ProcessHeap.ReadLengthPrefixedString(p)),
                    FirstName = Cached(nameEntries, BitConverter.ToInt64(block, Layout.PlayerFirstName), ProcessHeap.ReadNameEntry),
                    SecondName = Cached(nameEntries, BitConverter.ToInt64(block, Layout.PlayerSecondName), ProcessHeap.ReadNameEntry),
                    CommonName = Cached(nameEntries, BitConverter.ToInt64(block, Layout.PlayerCommonName), ProcessHeap.ReadNameEntry),
                    Nation = Cached(nations, BitConverter.ToInt64(block, Layout.PlayerNation),
                        nation => ProcessHeap.ReadLengthPrefixedString(ProcessHeap.ReadPointer(nation + Layout.NationName))),
                };

                // Owner club: the contract's team (preferred pointer, then the fallback) -> its club.
                TeamEntry owner = noTeam;
                long contract = BitConverter.ToInt64(block, (int)Layout.PlayerContract);
                if (contract != 0)
                {
                    info.ContractAddress = contract;
                    byte[]? pointers = ProcessHeap.TryRead(contract + Layout.ContractTeamFallback, 16);
                    if (pointers != null)
                    {
                        owner = Team(BitConverter.ToInt64(pointers, 8));
                        if (owner.ClubName.Length == 0)
                        {
                            owner = Team(BitConverter.ToInt64(pointers, 0));
                        }
                    }
                }

                info.ClubName = owner.ClubName;
                info.ClubShortName = owner.ClubShortName;
                info.ClubAddress = owner.Club;

                // Current team: which squad of which club the player is registered with right now.
                long currentTeam = ProcessHeap.ReadPointer(blockStart + Layout.PlayerCurrentTeam);
                TeamEntry current = Team(currentTeam);
                if (current.Valid)
                {
                    info.CurrentTeamAddress = currentTeam;
                    info.SquadType = current.Type;
                    info.CurrentClubAddress = current.Club;
                    info.CurrentClubName = current.ClubName;
                    info.PlaysForOtherClub = owner.Club != 0 && current.Club != 0 && current.Club != owner.Club;
                }

                players.Add(info);
            }

            ClassifyRegistrationsAtOtherClubs(players);
            return players;
        }

        /// <summary>A club needs at least this many registered players before it can be called an affiliate of another club.</summary>
        public const int AffiliateMinPlayers = 8;

        /// <summary>
        /// Splits "registered with another club's team" into loans and affiliate (B-team) registrations: a
        /// destination club most of whose registered players belong to one single owner is that owner's affiliate.
        /// </summary>
        private static void ClassifyRegistrationsAtOtherClubs(List<Fm27PlayerInfo> players)
        {
            var registered = players
                .Where(p => p.CurrentClubAddress != 0)
                .GroupBy(p => p.CurrentClubAddress)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var destination in registered)
            {
                List<Fm27PlayerInfo> inside = destination.Value;
                long affiliateOf = 0;
                if (inside.Count >= AffiliateMinPlayers)
                {
                    var topOwner = inside
                        .Where(p => p.ClubAddress != 0 && p.ClubAddress != destination.Key)
                        .GroupBy(p => p.ClubAddress)
                        .OrderByDescending(g => g.Count())
                        .FirstOrDefault();
                    if (topOwner != null && topOwner.Count() * 2 >= inside.Count)
                    {
                        affiliateOf = topOwner.Key;
                    }
                }

                foreach (Fm27PlayerInfo p in inside.Where(x => x.PlaysForOtherClub))
                {
                    if (affiliateOf != 0 && p.ClubAddress == affiliateOf)
                    {
                        p.IsWithAffiliateClub = true;
                    }
                    else
                    {
                        p.IsOnLoan = true;
                        p.LoanClubName = p.CurrentClubName;
                    }
                }
            }
        }

        /// <summary>
        /// Groups players by the team they are registered with now: one squad per club team (first team,
        /// reserves, U21, ...). Players on loan appear in the squad of the club they are playing for.
        /// Pass an existing <see cref="FindPlayers"/> result to avoid reading everyone again.
        /// </summary>
        public IReadOnlyList<Fm27SquadInfo> FindSquads(IReadOnlyList<Fm27PlayerInfo>? players = null)
        {
            players ??= FindPlayers();
            return players
                .Where(p => p.CurrentTeamAddress != 0 && p.SquadType >= 0)
                .GroupBy(p => p.CurrentTeamAddress)
                .Select(group =>
                {
                    Fm27PlayerInfo first = group.First();
                    return new Fm27SquadInfo
                    {
                        TeamAddress = group.Key,
                        ClubAddress = first.CurrentClubAddress,
                        ClubName = first.CurrentClubName,
                        SquadType = first.SquadType,
                        Players = group.ToList(),
                    };
                })
                .OrderBy(s => s.ClubName, StringComparer.OrdinalIgnoreCase)
                .ThenBy(s => s.SquadType)
                .ToList();
        }

        /// <summary>
        /// Positive raw values are the potential ability itself. Negative values are the game's
        /// encoding for a hidden / star-rated potential; this follows FM Moleman's decoding and has
        /// not been independently verified.
        /// </summary>
        private static int DecodePotential(short raw)
        {
            if (raw >= 0)
            {
                return raw;
            }

            double scaled = -raw >= 15 ? -raw / 10.0 : -raw;
            return (int)Math.Max(1, Math.Min(200, Math.Round(20.0 * scaled - 15.0)));
        }
    }

    /// <summary>
    /// Implemented by version definitions that locate the game through RTTI rather than the legacy
    /// MainAddress/XorDistance master table. Such versions have no ObjectManager master tables.
    /// </summary>
    public interface IFm27Version
    {
        Fm27Game Game { get; }
    }
}
