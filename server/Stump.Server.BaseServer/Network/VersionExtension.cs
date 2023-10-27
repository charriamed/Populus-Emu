using Stump.Core.Attributes;
using Stump.DofusProtocol.Enums;
using Stump.DofusProtocol.Types;

namespace Stump.Server.BaseServer.Network
{
    public enum VersionCheckingSeverity
    {
        /// <summary>
        /// Do not check version
        /// </summary>
        None,
        /// <summary>
        /// Check major minor and release values
        /// </summary>
        Light,
        /// <summary>
        /// Check revision value too
        /// </summary>
        Medium,
        /// <summary>
        /// Check all values
        /// </summary>
        Heavy,
    }

    public static class VersionExtension
    {
        /// <summary>
        ///   Define the severity of the client version checking. Set to Light/NoCheck if you have any bugs with it.
        /// </summary>
        [Variable(true)]
        public static VersionCheckingSeverity Severity = VersionCheckingSeverity.Light;

        /// <summary>
        /// Version for the client. 
        /// </summary>
        [Variable(true)]
        public static Version ExpectedVersion = new Version(2, 33, 0, 101615, 2, (sbyte) BuildTypeEnum.RELEASE);

        /// <summary>
        /// Actual version
        /// </summary>
        [Variable(true)]
        public static int ActualProtocol = 1692;

        /// <summary>
        /// Required version
        /// </summary>
        [Variable(true)]
        public static int ProtocolRequired = 1692;

        /// <summary>
        /// Compare the given version and the required version
        /// </summary>
        /// <param name="versionToCompare"></param>
        /// <returns></returns>
        public static bool IsUpToDate(this Version versionToCompare)
        {
            switch (Severity)
            {
                case VersionCheckingSeverity.None:
                    return true;
                case VersionCheckingSeverity.Light:
                    return ExpectedVersion.Major == versionToCompare.Major &&
                           ExpectedVersion.Minor == versionToCompare.Minor &&
                           ExpectedVersion.Release == versionToCompare.Release;
                case VersionCheckingSeverity.Medium:
                    return ExpectedVersion.Major == versionToCompare.Major &&
                           ExpectedVersion.Minor == versionToCompare.Minor &&
                           ExpectedVersion.Release == versionToCompare.Release &&
                           ExpectedVersion.Revision == versionToCompare.Revision;
                case VersionCheckingSeverity.Heavy:
                    return ExpectedVersion.Major == versionToCompare.Major &&
                           ExpectedVersion.Minor == versionToCompare.Minor &&
                           ExpectedVersion.Release == versionToCompare.Release &&
                           ExpectedVersion.Revision == versionToCompare.Revision &&
                           ExpectedVersion.Patch == versionToCompare.Patch;
            }
            
            return false;
        }

        public static VersionExtended ToVersionExtended(this Version version, sbyte install, sbyte technology)
        {
            return new VersionExtended(version.Major, version.Minor, version.Release, version.Revision, version.Patch,
                version.BuildType, install, technology);
        }
    }
}