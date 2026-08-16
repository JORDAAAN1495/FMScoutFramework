using System;
namespace FMScoutFramework.Core.Entities.GameVersions
{
    public interface IVersionPersonEnumPointers
    {
        Int64 Player { get; }
        Int64 Staff { get; }
        Int64 PlayerStaff { get; }
        Int64 HumanManager { get; }
        Int64 Official { get; }
    }
}