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

    public const short ClubInfoOne = 0xB0;
    public const short Name= 0xB8;  // done
    public const short ShortName = 0xC0;        // done
    public const short ThreeLetterName = 0xD0;  // done
    public const short Nationality = 0xD8;      // done

    public const short TaxRules = 0x118;      
    public const short NonForeignRules = 0x1A8;
    public const short EECForeignRules = 0x1C0;
    public const short Agreements = 0x1D8;
    public const short Capital = 0x1F0;         // done
    public const short Continent = 0x1F8;       // done
    public const short Region = 0x200;
    public const short Currency = 0x208;
    public const short GainNationalityType = 0x210; // done

    public const short NationalStadium = 0x218;
    public const short FIFAPosition = 0x2A8;
    public const short FIFARankingPoints = 0x2AA;
    public const short FIFARankingMatches = 0x3A0;
    public const short ContCupCoefForNT = 0x3B8;
    public const short ContCupCoefForNTCycle1 = 0x3BC;
    public const short ContCupCoefForNTCycle2 = 0x3C0;
    public const short ContCupCoefForNTCycle3 = 0x3C4;
    public const short ContCupCoefForNTCycle3WBigTournPts = 0x3C8;
    public const short ContCupCoefForNTLastYearCalc = 0x3CC;
    public const short ContCupCoefForNTGamesTBCForCycle3 = 0x3D0;
    public const short UEFACoefficient = 0x3D0;
    public const short LeagueStandard = 0x470;
    public const short DoesNotAllowDualNationality = 0x471;
    public const short OverlapsYearsFlag = 0x478;
    public const short Rules = 0x480;
    public const short CentrePointLatitude = 0x758;
    public const short CentrePointLongitude = 0x760;

    public short StateOfDevelopment {
      get {
        if (this.Version is Steam_19_1_0_Windows
          || this.Version is Steam_19_1_0_Touch_Windows
          || this.Version is Steam_19_1_1_Windows
          || this.Version is Steam_19_1_1_Touch_Windows
          || this.Version is Steam_19_1_2_Windows
          || this.Version is Steam_19_1_2_Touch_Windows
          || this.Version is Steam_19_1_3_Windows
          || this.Version is Steam_19_1_4_Windows
          || this.Version is Steam_19_1_4_Touch_Windows
          || this.Version is Steam_19_1_5_Windows
          || this.Version is Steam_19_1_5_Touch_Windows
          || this.Version is Steam_19_2_0_Windows
          || this.Version is Steam_19_2_0_Touch_Windows
          || this.Version is Steam_19_2_1_Windows
          || this.Version is Steam_19_2_1_Touch_Windows
          || this.Version is Steam_19_2_2_Windows
          || this.Version is Steam_19_2_2_Touch_Windows
          || this.Version is Steam_19_2_3_Windows
          || this.Version is Steam_19_2_3_Touch_Windows
          || this.Version is Steam_19_3_0_Windows
          || this.Version is Steam_19_3_1_Touch_Windows
          || this.Version is Steam_19_3_2_Windows
          || this.Version is Steam_19_3_3_Windows
          || this.Version is Steam_19_3_4_Windows
          || this.Version is Steam_19_3_4_Touch_Windows
          || this.Version is Steam_19_3_5_Windows
          || this.Version is Steam_19_3_5_Touch_Windows
          || this.Version is Steam_19_3_6_Windows
          || this.Version is Steam_19_3_5_GamePass_Windows
          || this.Version is Steam_19_3_5_1_GamePass_Windows
          || this.Version is Steam_19_3_5_2_GamePass_Windows) {
          return 0x8A8;
        }
        else {
          return 0x8C0;
        }
      }
    }

    public short Importance {
      get {
        if (this.Version is Steam_19_1_0_Windows
          || this.Version is Steam_19_1_0_Touch_Windows
          || this.Version is Steam_19_1_1_Windows
          || this.Version is Steam_19_1_1_Touch_Windows
          || this.Version is Steam_19_1_2_Windows
          || this.Version is Steam_19_1_2_Touch_Windows
          || this.Version is Steam_19_1_3_Windows
          || this.Version is Steam_19_1_4_Windows
          || this.Version is Steam_19_1_4_Touch_Windows
          || this.Version is Steam_19_1_5_Windows
          || this.Version is Steam_19_1_5_Touch_Windows
          || this.Version is Steam_19_2_0_Windows
          || this.Version is Steam_19_2_0_Touch_Windows
          || this.Version is Steam_19_2_1_Windows
          || this.Version is Steam_19_2_1_Touch_Windows
          || this.Version is Steam_19_2_2_Windows
          || this.Version is Steam_19_2_2_Touch_Windows
          || this.Version is Steam_19_2_3_Windows
          || this.Version is Steam_19_2_3_Touch_Windows
          || this.Version is Steam_19_3_0_Windows
          || this.Version is Steam_19_3_1_Touch_Windows
          || this.Version is Steam_19_3_2_Windows
          || this.Version is Steam_19_3_3_Windows
          || this.Version is Steam_19_3_4_Windows
          || this.Version is Steam_19_3_4_Touch_Windows
          || this.Version is Steam_19_3_5_Windows
          || this.Version is Steam_19_3_5_Touch_Windows
          || this.Version is Steam_19_3_6_Windows
          || this.Version is Steam_19_3_5_GamePass_Windows
          || this.Version is Steam_19_3_5_1_GamePass_Windows
          || this.Version is Steam_19_3_5_2_GamePass_Windows) {
          return 0x8A9;
        }
        else {
          return 0x8C1;
        }
      }
    }
        
    public short EconomicFactor {
      get {
        if (this.Version is Steam_19_1_0_Windows
          || this.Version is Steam_19_1_0_Touch_Windows
          || this.Version is Steam_19_1_1_Windows
          || this.Version is Steam_19_1_1_Touch_Windows
          || this.Version is Steam_19_1_2_Windows
          || this.Version is Steam_19_1_2_Touch_Windows
          || this.Version is Steam_19_1_3_Windows
          || this.Version is Steam_19_1_4_Windows
          || this.Version is Steam_19_1_4_Touch_Windows
          || this.Version is Steam_19_1_5_Windows
          || this.Version is Steam_19_1_5_Touch_Windows
          || this.Version is Steam_19_2_0_Windows
          || this.Version is Steam_19_2_0_Touch_Windows
          || this.Version is Steam_19_2_1_Windows
          || this.Version is Steam_19_2_1_Touch_Windows
          || this.Version is Steam_19_2_2_Windows
          || this.Version is Steam_19_2_2_Touch_Windows
          || this.Version is Steam_19_2_3_Windows
          || this.Version is Steam_19_2_3_Touch_Windows
          || this.Version is Steam_19_3_0_Windows
          || this.Version is Steam_19_3_1_Touch_Windows
          || this.Version is Steam_19_3_2_Windows
          || this.Version is Steam_19_3_3_Windows
          || this.Version is Steam_19_3_4_Windows
          || this.Version is Steam_19_3_4_Touch_Windows
          || this.Version is Steam_19_3_5_Windows
          || this.Version is Steam_19_3_5_Touch_Windows
          || this.Version is Steam_19_3_6_Windows
          || this.Version is Steam_19_3_5_GamePass_Windows
          || this.Version is Steam_19_3_5_1_GamePass_Windows
          || this.Version is Steam_19_3_5_2_GamePass_Windows) {
          return 0x8AD;
        }
        else {
          return 0x8C5;
        }
      }
    }

    public short FAFinancialPower {
      get {
        if (this.Version is Steam_19_1_0_Windows
          || this.Version is Steam_19_1_0_Touch_Windows
          || this.Version is Steam_19_1_1_Windows
          || this.Version is Steam_19_1_1_Touch_Windows
          || this.Version is Steam_19_1_2_Windows
          || this.Version is Steam_19_1_2_Touch_Windows
          || this.Version is Steam_19_1_3_Windows
          || this.Version is Steam_19_1_4_Windows
          || this.Version is Steam_19_1_4_Touch_Windows
          || this.Version is Steam_19_1_5_Windows
          || this.Version is Steam_19_1_5_Touch_Windows
          || this.Version is Steam_19_2_0_Windows
          || this.Version is Steam_19_2_0_Touch_Windows
          || this.Version is Steam_19_2_1_Windows
          || this.Version is Steam_19_2_1_Touch_Windows
          || this.Version is Steam_19_2_2_Windows
          || this.Version is Steam_19_2_2_Touch_Windows
          || this.Version is Steam_19_2_3_Windows
          || this.Version is Steam_19_2_3_Touch_Windows
          || this.Version is Steam_19_3_0_Windows
          || this.Version is Steam_19_3_1_Touch_Windows
          || this.Version is Steam_19_3_2_Windows
          || this.Version is Steam_19_3_3_Windows
          || this.Version is Steam_19_3_4_Windows
          || this.Version is Steam_19_3_4_Touch_Windows
          || this.Version is Steam_19_3_5_Windows
          || this.Version is Steam_19_3_5_Touch_Windows
          || this.Version is Steam_19_3_6_Windows
          || this.Version is Steam_19_3_5_GamePass_Windows
          || this.Version is Steam_19_3_5_1_GamePass_Windows
          || this.Version is Steam_19_3_5_2_GamePass_Windows) {
          return 0x8AE;
        }
        else {
          return 0x8C6;
        }
      }
    } 

    public short YearsToGainNationality {
      get {
        if (this.Version is Steam_19_1_0_Windows
          || this.Version is Steam_19_1_0_Touch_Windows
          || this.Version is Steam_19_1_1_Windows
          || this.Version is Steam_19_1_1_Touch_Windows
          || this.Version is Steam_19_1_2_Windows
          || this.Version is Steam_19_1_2_Touch_Windows
          || this.Version is Steam_19_1_3_Windows
          || this.Version is Steam_19_1_4_Windows
          || this.Version is Steam_19_1_4_Touch_Windows
          || this.Version is Steam_19_1_5_Windows
          || this.Version is Steam_19_1_5_Touch_Windows
          || this.Version is Steam_19_2_0_Windows
          || this.Version is Steam_19_2_0_Touch_Windows
          || this.Version is Steam_19_2_1_Windows
          || this.Version is Steam_19_2_1_Touch_Windows
          || this.Version is Steam_19_2_2_Windows
          || this.Version is Steam_19_2_2_Touch_Windows
          || this.Version is Steam_19_2_3_Windows
          || this.Version is Steam_19_2_3_Touch_Windows
          || this.Version is Steam_19_3_0_Windows
          || this.Version is Steam_19_3_1_Touch_Windows
          || this.Version is Steam_19_3_2_Windows
          || this.Version is Steam_19_3_3_Windows
          || this.Version is Steam_19_3_4_Windows
          || this.Version is Steam_19_3_4_Touch_Windows
          || this.Version is Steam_19_3_5_Windows
          || this.Version is Steam_19_3_5_Touch_Windows
          || this.Version is Steam_19_3_6_Windows
          || this.Version is Steam_19_3_5_GamePass_Windows
          || this.Version is Steam_19_3_5_1_GamePass_Windows
          || this.Version is Steam_19_3_5_2_GamePass_Windows) {
          return 0x8AF;
        }
        else {
          return 0x8C7;
        }
      }
    } 

    public short MaxYouthAge {
      get {
        if (this.Version is Steam_19_1_0_Windows
          || this.Version is Steam_19_1_0_Touch_Windows
          || this.Version is Steam_19_1_1_Windows
          || this.Version is Steam_19_1_1_Touch_Windows
          || this.Version is Steam_19_1_2_Windows
          || this.Version is Steam_19_1_2_Touch_Windows
          || this.Version is Steam_19_1_3_Windows
          || this.Version is Steam_19_1_4_Windows
          || this.Version is Steam_19_1_4_Touch_Windows
          || this.Version is Steam_19_1_5_Windows
          || this.Version is Steam_19_1_5_Touch_Windows
          || this.Version is Steam_19_2_0_Windows
          || this.Version is Steam_19_2_0_Touch_Windows
          || this.Version is Steam_19_2_1_Windows
          || this.Version is Steam_19_2_1_Touch_Windows
          || this.Version is Steam_19_2_2_Windows
          || this.Version is Steam_19_2_2_Touch_Windows
          || this.Version is Steam_19_2_3_Windows
          || this.Version is Steam_19_2_3_Touch_Windows
          || this.Version is Steam_19_3_0_Windows
          || this.Version is Steam_19_3_1_Touch_Windows
          || this.Version is Steam_19_3_2_Windows
          || this.Version is Steam_19_3_3_Windows
          || this.Version is Steam_19_3_4_Windows
          || this.Version is Steam_19_3_4_Touch_Windows
          || this.Version is Steam_19_3_5_Windows
          || this.Version is Steam_19_3_5_Touch_Windows
          || this.Version is Steam_19_3_6_Windows
          || this.Version is Steam_19_3_5_GamePass_Windows
          || this.Version is Steam_19_3_5_1_GamePass_Windows
          || this.Version is Steam_19_3_5_2_GamePass_Windows) {
          return 0x8B0;
        }
        else {
          return 0x8C8;
        }
      }
    } 

    public short FAPatience {
      get {
        if (this.Version is Steam_19_1_0_Windows
          || this.Version is Steam_19_1_0_Touch_Windows
          || this.Version is Steam_19_1_1_Windows
          || this.Version is Steam_19_1_1_Touch_Windows
          || this.Version is Steam_19_1_2_Windows
          || this.Version is Steam_19_1_2_Touch_Windows
          || this.Version is Steam_19_1_3_Windows
          || this.Version is Steam_19_1_4_Windows
          || this.Version is Steam_19_1_4_Touch_Windows
          || this.Version is Steam_19_1_5_Windows
          || this.Version is Steam_19_1_5_Touch_Windows
          || this.Version is Steam_19_2_0_Windows
          || this.Version is Steam_19_2_0_Touch_Windows
          || this.Version is Steam_19_2_1_Windows
          || this.Version is Steam_19_2_1_Touch_Windows
          || this.Version is Steam_19_2_2_Windows
          || this.Version is Steam_19_2_2_Touch_Windows
          || this.Version is Steam_19_2_3_Windows
          || this.Version is Steam_19_2_3_Touch_Windows
          || this.Version is Steam_19_3_0_Windows
          || this.Version is Steam_19_3_1_Touch_Windows
          || this.Version is Steam_19_3_2_Windows
          || this.Version is Steam_19_3_3_Windows
          || this.Version is Steam_19_3_4_Windows
          || this.Version is Steam_19_3_4_Touch_Windows
          || this.Version is Steam_19_3_5_Windows
          || this.Version is Steam_19_3_5_Touch_Windows
          || this.Version is Steam_19_3_6_Windows
          || this.Version is Steam_19_3_5_GamePass_Windows
          || this.Version is Steam_19_3_5_1_GamePass_Windows
          || this.Version is Steam_19_3_5_2_GamePass_Windows) {
          return 0x8B9;
        }
        else {
          return 0x8D1;
        }
      }
    }

    public short ForeignManagerLikelihood {
      get {
        if (this.Version is Steam_19_1_0_Windows
          || this.Version is Steam_19_1_0_Touch_Windows
          || this.Version is Steam_19_1_1_Windows
          || this.Version is Steam_19_1_1_Touch_Windows
          || this.Version is Steam_19_1_2_Windows
          || this.Version is Steam_19_1_2_Touch_Windows
          || this.Version is Steam_19_1_3_Windows
          || this.Version is Steam_19_1_4_Windows
          || this.Version is Steam_19_1_4_Touch_Windows
          || this.Version is Steam_19_1_5_Windows
          || this.Version is Steam_19_1_5_Touch_Windows
          || this.Version is Steam_19_2_0_Windows
          || this.Version is Steam_19_2_0_Touch_Windows
          || this.Version is Steam_19_2_1_Windows
          || this.Version is Steam_19_2_1_Touch_Windows
          || this.Version is Steam_19_2_2_Windows
          || this.Version is Steam_19_2_2_Touch_Windows
          || this.Version is Steam_19_2_3_Windows
          || this.Version is Steam_19_2_3_Touch_Windows
          || this.Version is Steam_19_3_0_Windows
          || this.Version is Steam_19_3_1_Touch_Windows
          || this.Version is Steam_19_3_2_Windows
          || this.Version is Steam_19_3_3_Windows
          || this.Version is Steam_19_3_4_Windows
          || this.Version is Steam_19_3_4_Touch_Windows
          || this.Version is Steam_19_3_5_Windows
          || this.Version is Steam_19_3_5_Touch_Windows
          || this.Version is Steam_19_3_6_Windows
          || this.Version is Steam_19_3_5_GamePass_Windows
          || this.Version is Steam_19_3_5_1_GamePass_Windows
          || this.Version is Steam_19_3_5_2_GamePass_Windows) {
          return 0x8BA;
        }
        else {
          return 0x8D2;
        }
      }
    } 
  }
}
