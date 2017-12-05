using FMScoutFramework.Core.Entities.GameVersions;
using FMScoutFramework.Core.Managers;
using FMScoutFramework.Core.Offsets;
using FMScoutFramework.Entities.Ingame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMScoutFramework.Core.Entities.InGame {
  public class PreferredMoves : BaseObject, IPreferredMoves {

    private enum PreferredMovesFirst : uint {
      PMFRunsWithBallDownLeft = 0x01,         // OK
      PMFRunsWithBallDownRight = 0x02,         // OK
      PMFRunsWithBallThroughCentre = 0x04,         // OK
      PMFGetsIntoOppositionArea = 0x08,         // OK
      PMFMovesIntoChannels = 0x10,         // OK
      PMFGetsForwardWheneverPossible = 0x20,         // OK
      PMFPlaysShortSimplePasses = 0x40,         // OK
      PMFTriesKillerBallsOften = 0x80,         // OK
      PMFShootsFromDistance = 0x100,        // OK
      PMFShootsWithPower = 0x200,        // OK
      PMFPlacesShots = 0x400,        // OK
      PMFCurlsBall = 0x800,        // OK
      PMFLikesToRoundKeeper = 0x1000,       // OK
      PMFLikesToTryToBeatOffsideTrap = 0x2000,       // OK
      PMFUsesOutsideOfFoot = 0x4000,       // OK
      PMFMarksOpponentTightly = 0x8000,       // OK
      PMFWindsUpOpponents = 0x10000,      // OK
      PMFArguesWithOfficials = 0x20000,      // OK
      PMFPlaysWithBackToGoal = 0x40000,      // OK
      PMFComesDeepToGetBall = 0x80000,      // OK
      PMFPlaysOneTwos = 0x100000,     // OK
      PMFLikesToLobKeeper = 0x200000,     // OK
      PMFDictatesTempo = 0x400000,     // OK
      PMFAttemptsOverheadKicks = 0x800000,     // OK
      PMFLooksForPassNotShot = 0x1000000,    // OK
      PMFPlaysNoThroughBalls = 0x2000000,    // OK
      PMFStopsPlay = 0x4000000,    // OK
      PMFKnocksBallPastOpponent = 0x8000000,    // OK
      PMFMovesBallToRightFootBeforeDribbleAttempt = 0x10000000,   // OK
      PMFMovesBallToLeftFootBeforeDribbleAttempt = 0x20000000,   // OK
      PMFDwellsOnBall = 0x40000000,   // OK
      PMFArrivesLateInOpponentsArea = 0x80000000    // OK
    }

    private enum PreferredMovesSecond : uint {
      PMSTriesToPlayWayOutOfTrouble = 0x01,             // OK
      PMSStaysBackAtAllTimes = 0x02,             // OK
      PMSAvoidsUsingWeakerFoot = 0x04,             // OK
      PMSTriesTricks = 0x08,             // OK
      PMSTriesLongRangeFreeKicks = 0x10,             // OK
      PMSDivesIntoTackles = 0x20,             // OK
      PMSDoesNotDiveIntoTackles = 0x40,             // OK
      PMSCutsInsideFromBothWings = 0x80,             // OK
      PMSHugsLine = 0x100,            // OK
      PMSGetsCrowdGoing = 0x200,            // OK
      PMSTriesFirstTimeShots = 0x400,            // OK
      PMSTriesLongRangePasses = 0x800,            // OK
      PMSLikesBallPlayedIntoFeet = 0x1000,           // OK
      PMSHitsFreeKickWithPower = 0x2000,           // OK
      PMSLikesToBeatManRepeatedly = 0x4000,           // OK
      PMSLikesToSwitchBallToOtherFlank = 0x8000,           // OK
      PMSWillRetireAtTop = 0x10000,          // OK
      PMSWillPlayFootballAsLongAsPossible = 0x20000,          // OK
      PMSHasLongFlatBulletThrow = 0x40000,          // OK
      PMSRunsWithBallOften = 0x80000,          // OK
      PMSRunsWithBallRarely = 0x100000,         // OK
      PMSBoxPlayer = 0x400000,         // OK
      PMSUsesLongThrowsToStartCounterAttacks = 0x800000,         // OK
      PMSRefrainsFromTakingLongShots = 0x1000000,        // OK
      PMSCutsInsideFromLeftWing = 0x2000000,        // OK
      PMSCutsInsideFromRightWing = 0x4000000,        // OK
      PMSCrossesEarly = 0x8000000,        // OK
      PMSBringsBallOutOfDefense = 0x10000000        // OK
    }

    PreferredMovesOffsets PreferredMovesOffsets;

    public PreferredMoves(Int64 memoryAddress, IVersion version)
        : base(memoryAddress, version) {
      PreferredMovesOffsets = new PreferredMovesOffsets(version);
    }
    public PreferredMoves(Int64 memoryAddress, ArraySegment<byte> originalBytes, IVersion version)
        : base(memoryAddress, originalBytes, version) {
      PreferredMovesOffsets = new PreferredMovesOffsets(version);
    }

    public void Save() {
      // Start with 0x000000 and OR
      uint cFlagsOne = 0x0;
      uint cFlagsTwo = 0x0;
      if (RunsWithBallDownLeft) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFRunsWithBallDownLeft;
      }
      if (RunsWithBallDownRight) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFRunsWithBallDownRight;
      }
      if (RunsWithBallThroughCentre) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFRunsWithBallThroughCentre;
      }
      if (GetsIntoOppositionArea) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFGetsIntoOppositionArea;
      }
      if (MovesIntoChannels) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFMovesIntoChannels;
      }
      if (GetsForwardWheneverPossible) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFGetsForwardWheneverPossible;
      }
      if (PlaysShortSimplePasses) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFPlaysShortSimplePasses;
      }
      if (TriesKillerBallsOften) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFTriesKillerBallsOften;
      }
      if (ShootsFromDistance) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFShootsFromDistance;
      }
      if (ShootsWithPower) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFShootsWithPower;
      }
      if (PlacesShots) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFPlacesShots;
      }
      if (CurlsBall) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFCurlsBall;
      }
      if (LikesToRoundKeeper) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFLikesToRoundKeeper;
      }
      if (LikesToTryToBeatOffsideTrap) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFLikesToTryToBeatOffsideTrap;
      }
      if (UsesOutsideOfFoot) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFUsesOutsideOfFoot;
      }
      if (MarksOpponentTightly) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFMarksOpponentTightly;
      }
      if (WindsUpOpponents) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFWindsUpOpponents;
      }
      if (ArguesWithOfficials) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFArguesWithOfficials;
      }
      if (PlaysWithBackToGoal) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFPlaysWithBackToGoal;
      }
      if (ComesDeepToGetBall) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFComesDeepToGetBall;
      }
      if (PlaysOneTwos) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFPlaysOneTwos;
      }
      if (LikesToLobKeeper) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFLikesToLobKeeper;
      }
      if (DictatesTempo) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFDictatesTempo;
      }
      if (AttemptsOverheadKicks) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFAttemptsOverheadKicks;
      }
      if (LooksForPassNotShot) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFLooksForPassNotShot;
      }
      if (PlaysNoThroughBalls) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFPlaysNoThroughBalls;
      }
      if (StopsPlay) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFStopsPlay;
      }
      if (KnocksBallPastOpponent) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFKnocksBallPastOpponent;
      }
      if (MovesBallToRightFootBeforeDribbleAttempt) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFMovesBallToRightFootBeforeDribbleAttempt;
      }
      if (MovesBallToLeftFootBeforeDribbleAttempt) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFMovesBallToLeftFootBeforeDribbleAttempt;
      }
      if (DwellsOnBall) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFDwellsOnBall;
      }
      if (ArrivesLateInOpponentsArea) {
        cFlagsOne = cFlagsOne | (uint)PreferredMovesFirst.PMFArrivesLateInOpponentsArea;
      }
      if (TriesToPlayWayOutOfTrouble) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSTriesToPlayWayOutOfTrouble;
      }
      if (StaysBackAtAllTimes) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSStaysBackAtAllTimes;
      }
      if (AvoidsUsingWeakerFoot) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSAvoidsUsingWeakerFoot;
      }
      if (TriesTricks) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSTriesTricks;
      }
      if (TriesLongRangeFreeKicks) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSTriesLongRangeFreeKicks;
      }
      if (DivesIntoTackles) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSDivesIntoTackles;
      }
      if (DoesNotDiveIntoTackles) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSDoesNotDiveIntoTackles;
      }
      if (CutsInsideFromBothWings) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSCutsInsideFromBothWings;
      }
      if (HugsLine) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSHugsLine;
      }
      if (GetsCrowdGoing) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSGetsCrowdGoing;
      }
      if (TriesFirstTimeShots) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSTriesFirstTimeShots;
      }
      if (TriesLongRangePasses) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSTriesLongRangePasses;
      }
      if (LikesBallPlayedIntoFeet) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSLikesBallPlayedIntoFeet;
      }
      if (HitsFreeKickWithPower) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSHitsFreeKickWithPower;
      }
      if (LikesToBeatManRepeatedly) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSLikesToBeatManRepeatedly;
      }
      if (LikesToSwitchBallToOtherFlank) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSLikesToSwitchBallToOtherFlank;
      }
      if (HasLongFlatBulletThrow) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSHasLongFlatBulletThrow;
      }
      if (RunsWithBallOften) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSRunsWithBallOften;
      }
      if (RunsWithBallRarely) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSRunsWithBallRarely;
      }
      if (WillRetireAtTop) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSWillRetireAtTop;
      }
      if (WillPlayFootballAsLongAsPossible) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSWillPlayFootballAsLongAsPossible;
      }
      if (BoxPlayer) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSBoxPlayer;
      }
      if (UsesLongThrowsToStartCounterAttacks) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSUsesLongThrowsToStartCounterAttacks;
      }
      if (RefrainsFromTakingLongShots) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSRefrainsFromTakingLongShots;
      }
      if (CutsInsideFromLeftWing) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSCutsInsideFromLeftWing;
      }
      if (CutsInsideFromRightWing) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSCutsInsideFromRightWing;
      }
      if (CrossesEarly) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSCrossesEarly;
      }
      if (BringsBallOutOfDefense) {
        cFlagsTwo = cFlagsTwo | (uint)PreferredMovesSecond.PMSBringsBallOutOfDefense;
      }


      PropertyInvoker.Set<uint>(PreferredMovesOffsets.FlagsOne, OriginalBytes, MemoryAddress, DatabaseMode, cFlagsOne);
      PropertyInvoker.Set<uint>(PreferredMovesOffsets.FlagsTwo, OriginalBytes, MemoryAddress, DatabaseMode, cFlagsTwo);

      _isDirty = false;
    }

    private bool _isDirty = false;
    public bool isDirty {
      get {
        return _isDirty;
      }
      set {
        if (value) {
          Version.gameManager.RaiseObjectEdited(this);
        }
        _isDirty = value;
      }
    }

    private uint _movesFlagsOne = 0;
    public uint MovesFlagsOne {
      get {
        if (_movesFlagsOne == 0) {
          _movesFlagsOne = PropertyInvoker.Get<uint>(PreferredMovesOffsets.FlagsOne, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _movesFlagsOne;
      }
    }

    private uint _movesFlagsTwo = 0;
    public uint MovesFlagsTwo {
      get {
        if (_movesFlagsTwo == 0) {
          _movesFlagsTwo = PropertyInvoker.Get<uint>(PreferredMovesOffsets.FlagsTwo, OriginalBytes, MemoryAddress, DatabaseMode);
        }

        return _movesFlagsTwo;
      }
    }

    private uint _runsWithBallDownLeft = 1234;
    public bool RunsWithBallDownLeft {
      get {
        if (_runsWithBallDownLeft == 1234) {
          _runsWithBallDownLeft = MovesFlagsOne & (uint)PreferredMovesFirst.PMFRunsWithBallDownLeft;
        }

        return _runsWithBallDownLeft > 0;
      }
      set {
        if (_runsWithBallDownLeft != Convert.ToUInt32(value)) {
          isDirty = true;
          _runsWithBallDownLeft = Convert.ToUInt32(value);
        }
      }
    }

    private uint _runsWithBallDownRight = 1234;
    public bool RunsWithBallDownRight {
      get {
        if (_runsWithBallDownRight == 1234) {
          _runsWithBallDownRight = MovesFlagsOne & (uint)PreferredMovesFirst.PMFRunsWithBallDownRight;
        }

        return _runsWithBallDownRight > 0;
      }
      set {
        if (_runsWithBallDownRight != Convert.ToUInt32(value)) {
          isDirty = true;
          _runsWithBallDownRight = Convert.ToUInt32(value);
        }
      }
    }

    private uint _runsWithBallThroughCentre = 1234;
    public bool RunsWithBallThroughCentre {
      get {
        if (_runsWithBallThroughCentre == 1234) {
          _runsWithBallThroughCentre = MovesFlagsOne & (uint)PreferredMovesFirst.PMFRunsWithBallThroughCentre;
        }

        return _runsWithBallThroughCentre > 0;
      }
      set {
        if (_runsWithBallThroughCentre != Convert.ToUInt32(value)) {
          isDirty = true;
          _runsWithBallThroughCentre = Convert.ToUInt32(value);
        }
      }
    }

    private uint _getsIntoOppositionArea = 1234;
    public bool GetsIntoOppositionArea {
      get {
        if (_getsIntoOppositionArea == 1234) {
          _getsIntoOppositionArea = MovesFlagsOne & (uint)PreferredMovesFirst.PMFGetsIntoOppositionArea;
        }

        return _getsIntoOppositionArea > 0;
      }
      set {
        if (_getsIntoOppositionArea != Convert.ToUInt32(value)) {
          isDirty = true;
          _getsIntoOppositionArea = Convert.ToUInt32(value);
        }
      }
    }

    private uint _movesIntoChannels = 1234;
    public bool MovesIntoChannels {
      get {
        if (_movesIntoChannels == 1234) {
          _movesIntoChannels = MovesFlagsOne & (uint)PreferredMovesFirst.PMFMovesIntoChannels;
        }

        return _movesIntoChannels > 0;
      }
      set {
        if (_movesIntoChannels != Convert.ToUInt32(value)) {
          isDirty = true;
          _movesIntoChannels = Convert.ToUInt32(value);
        }
      }
    }

    private uint _getsForwardWheneverPossible = 1234;
    public bool GetsForwardWheneverPossible {
      get {
        if (_getsForwardWheneverPossible == 1234) {
          _getsForwardWheneverPossible = MovesFlagsOne & (uint)PreferredMovesFirst.PMFGetsForwardWheneverPossible;
        }

        return _getsForwardWheneverPossible > 0;
      }
      set {
        if (_getsForwardWheneverPossible != Convert.ToUInt32(value)) {
          isDirty = true;
          _getsForwardWheneverPossible = Convert.ToUInt32(value);
        }
      }
    }

    private uint _playsShortSimplePasses = 1234;
    public bool PlaysShortSimplePasses {
      get {
        if (_playsShortSimplePasses == 1234) {
          _playsShortSimplePasses = MovesFlagsOne & (uint)PreferredMovesFirst.PMFPlaysShortSimplePasses;
        }

        return _playsShortSimplePasses > 0;
      }
      set {
        if (_playsShortSimplePasses != Convert.ToUInt32(value)) {
          isDirty = true;
          _playsShortSimplePasses = Convert.ToUInt32(value);
        }
      }
    }

    private uint _triesKillerBallsOften = 1234;
    public bool TriesKillerBallsOften {
      get {
        if (_triesKillerBallsOften == 1234) {
          _triesKillerBallsOften = MovesFlagsOne & (uint)PreferredMovesFirst.PMFTriesKillerBallsOften;
        }

        return _triesKillerBallsOften > 0;
      }
      set {
        if (_triesKillerBallsOften != Convert.ToUInt32(value)) {
          isDirty = true;
          _triesKillerBallsOften = Convert.ToUInt32(value);
        }
      }
    }

    private uint _shootsFromDistance = 1234;
    public bool ShootsFromDistance {
      get {
        if (_shootsFromDistance == 1234) {
          _shootsFromDistance = MovesFlagsOne & (uint)PreferredMovesFirst.PMFShootsFromDistance;
        }

        return _shootsFromDistance > 0;
      }
      set {
        if (_shootsFromDistance != Convert.ToUInt32(value)) {
          isDirty = true;
          _shootsFromDistance = Convert.ToUInt32(value);
        }
      }
    }

    private uint _shootsWithPower = 1234;
    public bool ShootsWithPower {
      get {
        if (_shootsWithPower == 1234) {
          _shootsWithPower = MovesFlagsOne & (uint)PreferredMovesFirst.PMFShootsWithPower;
        }

        return _shootsWithPower > 0;
      }
      set {
        if (_shootsWithPower != Convert.ToUInt32(value)) {
          isDirty = true;
          _shootsWithPower = Convert.ToUInt32(value);
        }
      }
    }

    private uint _placesShots = 1234;
    public bool PlacesShots {
      get {
        if (_placesShots == 1234) {
          _placesShots = MovesFlagsOne & (uint)PreferredMovesFirst.PMFPlacesShots;
        }

        return _placesShots > 0;
      }
      set {
        if (_placesShots != Convert.ToUInt32(value)) {
          isDirty = true;
          _placesShots = Convert.ToUInt32(value);
        }
      }
    }

    private uint _curlsBall = 1234;
    public bool CurlsBall {
      get {
        if (_curlsBall == 1234) {
          _curlsBall = MovesFlagsOne & (uint)PreferredMovesFirst.PMFCurlsBall;
        }

        return _curlsBall > 0;
      }
      set {
        if (_curlsBall != Convert.ToUInt32(value)) {
          isDirty = true;
          _curlsBall = Convert.ToUInt32(value);
        }
      }
    }

    private uint _likesToRoundKeeper = 1234;
    public bool LikesToRoundKeeper {
      get {
        if (_likesToRoundKeeper == 1234) {
          _likesToRoundKeeper = MovesFlagsOne & (uint)PreferredMovesFirst.PMFLikesToRoundKeeper;
        }

        return _likesToRoundKeeper > 0;
      }
      set {
        if (_likesToRoundKeeper != Convert.ToUInt32(value)) {
          isDirty = true;
          _likesToRoundKeeper = Convert.ToUInt32(value);
        }
      }
    }

    private uint _likesToTryToBeatOffsideTrap = 1234;
    public bool LikesToTryToBeatOffsideTrap {
      get {
        if (_likesToTryToBeatOffsideTrap == 1234) {
          _likesToTryToBeatOffsideTrap = MovesFlagsOne & (uint)PreferredMovesFirst.PMFLikesToTryToBeatOffsideTrap;
        }

        return _likesToTryToBeatOffsideTrap > 0;
      }
      set {
        if (_likesToTryToBeatOffsideTrap != Convert.ToUInt32(value)) {
          isDirty = true;
          _likesToTryToBeatOffsideTrap = Convert.ToUInt32(value);
        }
      }
    }

    private uint _usesOutsideOfFoot = 1234;
    public bool UsesOutsideOfFoot {
      get {
        if (_usesOutsideOfFoot == 1234) {
          _usesOutsideOfFoot = MovesFlagsOne & (uint)PreferredMovesFirst.PMFUsesOutsideOfFoot;
        }

        return _usesOutsideOfFoot > 0;
      }
      set {
        if (_usesOutsideOfFoot != Convert.ToUInt32(value)) {
          isDirty = true;
          _usesOutsideOfFoot = Convert.ToUInt32(value);
        }
      }
    }

    private uint _marksOpponentTightly = 1234;
    public bool MarksOpponentTightly {
      get {
        if (_marksOpponentTightly == 1234) {
          _marksOpponentTightly = MovesFlagsOne & (uint)PreferredMovesFirst.PMFMarksOpponentTightly;
        }

        return _marksOpponentTightly > 0;
      }
      set {
        if (_marksOpponentTightly != Convert.ToUInt32(value)) {
          isDirty = true;
          _marksOpponentTightly = Convert.ToUInt32(value);
        }
      }
    }

    private uint _windsUpOpponents = 1234;
    public bool WindsUpOpponents {
      get {
        if (_windsUpOpponents == 1234) {
          _windsUpOpponents = MovesFlagsOne & (uint)PreferredMovesFirst.PMFWindsUpOpponents;
        }

        return _windsUpOpponents > 0;
      }
      set {
        if (_windsUpOpponents != Convert.ToUInt32(value)) {
          isDirty = true;
          _windsUpOpponents = Convert.ToUInt32(value);
        }
      }
    }

    private uint _arguesWithOfficials = 1234;
    public bool ArguesWithOfficials {
      get {
        if (_arguesWithOfficials == 1234) {
          _arguesWithOfficials = MovesFlagsOne & (uint)PreferredMovesFirst.PMFArguesWithOfficials;
        }

        return _arguesWithOfficials > 0;
      }
      set {
        if (_arguesWithOfficials != Convert.ToUInt32(value)) {
          isDirty = true;
          _arguesWithOfficials = Convert.ToUInt32(value);
        }
      }
    }

    private uint _playsWithBackToGoal = 1234;
    public bool PlaysWithBackToGoal {
      get {
        if (_playsWithBackToGoal == 1234) {
          _playsWithBackToGoal = MovesFlagsOne & (uint)PreferredMovesFirst.PMFPlaysWithBackToGoal;
        }

        return _playsWithBackToGoal > 0;
      }
      set {
        if (_playsWithBackToGoal != Convert.ToUInt32(value)) {
          isDirty = true;
          _playsWithBackToGoal = Convert.ToUInt32(value);
        }
      }
    }

    private uint _comesDeepToGetBall = 1234;
    public bool ComesDeepToGetBall {
      get {
        if (_comesDeepToGetBall == 1234) {
          _comesDeepToGetBall = MovesFlagsOne & (uint)PreferredMovesFirst.PMFComesDeepToGetBall;
        }

        return _comesDeepToGetBall > 0;
      }
      set {
        if (_comesDeepToGetBall != Convert.ToUInt32(value)) {
          isDirty = true;
          _comesDeepToGetBall = Convert.ToUInt32(value);
        }
      }
    }

    private uint _playsOneTwos = 1234;
    public bool PlaysOneTwos {
      get {
        if (_playsOneTwos == 1234) {
          _playsOneTwos = MovesFlagsOne & (uint)PreferredMovesFirst.PMFPlaysOneTwos;
        }

        return _playsOneTwos > 0;
      }
      set {
        if (_playsOneTwos != Convert.ToUInt32(value)) {
          isDirty = true;
          _playsOneTwos = Convert.ToUInt32(value);
        }
      }
    }

    private uint _likesToLobKeeper = 1234;
    public bool LikesToLobKeeper {
      get {
        if (_likesToLobKeeper == 1234) {
          _likesToLobKeeper = MovesFlagsOne & (uint)PreferredMovesFirst.PMFLikesToLobKeeper;
        }

        return _likesToLobKeeper > 0;
      }
      set {
        if (_likesToLobKeeper != Convert.ToUInt32(value)) {
          isDirty = true;
          _likesToLobKeeper = Convert.ToUInt32(value);
        }
      }
    }

    private uint _dictatesTempo = 1234;
    public bool DictatesTempo {
      get {
        if (_dictatesTempo == 1234) {
          _dictatesTempo = MovesFlagsOne & (uint)PreferredMovesFirst.PMFDictatesTempo;
        }

        return _dictatesTempo > 0;
      }
      set {
        if (_dictatesTempo != Convert.ToUInt32(value)) {
          isDirty = true;
          _dictatesTempo = Convert.ToUInt32(value);
        }
      }
    }

    private uint _attemptsOverheadKicks = 1234;
    public bool AttemptsOverheadKicks {
      get {
        if (_attemptsOverheadKicks == 1234) {
          _attemptsOverheadKicks = MovesFlagsOne & (uint)PreferredMovesFirst.PMFAttemptsOverheadKicks;
        }

        return _attemptsOverheadKicks > 0;
      }
      set {
        if (_attemptsOverheadKicks != Convert.ToUInt32(value)) {
          isDirty = true;
          _attemptsOverheadKicks = Convert.ToUInt32(value);
        }
      }
    }

    private uint _looksForPassNotShot = 1234;
    public bool LooksForPassNotShot {
      get {
        if (_looksForPassNotShot == 1234) {
          _looksForPassNotShot = MovesFlagsOne & (uint)PreferredMovesFirst.PMFLooksForPassNotShot;
        }

        return _looksForPassNotShot > 0;
      }
      set {
        if (_looksForPassNotShot != Convert.ToUInt32(value)) {
          isDirty = true;
          _looksForPassNotShot = Convert.ToUInt32(value);
        }
      }
    }

    private uint _playsNoThroughBalls = 1234;
    public bool PlaysNoThroughBalls {
      get {
        if (_playsNoThroughBalls == 1234) {
          _playsNoThroughBalls = MovesFlagsOne & (uint)PreferredMovesFirst.PMFPlaysNoThroughBalls;
        }

        return _playsNoThroughBalls > 0;
      }
      set {
        if (_playsNoThroughBalls != Convert.ToUInt32(value)) {
          isDirty = true;
          _playsNoThroughBalls = Convert.ToUInt32(value);
        }
      }
    }

    private uint _stopsPlay = 1234;
    public bool StopsPlay {
      get {
        if (_stopsPlay == 1234) {
          _stopsPlay = MovesFlagsOne & (uint)PreferredMovesFirst.PMFStopsPlay;
        }

        return _stopsPlay > 0;
      }
      set {
        if (_stopsPlay != Convert.ToUInt32(value)) {
          isDirty = true;
          _stopsPlay = Convert.ToUInt32(value);
        }
      }
    }

    private uint _knocksBallPastOpponent = 1234;
    public bool KnocksBallPastOpponent {
      get {
        if (_knocksBallPastOpponent == 1234) {
          _knocksBallPastOpponent = MovesFlagsOne & (uint)PreferredMovesFirst.PMFKnocksBallPastOpponent;
        }

        return _knocksBallPastOpponent > 0;
      }
      set {
        if (_knocksBallPastOpponent != Convert.ToUInt32(value)) {
          isDirty = true;
          _knocksBallPastOpponent = Convert.ToUInt32(value);
        }
      }
    }

    private uint _movesBallToRightFootBeforeDribbleAttempt = 1234;
    public bool MovesBallToRightFootBeforeDribbleAttempt {
      get {
        if (_movesBallToRightFootBeforeDribbleAttempt == 1234) {
          _movesBallToRightFootBeforeDribbleAttempt = MovesFlagsOne & (uint)PreferredMovesFirst.PMFMovesBallToRightFootBeforeDribbleAttempt;
        }

        return _movesBallToRightFootBeforeDribbleAttempt > 0;
      }
      set {
        if (_movesBallToRightFootBeforeDribbleAttempt != Convert.ToUInt32(value)) {
          isDirty = true;
          _movesBallToRightFootBeforeDribbleAttempt = Convert.ToUInt32(value);
        }
      }
    }

    private uint _movesBallToLeftFootBeforeDribbleAttempt = 1234;
    public bool MovesBallToLeftFootBeforeDribbleAttempt {
      get {
        if (_movesBallToLeftFootBeforeDribbleAttempt == 1234) {
          _movesBallToLeftFootBeforeDribbleAttempt = MovesFlagsOne & (uint)PreferredMovesFirst.PMFMovesBallToLeftFootBeforeDribbleAttempt;
        }

        return _movesBallToLeftFootBeforeDribbleAttempt > 0;
      }
      set {
        if (_movesBallToLeftFootBeforeDribbleAttempt != Convert.ToUInt32(value)) {
          isDirty = true;
          _movesBallToLeftFootBeforeDribbleAttempt = Convert.ToUInt32(value);
        }
      }
    }

    private uint _dwellsOnBall = 1234;
    public bool DwellsOnBall {
      get {
        if (_dwellsOnBall == 1234) {
          _dwellsOnBall = MovesFlagsOne & (uint)PreferredMovesFirst.PMFDwellsOnBall;
        }

        return _dwellsOnBall > 0;
      }
      set {
        if (_dwellsOnBall != Convert.ToUInt32(value)) {
          isDirty = true;
          _dwellsOnBall = Convert.ToUInt32(value);
        }
      }
    }

    private uint _arrivesLateInOpponentsArea = 1234;
    public bool ArrivesLateInOpponentsArea {
      get {
        if (_arrivesLateInOpponentsArea == 1234) {
          _arrivesLateInOpponentsArea = MovesFlagsOne & (uint)PreferredMovesFirst.PMFArrivesLateInOpponentsArea;
        }

        return _arrivesLateInOpponentsArea > 0;
      }
      set {
        if (_arrivesLateInOpponentsArea != Convert.ToUInt32(value)) {
          isDirty = true;
          _arrivesLateInOpponentsArea = Convert.ToUInt32(value);
        }
      }
    }

    private uint _triesToPlayWayOutOfTrouble = 1234;
    public bool TriesToPlayWayOutOfTrouble {
      get {
        if (_triesToPlayWayOutOfTrouble == 1234) {
          _triesToPlayWayOutOfTrouble = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSTriesToPlayWayOutOfTrouble;
        }

        return _triesToPlayWayOutOfTrouble > 0;
      }
      set {
        if (_triesToPlayWayOutOfTrouble != Convert.ToUInt32(value)) {
          isDirty = true;
          _triesToPlayWayOutOfTrouble = Convert.ToUInt32(value);
        }
      }
    }

    private uint _staysBackAtAllTimes = 1234;
    public bool StaysBackAtAllTimes {
      get {
        if (_staysBackAtAllTimes == 1234) {
          _staysBackAtAllTimes = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSStaysBackAtAllTimes;
        }

        return _staysBackAtAllTimes > 0;
      }
      set {
        if (_staysBackAtAllTimes != Convert.ToUInt32(value)) {
          isDirty = true;
          _staysBackAtAllTimes = Convert.ToUInt32(value);
        }
      }
    }

    private uint _avoidsUsingWeakerFoot = 1234;
    public bool AvoidsUsingWeakerFoot {
      get {
        if (_avoidsUsingWeakerFoot == 1234) {
          _avoidsUsingWeakerFoot = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSAvoidsUsingWeakerFoot;
        }

        return _avoidsUsingWeakerFoot > 0;
      }
      set {
        if (_avoidsUsingWeakerFoot != Convert.ToUInt32(value)) {
          isDirty = true;
          _avoidsUsingWeakerFoot = Convert.ToUInt32(value);
        }
      }
    }

    private uint _triesTricks = 1234;
    public bool TriesTricks {
      get {
        if (_triesTricks == 1234) {
          _triesTricks = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSTriesTricks;
        }

        return _triesTricks > 0;
      }
      set {
        if (_triesTricks != Convert.ToUInt32(value)) {
          isDirty = true;
          _triesTricks = Convert.ToUInt32(value);
        }
      }
    }

    private uint _triesLongRangeFreeKicks = 1234;
    public bool TriesLongRangeFreeKicks {
      get {
        if (_triesLongRangeFreeKicks == 1234) {
          _triesLongRangeFreeKicks = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSTriesLongRangeFreeKicks;
        }

        return _triesLongRangeFreeKicks > 0;
      }
      set {
        if (_triesLongRangeFreeKicks != Convert.ToUInt32(value)) {
          isDirty = true;
          _triesLongRangeFreeKicks = Convert.ToUInt32(value);
        }
      }
    }

    private uint _divesIntoTackles = 1234;
    public bool DivesIntoTackles {
      get {
        if (_divesIntoTackles == 1234) {
          _divesIntoTackles = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSDivesIntoTackles;
        }

        return _divesIntoTackles > 0;
      }
      set {
        if (_divesIntoTackles != Convert.ToUInt32(value)) {
          isDirty = true;
          _divesIntoTackles = Convert.ToUInt32(value);
        }
      }
    }

    private uint _doesNotDiveIntoTackles = 1234;
    public bool DoesNotDiveIntoTackles {
      get {
        if (_doesNotDiveIntoTackles == 1234) {
          _doesNotDiveIntoTackles = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSDoesNotDiveIntoTackles;
        }

        return _doesNotDiveIntoTackles > 0;
      }
      set {
        if (_doesNotDiveIntoTackles != Convert.ToUInt32(value)) {
          isDirty = true;
          _doesNotDiveIntoTackles = Convert.ToUInt32(value);
        }
      }
    }

    private uint _cutsInsideFromBothWings = 1234;
    public bool CutsInsideFromBothWings {
      get {
        if (_cutsInsideFromBothWings == 1234) {
          _cutsInsideFromBothWings = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSCutsInsideFromBothWings;
        }

        return _cutsInsideFromBothWings > 0;
      }
      set {
        if (_cutsInsideFromBothWings != Convert.ToUInt32(value)) {
          isDirty = true;
          _cutsInsideFromBothWings = Convert.ToUInt32(value);
        }
      }
    }

    private uint _hugsLine = 1234;
    public bool HugsLine {
      get {
        if (_hugsLine == 1234) {
          _hugsLine = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSHugsLine;
        }

        return _hugsLine > 0;
      }
      set {
        if (_hugsLine != Convert.ToUInt32(value)) {
          isDirty = true;
          _hugsLine = Convert.ToUInt32(value);
        }
      }
    }

    private uint _getsCrowdGoing = 1234;
    public bool GetsCrowdGoing {
      get {
        if (_getsCrowdGoing == 1234) {
          _getsCrowdGoing = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSGetsCrowdGoing;
        }

        return _getsCrowdGoing > 0;
      }
      set {
        if (_getsCrowdGoing != Convert.ToUInt32(value)) {
          isDirty = true;
          _getsCrowdGoing = Convert.ToUInt32(value);
        }
      }
    }

    private uint _triesFirstTimeShots = 1234;
    public bool TriesFirstTimeShots {
      get {
        if (_triesFirstTimeShots == 1234) {
          _triesFirstTimeShots = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSTriesFirstTimeShots;
        }

        return _triesFirstTimeShots > 0;
      }
      set {
        if (_triesFirstTimeShots != Convert.ToUInt32(value)) {
          isDirty = true;
          _triesFirstTimeShots = Convert.ToUInt32(value);
        }
      }
    }

    private uint _triesLongRangePasses = 1234;
    public bool TriesLongRangePasses {
      get {
        if (_triesLongRangePasses == 1234) {
          _triesLongRangePasses = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSTriesLongRangePasses;
        }

        return _triesLongRangePasses > 0;
      }
      set {
        if (_triesLongRangePasses != Convert.ToUInt32(value)) {
          isDirty = true;
          _triesLongRangePasses = Convert.ToUInt32(value);
        }
      }
    }

    private uint _likesBallPlayedIntoFeet = 1234;
    public bool LikesBallPlayedIntoFeet {
      get {
        if (_likesBallPlayedIntoFeet == 1234) {
          _likesBallPlayedIntoFeet = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSLikesBallPlayedIntoFeet;
        }

        return _likesBallPlayedIntoFeet > 0;
      }
      set {
        if (_likesBallPlayedIntoFeet != Convert.ToUInt32(value)) {
          isDirty = true;
          _likesBallPlayedIntoFeet = Convert.ToUInt32(value);
        }
      }
    }

    private uint _hitsFreeKickWithPower = 1234;
    public bool HitsFreeKickWithPower {
      get {
        if (_hitsFreeKickWithPower == 1234) {
          _hitsFreeKickWithPower = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSHitsFreeKickWithPower;
        }

        return _hitsFreeKickWithPower > 0;
      }
      set {
        if (_hitsFreeKickWithPower != Convert.ToUInt32(value)) {
          isDirty = true;
          _hitsFreeKickWithPower = Convert.ToUInt32(value);
        }
      }
    }

    private uint _likesToBeatManRepeatedly = 1234;
    public bool LikesToBeatManRepeatedly {
      get {
        if (_likesToBeatManRepeatedly == 1234) {
          _likesToBeatManRepeatedly = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSLikesToBeatManRepeatedly;
        }

        return _likesToBeatManRepeatedly > 0;
      }
      set {
        if (_likesToBeatManRepeatedly != Convert.ToUInt32(value)) {
          isDirty = true;
          _likesToBeatManRepeatedly = Convert.ToUInt32(value);
        }
      }
    }

    private uint _likesToSwitchBallToOtherFlank = 1234;
    public bool LikesToSwitchBallToOtherFlank {
      get {
        if (_likesToSwitchBallToOtherFlank == 1234) {
          _likesToSwitchBallToOtherFlank = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSLikesToSwitchBallToOtherFlank;
        }

        return _likesToSwitchBallToOtherFlank > 0;
      }
      set {
        if (_likesToSwitchBallToOtherFlank != Convert.ToUInt32(value)) {
          isDirty = true;
          _likesToSwitchBallToOtherFlank = Convert.ToUInt32(value);
        }
      }
    }

    private uint _hasLongFlatBulletThrow = 1234;
    public bool HasLongFlatBulletThrow {
      get {
        if (_hasLongFlatBulletThrow == 1234) {
          _hasLongFlatBulletThrow = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSHasLongFlatBulletThrow;
        }

        return _hasLongFlatBulletThrow > 0;
      }
      set {
        if (_hasLongFlatBulletThrow != Convert.ToUInt32(value)) {
          isDirty = true;
          _hasLongFlatBulletThrow = Convert.ToUInt32(value);
        }
      }
    }

    private uint _runsWithBallOften = 1234;
    public bool RunsWithBallOften {
      get {
        if (_runsWithBallOften == 1234) {
          _runsWithBallOften = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSRunsWithBallOften;
        }

        return _runsWithBallOften > 0;
      }
      set {
        if (_runsWithBallOften != Convert.ToUInt32(value)) {
          isDirty = true;
          _runsWithBallOften = Convert.ToUInt32(value);
        }
      }
    }

    private uint _willRetireAtTop = 1234;
    public bool WillRetireAtTop {
      get {
        if (_willRetireAtTop == 1234) {
          _willRetireAtTop = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSWillRetireAtTop;
        }

        return _willRetireAtTop > 0;
      }
      set {
        if (_willRetireAtTop != Convert.ToUInt32(value)) {
          isDirty = true;
          _willRetireAtTop = Convert.ToUInt32(value);
        }
      }
    }

    private uint _willPlayFootballAsLongAsPossible = 1234;
    public bool WillPlayFootballAsLongAsPossible {
      get {
        if (_willPlayFootballAsLongAsPossible == 1234) {
          _willPlayFootballAsLongAsPossible = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSWillPlayFootballAsLongAsPossible;
        }

        return _willPlayFootballAsLongAsPossible > 0;
      }
      set {
        if (_willPlayFootballAsLongAsPossible != Convert.ToUInt32(value)) {
          isDirty = true;
          _willPlayFootballAsLongAsPossible = Convert.ToUInt32(value);
        }
      }
    }

    private uint _runsWithBallRarely = 1234;
    public bool RunsWithBallRarely {
      get {
        if (_runsWithBallRarely == 1234) {
          _runsWithBallRarely = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSRunsWithBallRarely;
        }

        return _runsWithBallRarely > 0;
      }
      set {
        if (_runsWithBallRarely != Convert.ToUInt32(value)) {
          isDirty = true;
          _runsWithBallRarely = Convert.ToUInt32(value);
        }
      }
    }

    private uint _boxPlayer = 1234;
    public bool BoxPlayer {
      get {
        if (_boxPlayer == 1234) {
          _boxPlayer = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSBoxPlayer;
        }

        return _boxPlayer > 0;
      }
      set {
        if (_boxPlayer != Convert.ToUInt32(value)) {
          isDirty = true;
          _boxPlayer = Convert.ToUInt32(value);
        }
      }
    }

    private uint _usesLongThrowsToStartCounterAttacks = 1234;
    public bool UsesLongThrowsToStartCounterAttacks {
      get {
        if (_usesLongThrowsToStartCounterAttacks == 1234) {
          _usesLongThrowsToStartCounterAttacks = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSUsesLongThrowsToStartCounterAttacks;
        }

        return _usesLongThrowsToStartCounterAttacks > 0;
      }
      set {
        if (_usesLongThrowsToStartCounterAttacks != Convert.ToUInt32(value)) {
          isDirty = true;
          _usesLongThrowsToStartCounterAttacks = Convert.ToUInt32(value);
        }
      }
    }

    private uint _refrainsFromTakingLongShots = 1234;
    public bool RefrainsFromTakingLongShots {
      get {
        if (_refrainsFromTakingLongShots == 1234) {
          _refrainsFromTakingLongShots = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSRefrainsFromTakingLongShots;
        }

        return _refrainsFromTakingLongShots > 0;
      }
      set {
        if (_refrainsFromTakingLongShots != Convert.ToUInt32(value)) {
          isDirty = true;
          _refrainsFromTakingLongShots = Convert.ToUInt32(value);
        }
      }
    }

    private uint _cutsInsideFromLeftWing = 1234;
    public bool CutsInsideFromLeftWing {
      get {
        if (_cutsInsideFromLeftWing == 1234) {
          _cutsInsideFromLeftWing = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSCutsInsideFromLeftWing;
        }

        return _cutsInsideFromLeftWing > 0;
      }
      set {
        if (_cutsInsideFromLeftWing != Convert.ToUInt32(value)) {
          isDirty = true;
          _cutsInsideFromLeftWing = Convert.ToUInt32(value);
        }
      }
    }

    private uint _cutsInsideFromRightWing = 1234;
    public bool CutsInsideFromRightWing {
      get {
        if (_cutsInsideFromRightWing == 1234) {
          _cutsInsideFromRightWing = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSCutsInsideFromRightWing;
        }

        return _cutsInsideFromRightWing > 0;
      }
      set {
        if (_cutsInsideFromRightWing != Convert.ToUInt32(value)) {
          isDirty = true;
          _cutsInsideFromRightWing = Convert.ToUInt32(value);
        }
      }
    }

    private uint _crossesEarly = 1234;
    public bool CrossesEarly {
      get {
        if (_crossesEarly == 1234) {
          _crossesEarly = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSCrossesEarly;
        }

        return _crossesEarly > 0;
      }
      set {
        if (_crossesEarly != Convert.ToUInt32(value)) {
          isDirty = true;
          _crossesEarly = Convert.ToUInt32(value);
        }
      }
    }

    private uint _bringsBallOutOfDefense = 1234;
    public bool BringsBallOutOfDefense {
      get {
        if (_bringsBallOutOfDefense == 1234) {
          _bringsBallOutOfDefense = MovesFlagsTwo & (uint)PreferredMovesSecond.PMSBringsBallOutOfDefense;
        }

        return _bringsBallOutOfDefense > 0;
      }
      set {
        if (_bringsBallOutOfDefense != Convert.ToUInt32(value)) {
          isDirty = true;
          _bringsBallOutOfDefense = Convert.ToUInt32(value);
        }
      }
    }
  }
}
