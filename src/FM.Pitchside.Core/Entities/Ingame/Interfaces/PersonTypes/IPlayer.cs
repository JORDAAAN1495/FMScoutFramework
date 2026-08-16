using System;

namespace FMScoutFramework.Core.Entities.InGame.Interfaces
{
    public interface IPlayer
    {
        ActualPerson ActualPerson { get; }
        Int64 InjuriesPtr { get; }
        Int64 BansPtr { get; }
        Team Team { get; }
        int Value { get; set; }
        int AskingPrice { get; set; }
        short Fitness { get; set; }
        short Jadedness { get; set; }
        short Condition { get; set; }
        short HomeReputation { get; set; }
        short CurrentReputation { get; set; }
        short WorldReputation { get; set; }
        short CA { get; set; }
        short PA { get; set; }
        short Weight { get; set; }
        short Height { get; set; }
        PlayerAttributes Attributes { get; }
    }
}