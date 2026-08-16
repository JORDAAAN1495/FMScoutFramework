namespace FM.Pitchside.Core.Entities.Ingame.Persons.Sub_Entities;

public class RoleRatings
{
    private Player CurrentPlayer { get; set; }

    public RoleRatings() { }

    public RoleRatings(Player player)
    {
        CurrentPlayer = player;
    }

    private string _preferredPosition;

    public string PreferredPosition
    {
        get
        {
            _preferredPosition ??= GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(float))
                .Select(p => new
                {
                    Description = p.GetCustomAttribute<DescriptionAttribute>()?.Description ?? p.Name,
                    Rating = (float)p.GetValue(this)
                })
                .OrderByDescending(x => x.Rating)
                .First()
                .Description;

            return _preferredPosition;
        }
    }

    #region Goalkeepers

    [Description("Goalkeeper (Defend)")]
    public float GoalkeeperDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();
            // Primary
            Weights.AerialReach = 1.0f;
            Weights.CommandOfArea = 1.0f;
            Weights.Communication = 1.0f;
            Weights.Handling = 1.0f;
            Weights.Kicking = 1.0f;
            Weights.Reflexes = 1.0f;
            Weights.Positioning = 1.0f;
            Weights.Agility = 1.0f;

            // Physical Secondaries
            // - None

            // Mental Secondaries
            Weights.Anticipation = 0.6f;
            Weights.Decisions = 0.6f;

            // Technical Secondaries
            Weights.FirstTouch = 0.5f;
            Weights.OneOnOnes = 0.5f;
            Weights.Throwing = 0.5f;

            return FinalRating(Weights, CurrentPlayer.Attributes.Goalkeeper);
        }
    }

    [Description("Sweeper Keeper (Defend)")]
    public float SweeperKeeperDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();
            // Primary
            Weights.AerialReach = 1.0f;
            Weights.CommandOfArea = 1.0f;
            Weights.FirstTouch = 1.0f;
            Weights.Handling = 1.0f;
            Weights.Kicking = 1.0f;
            Weights.Passing = 1.0f;
            Weights.Reflexes = 1.0f;
            Weights.Anticipation = 1.0f;
            Weights.Composure = 1.0f;
            Weights.Positioning = 1.0f;
            Weights.Agility = 1.0f;

            // Physical Secondaries
            // - None

            // Mental Secondaries
            Weights.Concentration = 0.6f;
            Weights.Decisions = 0.6f;

            // Technical Secondaries
            Weights.OneOnOnes = 0.5f;
            Weights.Throwing = 0.5f;

            return FinalRating(Weights, CurrentPlayer.Attributes.Goalkeeper);
        }
    }

    [Description("Sweeper Keeper (Support)")]
    public float SweeperKeeperSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();

            // Primary
            Weights.AerialReach = 1.0f;
            Weights.CommandOfArea = 1.0f;
            Weights.FirstTouch = 1.0f;
            Weights.Handling = 1.0f;
            Weights.Kicking = 1.0f;
            Weights.Passing = 1.0f;
            Weights.Reflexes = 1.0f;
            Weights.Anticipation = 1.0f;
            Weights.Positioning = 1.0f;
            Weights.Agility = 1.0f;

            // Physical Secondaries
            Weights.Acceleration = 0.7f;

            // Mental Secondaries
            Weights.Composure = 0.6f;
            Weights.Concentration = 0.6f;
            Weights.Decisions = 0.6f;

            // Technical Secondaries
            Weights.OneOnOnes = 0.5f;
            Weights.RushingOut = 0.5f;
            Weights.Throwing = 0.5f;

            return FinalRating(Weights, CurrentPlayer.Attributes.Goalkeeper);
        }
    }

    [Description("Sweeper Keeper (Attack)")]
    public float SweeperKeeperAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();

            // Primary
            Weights.AerialReach = 1.0f;
            Weights.CommandOfArea = 1.0f;
            Weights.FirstTouch = 1.0f;
            Weights.Handling = 1.0f;
            Weights.Kicking = 1.0f;
            Weights.Passing = 1.0f;
            Weights.Reflexes = 1.0f;
            Weights.RushingOut = 1.0f;
            Weights.Anticipation = 1.0f;
            Weights.Composure = 1.0f;
            Weights.Positioning = 1.0f;
            Weights.Acceleration = 1.0f;
            Weights.Agility = 1.0f;

            // Physical Secondaries
            // - None

            // Mental Secondaries
            Weights.Concentration = 0.6f;

            // Technical Secondaries
            Weights.Eccentricity = 0.5f;
            Weights.OneOnOnes = 0.5f;
            Weights.Throwing = 0.5f;

            return FinalRating(Weights, CurrentPlayer.Attributes.Goalkeeper);
        }
    }

    #endregion

    #region Defenders

    [Description("Ball Playing Defender (Defend)")]
    public float BallPlayingDefenderDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();

            // Primary
            Weights.Heading = 1.0f;
            Weights.Marking = 1.0f;
            Weights.Passing = 1.0f;
            Weights.Tackling = 1.0f;
            Weights.Positioning = 1.0f;
            Weights.Jumping = 1.0f;

            // Physical Secondaries
            Weights.Strength = 0.7f;

            // Mental Secondaries
            Weights.Aggression = 0.6f;
            Weights.Anticipation = 0.6f;
            Weights.Bravery = 0.6f;
            Weights.Composure = 0.6f;
            Weights.Concentration = 0.6f;
            Weights.Decisions = 0.6f;
            Weights.Determination = 0.6f;
            Weights.Vision = 0.6f;

            // Technical Secondaries
            Weights.FirstTouch = 0.5f;
            Weights.Technique = 0.5f;

            return FinalRating(Weights, CurrentPlayer.Attributes.DefenderCenter);
        }
    }

    [Description("Ball Playing Defender (Stopper)")]
    public float BallPlayingDefenderStopper
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();

            // Primary
            Weights.Heading = 1.0f;
            Weights.Passing = 1.0f;
            Weights.Tackling = 1.0f;
            Weights.Aggression = 1.0f;
            Weights.Bravery = 1.0f;
            Weights.Decisions = 1.0f;
            Weights.Determination = 1.0f;
            Weights.Jumping = 1.0f;

            // Physical Secondaries
            Weights.Strength = 0.7f;

            // Mental Secondaries
            Weights.Anticipation = 0.6f;
            Weights.Composure = 0.6f;
            Weights.Concentration = 0.6f;
            Weights.Positioning = 0.6f;
            Weights.Vision = 0.6f;

            // Technical Secondaries
            Weights.FirstTouch = 0.5f;
            Weights.Marking = 0.5f;
            Weights.Technique = 0.5f;

            return FinalRating(Weights, CurrentPlayer.Attributes.DefenderCenter);
        }
    }

    [Description("Ball Playing Defender (Cover)")]
    public float BallPlayingDefenderCover
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();

            // Primary
            Weights.Heading = 1.0f;
            Weights.Passing = 1.0f;
            Weights.Tackling = 1.0f;
            Weights.Anticipation = 1.0f;
            Weights.Concentration = 1.0f;
            Weights.Decisions = 1.0f;
            Weights.Positioning = 1.0f;
            Weights.Acceleration = 1.0f;
            Weights.Jumping = 1.0f;

            // Physical Secondaries
            Weights.Strength = 0.7f;

            // Mental Secondaries
            Weights.Bravery = 0.6f;
            Weights.Composure = 0.6f;
            Weights.Determination = 0.6f;
            Weights.Vision = 0.6f;

            // Techical Secondaries
            Weights.FirstTouch = 0.5f;
            Weights.Marking = 0.5f;
            Weights.Technique = 0.5f;

            return FinalRating(Weights, CurrentPlayer.Attributes.DefenderCenter);
        }
    }

    [Description("Central Defender (Defend)")]
    public float CentralDefenderDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();

            // Primary
            Weights.Heading = 1.0f;
            Weights.Marking = 1.0f;
            Weights.Tackling = 1.0f;
            Weights.Positioning = 1.0f;
            Weights.Jumping = 1.0f;

            // Physical Secondaries
            Weights.Strength = 0.7f;

            // Mental Secondaries
            Weights.Aggression = 0.6f;
            Weights.Anticipation = 0.6f;
            Weights.Bravery = 0.6f;
            Weights.Concentration = 0.6f;
            Weights.Decisions = 0.6f;
            Weights.Determination = 0.6f;

            // Technical Secondaries
            // - None

            return FinalRating(Weights, CurrentPlayer.Attributes.DefenderCenter);
        }
    }

    [Description("Central Defender (Stopper)")]
    public float CentralDefenderStopper
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();

            // Primary
            Weights.Heading = 1.0f;
            Weights.Tackling = 1.0f;
            Weights.Aggression = 1.0f;
            Weights.Bravery = 1.0f;
            Weights.Decisions = 1.0f;
            Weights.Determination = 1.0f;
            Weights.Jumping = 1.0f;

            // Physical Secondaries
            Weights.Strength = 0.7f;

            // Mental Secondaries
            Weights.Anticipation = 0.6f;
            Weights.Concentration = 0.6f;
            Weights.Positioning = 0.6f;

            // Technical Secondaries
            Weights.Marking = 0.5f;

            return FinalRating(Weights, CurrentPlayer.Attributes.DefenderCenter);
        }
    }

    [Description("Central Defender (Cover)")]
    public float CentralDefenderCover
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();

            // Primary
            Weights.Heading = 1.0f;
            Weights.Tackling = 1.0f;
            Weights.Anticipation = 1.0f;
            Weights.Concentration = 1.0f;
            Weights.Decisions = 1.0f;
            Weights.Positioning = 1.0f;
            Weights.Acceleration = 1.0f;
            Weights.Jumping = 1.0f;

            // Physical Secondaries
            Weights.Strength = 0.7f;

            // Mental Secondaries
            Weights.Bravery = 0.6f;
            Weights.Determination = 0.6f;

            // Techical Secondaries
            Weights.Marking = 0.5f;

            return FinalRating(Weights, CurrentPlayer.Attributes.DefenderCenter);
        }
    }

    [Description("Complete Wing Back (Support)")]
    public float CompleteWingBackSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights();

            // Primary
            Weights.Crossing = 1.0f;
            Weights.Dribbling = 1.0f;
            Weights.FirstTouch = 1.0f;
            Weights.Passing = 1.0f;
            Weights.Tackling = 1.0f;
            Weights.Decisions = 1.0f;
            Weights.OffTheBall = 1.0f;
            Weights.Positioning = 1.0f;
            Weights.Teamwork = 1.0f;
            Weights.WorkRate = 1.0f;
            Weights.Acceleration = 1.0f;
            Weights.Pace = 1.0f;
            Weights.Stamina = 1.0f;

            // Physical Secondaries
            Weights.Agility = 0.7f;
            Weights.Balance = 0.7f;

            // Mental Secondaries
            Weights.Anticipation = 0.6f;
            Weights.Composure = 0.6f;
            Weights.Concentration = 0.6f;
            Weights.Flair = 0.6f;

            // Technical Secondaries
            Weights.Marking = 0.5f;
            Weights.Technique = 0.5f;

            return FinalRating(Weights, this.BestWingSide());
        }
    }

    [Description("Complete Wing Back (Attack)")]
    public float CompleteWingBackAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Crossing = 1.0f,
                Dribbling = 1.0f,
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Tackling = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Pace = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Agility = 0.7f,
                Balance = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Composure = 0.6f,
                Concentration = 0.6f,
                Flair = 0.6f,

                // Technical Secondaries
                Marking = 0.5f,
                Technique = 0.5f
            };

            return FinalRating(Weights, this.BestWingSide());
        }
    }

    [Description("Defensive Centre Back (Defend)")]
    public float DefensiveCentreBackDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Heading = 1.0f,
                Tackling = 1.0f,
                Jumping = 1.0f,

                // Physical Secondaries
                Strength = 0.7f,

                // Mental Secondaries
                Determination = 0.6f,
                Positioning = 0.6f,

                // Technical Secondaries
                Marking = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.DefenderCenter);
        }
    }

    [Description("Defensive Centre Back (Stopper)")]
    public float DefensiveCentreBackStopper
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Heading = 1.0f,
                Tackling = 1.0f,
                Jumping = 1.0f,

                // Physical Secondaries
                Strength = 0.7f,

                // Mental Secondaries
                Determination = 0.6f,
                Positioning = 0.6f,

                // Technical Secondaries
                Marking = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.DefenderCenter);
        }
    }

    [Description("Defensive Centre Back (Cover)")]
    public float DefensiveCentreBackCover
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Heading = 1.0f,
                Tackling = 1.0f,
                Jumping = 1.0f,

                // Physical Secondaries
                Strength = 0.7f,

                // Mental Secondaries
                Determination = 0.6f,
                Positioning = 0.6f,

                // Technical Secondaries
                Marking = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.DefenderCenter);
        }
    }

    [Description("Defensive Full Back (Defend)")]
    public float DefensiveFullBackDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Marking = 1.0f,
                Tackling = 1.0f,
                Stamina = 1.0f,
                Strength = 1.0f,

                // Physical Secondaries
                // - None

                // Mental Secondaries
                Concentration = 0.6f,
                Determination = 0.6f,
                Positioning = 0.6f,
                Teamwork = 0.6f,

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, this.BestDefenderSide());
        }
    }

    [Description("Full Back (Defend)")]
    public float FullBackDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Marking = 1.0f,
                Tackling = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                Acceleration = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                // - Nonde

                // Mental Secondaries
                Anticipation = 0.6f,
                Concentration = 0.6f,

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, this.BestDefenderSide());
        }
    }

    [Description("Full Back (Support)")]
    public float FullBackSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Marking = 1.0f,
                Tackling = 1.0f,
                Anticipation = 1.0f,
                Concentration = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,

                // Mental Secondaries
                // - None

                // Technical Secondaries
                Crossing = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, this.BestDefenderSide());
        }
    }

    [Description("Full Back (Attack)")]
    public float FullBackAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Crossing = 1.0f,
                Tackling = 1.0f,
                Anticipation = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Pace = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,
                OffTheBall = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f,
                FirstTouch = 0.5f,
                Marking = 0.5f
            };

            return FinalRating(Weights, this.BestDefenderSide());
        }
    }

    [Description("Inverted Wing Back (Support)")]
    public float InvertedWingBackSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Marking = 1.0f,
                Passing = 1.0f,
                Tackling = 1.0f,
                Anticipation = 1.0f,
                Decisions = 1.0f,
                Determination = 1.0f,
                Positioning = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Pace = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,
                OffTheBall = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f
            };

            return FinalRating(Weights, BestWingSide());
        }
    }

    [Description("Libero (Support)")]
    public float LiberoSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Heading = 1.0f,
                Marking = 1.0f,
                Passing = 1.0f,
                Tackling = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                Concentration = 1.0f,
                Decisions = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                Acceleration = 1.0f,
                Balance = 1.0f,
                Jumping = 1.0f,

                // Physical Secondaries
                Agility = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Bravery = 0.6f,
                Determination = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Sweeper);
        }
    }

    [Description("Libero (Attack)")]
    public float LiberoAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Heading = 1.0f,
                Marking = 1.0f,
                Passing = 1.0f,
                Tackling = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                Concentration = 1.0f,
                Decisions = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                Acceleration = 1.0f,
                Balance = 1.0f,
                Jumping = 1.0f,
                Pace = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Agility = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Bravery = 0.6f,
                Determination = 0.6f,
                Flair = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Sweeper);
        }
    }

    [Description("Sweeper (Defend)")]
    public float SweeperDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Heading = 1.0f,
                Marking = 1.0f,
                Tackling = 1.0f,
                Anticipation = 1.0f,
                Concentration = 1.0f,
                Decisions = 1.0f,
                Positioning = 1.0f,
                Acceleration = 1.0f,
                Balance = 1.0f,
                Jumping = 1.0f,

                // Physical Secondaries
                Agility = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Bravery = 0.6f,
                Composure = 0.6f,
                Determination = 0.6f

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Sweeper);
        }
    }

    [Description("Wing Back (Defend)")]
    public float WingBackDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Marking = 1.0f,
                Tackling = 1.0f,
                Decisions = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                // - None

                // Mental Secondaries
                Concentration = 0.6f,

                // Technical Secondaries
                Crossing = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, BestWingSide());
        }
    }

    [Description("Wing Back (Support)")]
    public float WingBackSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Marking = 1.0f,
                Passing = 1.0f,
                Tackling = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,

                // Technical Secondaries
                Crossing = 0.5f,
                Dribbling = 0.5f
            };

            return FinalRating(Weights, BestWingSide());
        }
    }

    [Description("Wing Back (Attack)")]
    public float WingBackAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Crossing = 1.0f,
                Dribbling = 1.0f,
                Tackling = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Pace = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,
                Flair = 0.6f,

                // Technical Secondaries
                Marking = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, BestWingSide());
        }
    }

    #endregion

    #region Midfielders

    [Description("Advanced Playmaker (Support)")]
    public float AdvancedPlaymakerSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                Vision = 1.0f,

                // Physical Secondaries
                // - None

                // Mental Secondaries
                Flair = 0.6f,
                Teamwork = 0.6f

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, BestAttackingMidfielderSide());
        }
    }

    [Description("Advanced Playmaker (Attack)")]
    public float AdvancedPlaymakerAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Dribbling = 1.0f,
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                Vision = 1.0f,
                Acceleration = 1.0f,

                // Physical Secondaries
                Pace = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Flair = 0.6f,
                OffTheBall = 0.6f,
                Teamwork = 0.6f,

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, BestAttackingMidfielderSide());
        }
    }

    [Description("Anchor Man (Defend)")]
    public float AnchorManDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Tackling = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                Concentration = 1.0f,
                Decisions = 1.0f,
                Positioning = 1.0f,

                // Physical Secondaries
                Strength = 0.7f,

                // Mental Secondaries
                Determination = 0.6f,

                // Technical Secondaries
                Heading = 0.5f,
                Marking = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.DefensiveMidfielder);
        }
    }

    [Description("Attacking Midfielder (Support)")]
    public float AttackingMidfielderSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Anticipation = 1.0f,
                Decisions = 1.0f,

                // Physical Secondaries
                // - None

                // Mental Secondaries
                Composure = 0.7f,
                OffTheBall = 0.7f,
                Positioning = 0.7f,
                Teamwork = 0.7f,
                Vision = 0.7f,

                // Technical Secondaries
                LongShots = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.AttackingMidfielderCenter);
        }
    }

    [Description("Attacking Midfielder (Attack)")]
    public float AttackingMidfielderAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Anticipation = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,

                // Physical Secondaries
                // - None

                // Mental Secondaries
                Composure = 0.6f,
                Concentration = 0.6f,
                Teamwork = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                Finishing = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.AttackingMidfielderCenter);
        }
    }

    [Description("Ball Winning Midfielder (Defend)")]
    public float BallWinningMidfielderDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Tackling = 1.0f,
                Aggression = 1.0f,
                Bravery = 1.0f,
                Determination = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Stamina = 1.0f,
                Strength = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,

                // Mental Secondaries
                Positioning = 0.6f,

                // Technical Secondaries
                Marking = 0.5f
            };

            return FinalRating(Weights, BestMidfielderCentre());
        }
    }

    [Description("Ball Winning Midfielder (Support)")]
    public float BallWinningMidfielderSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Tackling = 1.0f,
                Aggression = 1.0f,
                Bravery = 1.0f,
                Determination = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Stamina = 1.0f,
                Strength = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,

                // Mental Secondaries
                Positioning = 0.6f,

                // Technical Secondaries
                Marking = 0.5f
            };

            return FinalRating(Weights, BestMidfielderCentre());
        }
    }

    [Description("Box To Box Midfielder (Support)")]
    public float BoxToBoxMidfielderSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Passing = 1.0f,
                Tackling = 1.0f,
                Decisions = 1.0f,
                Determination = 1.0f,
                OffTheBall = 1.0f,
                Positioning = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Fitness = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Bravery = 0.6f,
                Composure = 0.6f,
                Concentration = 0.6f,

                // Technical Secondaries
                Finishing = 0.5f,
                FirstTouch = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.MidfielderCenter);
        }
    }

    [Description("Central Midfielder (Defend)")]
    public float CentralMidfielderDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Passing = 1.0f,
                Tackling = 1.0f,
                Concentration = 1.0f,
                Teamwork = 1.0f,

                // Physical Secondaries
                Stamina = 0.7f,

                // Mental Secondaries
                Decisions = 0.6f,
                Determination = 0.6f,
                Positioning = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f,
                Heading = 0.5f,
                Marking = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.MidfielderCenter);
        }
    }

    [Description("Central Midfielder (Support)")]
    public float CentralMidfielderSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Decisions = 1.0f,
                Teamwork = 1.0f,

                // Physical Secondaries
                Stamina = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,
                Determination = 0.6f,
                OffTheBall = 0.6f,
                Positioning = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                Marking = 0.5f,
                Tackling = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.MidfielderCenter);
        }
    }

    [Description("Central Midfielder (Attack)")]
    public float CentralMidfielderAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,
                Stamina = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,
                Determination = 0.6f,
                Teamwork = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                Finishing = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.MidfielderCenter);
        }
    }

    [Description("Deep Lying Playmaker (Defend)")]
    public float DeepLyingPlaymakerDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                Vision = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,

                // Mental Secondaries
                Positioning = 0.6f,
                Teamwork = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, BestMidfielderCentre());
        }
    }

    [Description("Deep Lying Playmaker (Support)")]
    public float DeepLyingPlaymakerSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                Vision = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,

                // Mental Secondaries
                OffTheBall = 0.6f,
                Teamwork = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                LongShots = 0.5f
            };

            return FinalRating(Weights, BestMidfielderCentre());
        }
    }

    [Description("Defensive Midfielder (Defend)")]
    public float DefensiveMidfielderDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Tackling = 1.0f,
                Concentration = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Aggression = 0.6f,
                Composure = 0.6f,
                Decisions = 0.6f,
                Determination = 0.6f,

                // Technical Secondaries
                Marking = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.DefensiveMidfielder);
        }
    }

    [Description("Defensive Midfielder (Support)")]
    public float DefensiveMidfielderSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Tackling = 1.0f,
                Concentration = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Aggression = 0.6f,
                Composure = 0.6f,
                Decisions = 0.6f,
                Determination = 0.6f,

                // Technical Secondaries
                Marking = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.DefensiveMidfielder);
        }
    }

    [Description("Defensive Winger (Defend)")]
    public float DefensiveWingerDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Crossing = 1.0f,
                Anticipation = 1.0f,
                Decisions = 1.0f,
                Determination = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,

                // Mental Secondaries
                Aggression = 0.6f,
                Concentration = 0.6f,
                Positioning = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f,
                Marking = 0.5f,
                Passing = 0.5f,
                Tackling = 0.5f
            };

            return FinalRating(Weights, BestMidfielderSide());
        }
    }

    [Description("Defensive Winger (Support)")]
    public float DefensiveWingerSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Anticipation = 1.0f,
                Decisions = 1.0f,
                Determination = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,

                // Mental Secondaries
                Aggression = 0.6f,
                Concentration = 0.6f,
                Positioning = 0.6f,

                // Technical Secondaries
                Crossing = 0.5f,
                Dribbling = 0.5f,
                Marking = 0.5f,
                Passing = 0.5f,
                Tackling = 0.5f
            };

            return FinalRating(Weights, BestMidfielderSide());
        }
    }

    [Description("Enganche (Attack)")]
    public float EngancheAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Vision = 1.0f,

                // Physical Secondaries
                // - None

                // Mental Secondaries
                Anticipation = 0.6f,
                Flair = 0.6f,

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.AttackingMidfielderCenter);
        }
    }

    [Description("Half Back (Defend)")]
    public float HalfBackDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                Heading = 0.5f,
                Marking = 0.5f,
                Tackling = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.DefensiveMidfielder);
        }
    }

    [Description("Inside Forward (Support)")]
    public float InsideForwardSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Dribbling = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Acceleration = 1.0f,

                // Physical Secondaries
                Pace = 0.7f,

                // Mental Secondaries
                Composure = 0.6f,
                Flair = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                Crossing = 0.5f,
                Finishing = 0.5f,
                FirstTouch = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, BestAttackingMidfielderSide());
        }
    }

    [Description("Inside Forward (Attack)")]
    public float InsideForwardAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Dribbling = 1.0f,
                Finishing = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Acceleration = 1.0f,

                // Physical Secondaries
                Pace = 0.7f,

                // Mental Secondaries
                Composure = 0.6f,
                Flair = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, BestAttackingMidfielderSide());
        }
    }

    [Description("Raumdeuter (Attack)")]
    public float RaumdeuterAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Anticipation = 1.0f,
                Composure = 1.0f,
                Concentration = 1.0f,
                Decisions = 1.0f,
                Determination = 1.0f,
                OffTheBall = 1.0f,
                WorkRate = 1.0f,
                Balance = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,

                // Mental Secondaries
                // - None

                // Technical Secondaries
                Finishing = 0.5f,
                FirstTouch = 0.5f
            };

            return FinalRating(Weights, BestAttackingMidfielderSide());
        }
    }

    [Description("Regista (Support)")]
    public float RegistaSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Vision = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,

                // Mental Secondaries
                Aggression = 0.6f,
                Anticipation = 0.6f,
                Concentration = 0.6f,
                Determination = 0.6f,
                Positioning = 0.6f,
                Teamwork = 0.6f,

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.DefensiveMidfielder);
        }
    }

    [Description("Roaming Playmaker (Support)")]
    public float RoamingPlaymakerSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                Dribbling = 1.0f,
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                Determination = 1.0f,
                OffTheBall = 1.0f,
                Vision = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Agility = 0.7f,
                Balance = 0.7f,
                Fitness = 0.7f,
                Pace = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,
                Positioning = 0.6f,

                // Technical Secondaries
                Finishing = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, BestMidfielderCentre());
        }
    }

    [Description("Shadow Striker (Attack)")]
    public float ShadowStrikerAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Finishing = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                Determination = 1.0f,
                OffTheBall = 1.0f,
                WorkRate = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,
                Agility = 0.7f,
                Pace = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Aggression = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f,
                LongShots = 0.5f,
                Passing = 0.5f,
                Technique = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.AttackingMidfielderCenter);
        }
    }

    [Description("Wide Midfielder (Defend)")]
    public float WideMidfielderDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Passing = 1.0f,
                Tackling = 1.0f,
                Positioning = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,

                // Physical Secondaries
                Stamina = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Decisions = 0.6f,
                Determination = 0.6f,

                // Technical Secondaries
                Crossing = 0.5f
            };

            return FinalRating(Weights, BestMidfielderSide());
        }
    }

    [Description("Wide Midfielder (Support)")]
    public float WideMidfielderSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Passing = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,

                // Physical Secondaries
                Stamina = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Decisions = 0.6f,
                Determination = 0.6f,
                Positioning = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                Crossing = 0.5f,
                FirstTouch = 0.5f,
                Tackling = 0.5f
            };

            return FinalRating(Weights, BestMidfielderSide());
        }
    }

    [Description("Wide Midfielder (Attack)")]
    public float WideMidfielderAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Crossing = 1.0f,
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,

                // Physical Secondaries
                Stamina = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Decisions = 0.6f,
                Determination = 0.6f,
                OffTheBall = 0.6f,
                Positioning = 0.6f,

                // Technical Secondaries
                Tackling = 0.5f
            };

            return FinalRating(Weights, BestMidfielderSide());
        }
    }

    [Description("Wide Playmaker (Support)")]
    public float WidePlaymakerSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                Positioning = 1.0f,
                Vision = 1.0f,

                // Physical Secondaries
                // - None

                // Mental Secondaries
                Anticipation = 0.6f,
                Concentration = 0.6f,
                Teamwork = 0.6f,

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, BestMidfielderSide());
        }
    }

    [Description("Wide Playmaker (Attack)")]
    public float WidePlaymakerAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Positioning = 1.0f,
                Vision = 1.0f,

                // Physical Secondaries
                // - None

                // Mental Secondaries
                Anticipation = 0.6f,
                Concentration = 0.6f,
                Teamwork = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f
            };

            return FinalRating(Weights, BestMidfielderSide());
        }
    }

    [Description("Wide Target Man (Support)")]
    public float WideTargetManSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Heading = 1.0f,
                Jumping = 1.0f,
                Strength = 1.0f,

                // Physical Secondaries
                Agility = 0.7f,
                Balance = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Decisions = 0.6f,
                OffTheBall = 0.6f,
                Teamwork = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f
            };

            return FinalRating(Weights, BestAttackingMidfielderSide());
        }
    }

    [Description("Wide Target Man (Attack)")]
    public float WideTargetManAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Heading = 1.0f,
                Jumping = 1.0f,
                Strength = 1.0f,

                // Physical Secondaries
                Agility = 0.7f,
                Balance = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Decisions = 0.6f,
                OffTheBall = 0.6f,
                Teamwork = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, BestAttackingMidfielderSide());
        }
    }

    [Description("Winger (Support)")]
    public float WingerSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Crossing = 1.0f,
                Technique = 1.0f,
                Acceleration = 1.0f,
                Pace = 1.0f,

                // Physical Secondaries
                Stamina = 0.7f,

                // Mental Secondaries
                WorkRate = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f,
                FirstTouch = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, BestWingerSide());
        }
    }

    [Description("Winger (Attack)")]
    public float WingerAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Crossing = 1.0f,
                Dribbling = 1.0f,
                Technique = 1.0f,
                Acceleration = 1.0f,
                Pace = 1.0f,

                // Physical Secondaries
                Stamina = 0.7f,

                // Mental Secondaries
                Flair = 0.6f,
                OffTheBall = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, BestWingerSide());
        }
    }

    #endregion

    #region Attackers

    [Description("Advanced Forward (Attack)")]
    public float AdvancedForwardAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Finishing = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                OffTheBall = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,
                Balance = 0.7f,

                // Mental Secondaries
                Decisions = 0.6f,
                Determination = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f,
                FirstTouch = 0.5f,
                Heading = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("Complete Forward (Support)")]
    public float CompleteForwardSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Dribbling = 1.0f,
                FirstTouch = 1.0f,
                Heading = 1.0f,
                LongShots = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Vision = 1.0f,
                Acceleration = 1.0f,
                Agility = 1.0f,
                Strength = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,
                Jumping = 0.7f,
                Pace = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,
                Determination = 0.6f,
                Teamwork = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                Finishing = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("Complete Forward (Attack)")]
    public float CompleteForwardAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Dribbling = 1.0f,
                Finishing = 1.0f,
                FirstTouch = 1.0f,
                Heading = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Vision = 1.0f,
                Acceleration = 1.0f,
                Agility = 1.0f,
                Strength = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,
                Jumping = 0.7f,
                Pace = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,
                Determination = 0.6f,
                Teamwork = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                LongShots = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("Deep Lying Forward (Support)")]
    public float DeepLyingForwardSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Teamwork = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f,
                Finishing = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("Deep Lying Forward (Attack)")]
    public float DeepLyingForwardAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Teamwork = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("Defensive Forward (Defend)")]
    public float DefensiveForwardDefend
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Aggression = 1.0f,
                Bravery = 1.0f,
                Determination = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Strength = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Concentration = 0.6f,
                Positioning = 0.6f

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("Defensive Forward (Support)")]
    public float DefensiveForwardSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Aggression = 1.0f,
                Bravery = 1.0f,
                Determination = 1.0f,
                Teamwork = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Strength = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Concentration = 0.6f,
                Positioning = 0.6f

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("False Nine (Support)")]
    public float FalseNineSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Composure = 1.0f,
                OffTheBall = 1.0f,
                Teamwork = 1.0f,
                Vision = 1.0f,

                // Physical Secondaries
                Agility = 0.7f,
                Balance = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Decisions = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f,
                Finishing = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("Poacher (Attack)")]
    public float PoacherAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Finishing = 1.0f,
                FirstTouch = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                OffTheBall = 1.0f,

                // Physical Secondaries
                Acceleration = 0.7f,
                Agility = 0.7f,

                // Mental Secondaries
                Concentration = 0.6f,

                // Technical Secondaries
                // - None
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("Target Man (Support)")]
    public float TargetManSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Heading = 1.0f,
                Jumping = 1.0f,
                Strength = 1.0f,

                // Physical Secondaries
                // - None

                // Mental Secondaries
                Aggression = 0.6f,
                Anticipation = 0.6f,
                Bravery = 0.6f,
                Decisions = 0.6f,
                Determination = 0.6f,
                Teamwork = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("Target Man (Attack)")]
    public float TargetManAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Finishing = 1.0f,
                Heading = 1.0f,
                Jumping = 1.0f,
                Strength = 1.0f,

                // Physical Secondaries
                // - None

                // Mental Secondaries
                Aggression = 0.6f,
                Anticipation = 0.6f,
                Bravery = 0.6f,
                Decisions = 0.6f,
                Determination = 0.6f,
                Teamwork = 0.6f,
                WorkRate = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f,
                Passing = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.Striker);
        }
    }

    [Description("Trequartista (Attack)")]
    public float TrequartistaAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                FirstTouch = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Anticipation = 1.0f,
                Composure = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Vision = 1.0f,

                // Physical Secondaries
                Agility = 0.7f,

                // Mental Secondaries
                Flair = 0.6f,

                // Technical Secondaries
                Finishing = 0.5f
            };

            return FinalRating(Weights, BestSCSide());
        }
    }

    [Description("Carillero (Support)")]
    public float CarilleroSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Anticipation = 1.0f,
                Decisions = 1.0f,
                Positioning = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Fitness = 0.7f,
                Pace = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries

                // Technical Secondaries
                Tackling = 0.5f
            };

            return FinalRating(Weights, BestMidfielderCentre());
        }
    }

    [Description("Segundo Volante (Support)")]
    public float SegundoVolanteSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Marking = 1.0f,
                Passing = 1.0f,
                Tackling = 1.0f,
                Determination = 1.0f,
                OffTheBall = 1.0f,
                Positioning = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Fitness = 1.0f,
                Stamina = 1.0f,
                Strength = 1.0f,

                // Physican Secondaries
                Balance = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Bravery = 0.6f,
                Composure = 0.6f,
                Concentration = 0.6f,
                Decisions = 0.6f,

                // Technical Secondaries
                Finishing = 0.5f,
                FirstTouch = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.DefensiveMidfielder);
        }
    }

    [Description("Segundo Volante (Attack)")]
    public float SegundoVolanteAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Finishing = 1.0f,
                LongShots = 1.0f,
                Marking = 1.0f,
                Passing = 1.0f,
                Tackling = 1.0f,
                Determination = 1.0f,
                OffTheBall = 1.0f,
                Positioning = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Fitness = 1.0f,
                Stamina = 1.0f,
                Strength = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Bravery = 0.6f,
                Composure = 0.6f,
                Concentration = 0.6f,
                Decisions = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.DefensiveMidfielder);
        }
    }

    [Description("Mezzala (Support)")]
    public float MezzalaSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Passing = 1.0f,
                Decisions = 1.0f,
                Determination = 1.0f,
                OffTheBall = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Fitness = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Bravery = 0.6f,
                Composure = 0.6f,
                Concentration = 0.6f,
                Flair = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                Dribbling = 0.5f,
                Finishing = 0.5f,
                FirstTouch = 0.5f,
                LongShots = 0.5f,
                Tackling = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.MidfielderCenter);
        }
    }

    [Description("Mezzala (Attack)")]
    public float MezzalaAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Dribbling = 1.0f,
                Finishing = 1.0f,
                Passing = 1.0f,
                Decisions = 1.0f,
                Determination = 1.0f,
                Flair = 1.0f,
                OffTheBall = 1.0f,
                WorkRate = 1.0f,
                Acceleration = 1.0f,
                Fitness = 1.0f,
                Stamina = 1.0f,

                // Physical Secondaries
                Balance = 0.7f,
                Strength = 0.7f,

                // Mental Secondaries
                Anticipation = 0.6f,
                Bravery = 0.6f,
                Composure = 0.6f,
                Concentration = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, CurrentPlayer.Attributes.MidfielderCenter);
        }
    }

    [Description("Inverted Winger (Support)")]
    public float InvertedWingerSupport
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Crossing = 1.0f,
                Dribbling = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Acceleration = 1.0f,

                // Physical Secondaries
                Pace = 0.7f,

                // Mental Secondaries
                Composure = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                FirstTouch = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, BestMidfielderSide());
        }
    }

    [Description("Inverted Winger (Attack)")]
    public float InvertedWingerAttack
    {
        get
        {
            RoleRatingWeights Weights = new RoleRatingWeights()
            {
                // Primary
                Crossing = 1.0f,
                Dribbling = 1.0f,
                Passing = 1.0f,
                Technique = 1.0f,
                Decisions = 1.0f,
                OffTheBall = 1.0f,
                Acceleration = 1.0f,

                // Physical Secondaries
                Pace = 0.7f,

                // Mental Secondaries
                Composure = 0.6f,
                Flair = 0.6f,
                Vision = 0.6f,

                // Technical Secondaries
                Finishing = 0.5f,
                FirstTouch = 0.5f,
                LongShots = 0.5f
            };

            return FinalRating(Weights, BestMidfielderSide());
        }
    }

    #endregion

    private byte BestDefenderSide()
    {
        return System.Math.Max(CurrentPlayer.Attributes.DefenderLeft, CurrentPlayer.Attributes.DefenderRight);
    }

    private byte BestWingSide()
    {
        byte bestD = System.Math.Max(CurrentPlayer.Attributes.DefenderLeft, CurrentPlayer.Attributes.DefenderRight);
        byte bestW = System.Math.Max(CurrentPlayer.Attributes.WingbackLeft, CurrentPlayer.Attributes.WingbackRight);

        return System.Math.Max(bestD, bestW);
    }

    private byte BestMidfielderSide()
    {
        return System.Math.Max(CurrentPlayer.Attributes.MidfielderLeft, CurrentPlayer.Attributes.MidfielderRight);
    }

    private byte BestMidfielderCentre()
    {
        return System.Math.Max(CurrentPlayer.Attributes.MidfielderCenter,
            CurrentPlayer.Attributes.DefensiveMidfielder);
    }

    private byte BestAttackingMidfielderSide()
    {
        return System.Math.Max(CurrentPlayer.Attributes.AttackingMidfielderLeft,
            CurrentPlayer.Attributes.AttackingMidfielderRight);
    }

    private byte BestForwardMidfielderSide()
    {
        byte bestMrl = System.Math.Max(CurrentPlayer.Attributes.MidfielderLeft,
            CurrentPlayer.Attributes.MidfielderRight);
        byte bestM = System.Math.Max(bestMrl, CurrentPlayer.Attributes.MidfielderCenter);

        byte bestAMrl = System.Math.Max(CurrentPlayer.Attributes.AttackingMidfielderLeft,
            CurrentPlayer.Attributes.AttackingMidfielderRight);
        byte bestAM = System.Math.Max(bestAMrl, CurrentPlayer.Attributes.AttackingMidfielderCenter);

        return System.Math.Max(bestM, bestAM);
    }

    private byte BestWingerSide()
    {
        byte bestMrl = System.Math.Max(CurrentPlayer.Attributes.MidfielderLeft,
            CurrentPlayer.Attributes.MidfielderRight);
        byte bestAMrl = System.Math.Max(CurrentPlayer.Attributes.AttackingMidfielderLeft,
            CurrentPlayer.Attributes.AttackingMidfielderRight);

        return System.Math.Max(bestMrl, bestAMrl);
    }

    private byte BestSCSide()
    {
        return System.Math.Max(CurrentPlayer.Attributes.AttackingMidfielderCenter,
            CurrentPlayer.Attributes.Striker);
    }

    private float FinalRating(RoleRatingWeights Weights, float Natural)
    {
        float fRating = ((Quality(Weights) * 5) / WeightsRating(Weights)) * ((Natural * 5) / 100);

        return fRating;
    }

    private float Quality(RoleRatingWeights weights)
    {
        float total = 0.0f;
        total += (CurrentPlayer.Attributes.Corners / 5) * weights.Corners;
        total += (CurrentPlayer.Attributes.Crossing / 5) * weights.Crossing;
        total += (CurrentPlayer.Attributes.Dribbling / 5) * weights.Dribbling;
        total += (CurrentPlayer.Attributes.Finishing / 5) * weights.Finishing;
        total += (CurrentPlayer.Attributes.FirstTouch / 5) * weights.FirstTouch;
        total += (CurrentPlayer.Attributes.FreekickTaking / 5) * weights.FreeKicks;
        total += (CurrentPlayer.Attributes.Heading / 5) * weights.Heading;
        total += (CurrentPlayer.Attributes.LongShots / 5) * weights.LongShots;
        total += (CurrentPlayer.Attributes.Marking / 5) * weights.Marking;
        total += (CurrentPlayer.Attributes.Passing / 5) * weights.Passing;
        total += (CurrentPlayer.Attributes.Penalties / 5) * weights.Penalty;
        total += (CurrentPlayer.Attributes.Tackling / 5) * weights.Tackling;
        total += (CurrentPlayer.Attributes.Technique / 5) * weights.Technique;
        total += (CurrentPlayer.Attributes.Aggression / 5) * weights.Aggression;
        total += (CurrentPlayer.Attributes.Anticipation / 5) * weights.Anticipation;
        total += (CurrentPlayer.Attributes.Bravery / 5) * weights.Bravery;
        total += (CurrentPlayer.Attributes.Composure / 5) * weights.Composure;
        total += (CurrentPlayer.Attributes.Concentration / 5) * weights.Concentration;
        total += (CurrentPlayer.Attributes.Decisions / 5) * weights.Decisions;
        total += (CurrentPlayer.Attributes.Determination / 5) * weights.Determination;
        total += (CurrentPlayer.Attributes.Flair / 5) * weights.Flair;
        total += (CurrentPlayer.Attributes.Influence / 5) * weights.Leadership;
        total += (CurrentPlayer.Attributes.OffTheBall / 5) * weights.OffTheBall;
        total += (CurrentPlayer.Attributes.Positioning / 5) * weights.Positioning;
        total += (CurrentPlayer.Attributes.Teamwork / 5) * weights.Teamwork;
        total += (CurrentPlayer.Attributes.Vision / 5) * weights.Vision;
        total += (CurrentPlayer.Attributes.Workrate / 5) * weights.WorkRate;
        total += (CurrentPlayer.Attributes.Acceleration / 5) * weights.Acceleration;
        total += (CurrentPlayer.Attributes.Agility / 5) * weights.Agility;
        total += (CurrentPlayer.Attributes.Balance / 5) * weights.Balance;
        total += (CurrentPlayer.Attributes.Jumping / 5) * weights.Jumping;
        total += (CurrentPlayer.Attributes.NaturalFitness / 5) * weights.Fitness;
        total += (CurrentPlayer.Attributes.Pace / 5) * weights.Pace;
        total += (CurrentPlayer.Attributes.Stamina / 5) * weights.Stamina;
        total += (CurrentPlayer.Attributes.Strength / 5) * weights.Strength;
        total += (CurrentPlayer.Attributes.AerialAbility / 5) * weights.AerialReach;
        total += (CurrentPlayer.Attributes.CommandOfArea / 5) * weights.CommandOfArea;
        total += (CurrentPlayer.Attributes.Communication / 5) * weights.Communication;
        total += (CurrentPlayer.Attributes.Eccentricity / 5) * weights.Eccentricity;
        total += (CurrentPlayer.Attributes.Handling / 5) * weights.Handling;
        total += (CurrentPlayer.Attributes.Kicking / 5) * weights.Kicking;
        total += (CurrentPlayer.Attributes.OneOnOnes / 5) * weights.OneOnOnes;
        total += (CurrentPlayer.Attributes.Reflexes / 5) * weights.Reflexes;
        total += (CurrentPlayer.Attributes.RushingOut / 5) * weights.RushingOut;
        total += (CurrentPlayer.Attributes.TendencyToPunch / 5) * weights.TendencyToPunch;

        return total;
    }

    private float WeightsRating(RoleRatingWeights weights)
    {
        float total = 0.0f;

        total = weights.Acceleration +
                weights.AerialReach +
                weights.Aggression +
                weights.Agility +
                weights.Anticipation +
                weights.Balance +
                weights.Bravery +
                weights.CommandOfArea +
                weights.Communication +
                weights.Composure +
                weights.Concentration +
                weights.Corners +
                weights.Crossing +
                weights.Decisions +
                weights.Determination +
                weights.Dribbling +
                weights.Eccentricity +
                weights.Finishing +
                weights.FirstTouch +
                weights.Fitness +
                weights.Flair +
                weights.FreeKicks +
                weights.Handling +
                weights.Heading +
                weights.Jumping +
                weights.Kicking +
                weights.Leadership +
                weights.LongShots +
                weights.LongThrows +
                weights.Marking +
                weights.OffTheBall +
                weights.OneOnOnes +
                weights.Pace +
                weights.Passing +
                weights.Penalty +
                weights.Positioning +
                weights.Reflexes +
                weights.RushingOut +
                weights.Stamina +
                weights.Strength +
                weights.Tackling +
                weights.Teamwork +
                weights.Technique +
                weights.TendencyToPunch +
                weights.Throwing +
                weights.Vision +
                weights.WorkRate;

        return total;
    }
}