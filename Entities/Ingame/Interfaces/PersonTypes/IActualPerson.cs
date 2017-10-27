using System;

namespace FMScoutFramework.Core.Entities.InGame.Interfaces {
    public interface IActualPerson {
        DateTime DateOfBirth { get; set; }
        string FirstName { get; }
        string LastName { get; }
        string CommonName { get; }
        City CityOfBirth { get; }
        Nation Nation { get; }
        PersonAttributes Attributes { get; }
        byte Ethnicity { get; set; }  
        byte HairColour { get; set; }
        sbyte SkinTone { get; set; }
        Contract Contract { get; }
    }
}
