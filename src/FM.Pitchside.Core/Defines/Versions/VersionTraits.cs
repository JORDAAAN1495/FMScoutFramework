namespace FM.Pitchside.Core.Defines.Versions
{
    /// <summary>Layout facts shared by several version definitions, so entity offset tables don't hard-code version types.</summary>
    internal static class VersionTraits
    {
        /// <summary>
        /// True for the Unity-era builds (FM26, FM27), where a person's name/identity fields sit directly
        /// on the Player/Person object instead of in a separate ActualPerson sub-object (FM17-22 had one).
        /// </summary>
        public static bool ActualPersonIsInline(IVersion version)
        {
            return version is Steam_26_0_0_0_Windows || version is IFm27Version;
        }
    }
}
