#nullable enable
namespace FM.Pitchside.Core.Defines.Versions
{
    /// <summary>
    /// Name tables for the FM27 player block. Generated from the tables in FM Moleman 27 (positions, in- and
    /// out-of-possession roles, and the map from stored position order to the game's internal position number)
    /// and checked against the live game: every goalkeeper's roles are goalkeeper roles, every centre-back's are
    /// centre-back roles, and so on (see OffsetFinder fm27-roles). Null entries are role ids the game reserves
    /// but does not use.
    /// </summary>
    public static class Fm27Names
    {
        /// <summary>The 15 stored position proficiencies, in storage order.</summary>
        public static readonly string[] Positions =
        {
            "GK", "SW", "DL", "DC", "DR", "DM", "ML", "MC",
            "MR", "AML", "AMC", "AMR", "ST", "WBL", "WBR"
        };

        /// <summary>In-possession role names, indexed by role id (0-63).</summary>
        public static readonly string?[] RolesInPossession =
        {
            "Goalkeeper", "Centre-Back", "Full-Back", "Wing-Back", "Defensive Midfielder", "Central Midfielder", "Wide Midfielder", "Winger",
            null, "Attacking Midfielder", "Deep-Lying Forward", "Centre Forward", "Ball-Playing Goalkeeper", null, "Advanced Centre-Back", "Deep-Lying Playmaker",
            "Box-To-Box Midfielder", "Advanced Playmaker", "Target Forward", "Poacher", null, null, null, null,
            "Ball-Playing Centre-Back", null, null, "Inside Forward", null, "No-Nonsense Centre-Back", null, "Channel Forward",
            "Free Role", null, null, "Half-Back", null, null, "Advanced Wing-Back", null,
            "False Nine", "Second Striker", null, "Playmaking Winger", "Inside Wing-Back", "Wide Forward", "Box-To-Box Playmaker", null,
            "Wide Central Midfielder", "Inside Winger", null, "Wide Centre-Back", "Inside Full-Back", "No-Nonsense Goalkeeper", "Overlapping Centre-Back", "Playmaking Wing-Back",
            "Channel Midfielder", "Midfield Playmaker", null, null, null, null, null, null
        };

        /// <summary>Out-of-possession role names, indexed by role id (0-63).</summary>
        public static readonly string?[] RolesOutOfPossession =
        {
            "Goalkeeper", "Sweeper Keeper", "Line-Holding Goalkeeper", "Centre-Back", "Stopping Centre-Back", "Covering Centre-Back", "Wide Centre-Back", "Stopping Wide Centre-Back",
            "Covering Wide Centre-Back", "Full-Back", "Pressing Full-Back", "Holding Full-Back", "Wing-Back", "Pressing Wing-Back", "Holding Wing-Back", "Defensive Midfielder",
            "Dropping Defensive Midfielder", "Pressing Defensive Midfielder", "Screening Defensive Midfielder", "Wide Covering Defensive Midfielder", "Central Midfielder", null, null, null,
            null, null, null, "Pressing Central Midfielder", "Screening Central Midfielder", "Wide Covering Central Midfielder", "Attacking Midfielder", "Tracking Attacking Midfielder",
            "Central Outlet Attacking Midfielder", "Splitting Outlet Attacking Midfielder", null, "Wide Midfielder", "Tracking Wide Midfielder", "Wide Outlet Wide Midfielder", "Winger", "Tracking Winger",
            "Inside Outlet Winger", "Wide Outlet Winger", "Centre Forward", "Tracking Centre Forward", "Central Outlet Centre Forward", "Splitting Outlet Centre Forward", null, null,
            null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null
        };

        /// <summary>
        /// Name of a team's squad type (the byte at team+40): first team, reserves, the youth sides and so on.
        /// Types the table doesn't know come back as "Other (n)".
        /// </summary>
        public static string SquadName(int type)
        {
            switch (type)
            {
                case 0: return "First team";
                case 1: return "Reserves";
                case 3: return "B team";
                case 9: return "U23";
                case 10: return "U21";
                case 11: return "U19";
                case 12: return "U18";
                case 14: return "Amateurs";
                case 15: return "II";
                case 16: return "2nd team";
                case 17: return "III";
                case 18: return "U20";
                case 21: return "Youths";
                case 36: return "U19 B";
                default: return "Other (" + type + ")";
            }
        }

        /// <summary>For each stored position index, the game's internal position number (used when asking the game about roles).</summary>
        public static readonly int[] PositionToGame =
        {
            0, 1, 3, 4, 2, 7, 9, 10, 8, 12, 13, 11, 14, 6, 5
        };
    }
}
