using System;
using FMScoutFramework.Core.Entities.GameVersions;

namespace FMScoutFramework.Core.Offsets {
  public sealed class NationOffsets {
    public IVersion Version;

    public NationOffsets(IVersion version) {
      this.Version = version;
    }

    public const short RowID = 0x4;   // done
    public const short UID = 0xC;     // done
    public const short Teams = 0x18;  // done
    public const short RivalNations = 0x48; //done

    public short Name {
      get { return 0xB8; }  // done
    }

    public const short ShortName = 0xC0;        // done
    public const short ThreeLetterName = 0xD0;  // done
    public const short Nationality = 0xD8;      // done

    public const short TaxRules = 0x100;      
    public const short NonForeignRules = 0x190;
    public const short EECForeignRules = 0x1A8;
    public const short Agreements = 0x1C0;
    public const short Capital = 0x1D8;         // done
    public const short Continent = 0x1E0;       // done
    public const short Region = 0x1E8;
    public const short Currency = 0x1F0;
    public const short GainNationalityType = 0x1F8; // done

    public const short NationalStadium = 0x200;
    public const short FIFAPosition = 0x290;
    public const short FIFARankingPoints = 0x292;
    public const short FIFARankingMatches = 0x388;
    public const short ContCupCoefForNT = 0x3A0;
    public const short ContCupCoefForNTCycle1 = 0x3A4;
    public const short ContCupCoefForNTCycle2 = 0x3A8;
    public const short ContCupCoefForNTCycle3 = 0x3AC;
    public const short ContCupCoefForNTCycle3WBigTournPts = 0x3B0;
    public const short ContCupCoefForNTLastYearCalc = 0x3B4;
    public const short ContCupCoefForNTGamesTBCForCycle3 = 0x3B6;
    public const short UEFACoefficient = 0x3B8;
    public const short LeagueStandard = 0x458;
    public const short DoesNotAllowDualNationality = 0x459;
    public const short OverlapsYearsFlag = 0x460;
    public const short Rules = 0x468;
    public const short StateOfDevelopment = 0x7B0;
    public const short MaxYouthAge = 0x7B5;
    public const short YearsToGainNationality = 0x7B7;
    public const short EconomicFactor = 0x7B8;
    public const short FAFinancialPower = 0x7BE;
    public const short FAPatience = 0x7C1;
    public const short ForeignManagerLikelihood = 0x7C2;
    public const short CentrePointLongitude = 0x748;
    public const short CentrePointLatitude = 0x740;
  }
}
