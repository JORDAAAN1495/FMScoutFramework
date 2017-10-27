using System;
using System.Collections.Generic;

namespace FMScoutFramework.Core.Entities.InGame.Interfaces
{
    public interface IContract
    {
        Person Person { get; }
        Team Team { get; }
        int Wage { get; set; }
        byte JobType { get; set; }
        Int64 UnhappinessPointer { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        DateTime JoinDate { get; set; }
        byte SquadStatus { get; set; }
        byte TransferStatus { get; set; }
        byte SquadNumber { get; set; }
        List<ContractClause> Clauses { get; }
        // List<ContractBonus> Bonuses { get; }
        int LoyaltyBonus { get; set; }
        byte Type { get; set; }
    }
}