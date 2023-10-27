namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class VersionExtended : Version
    {
        public new const short Id = 393;
        public override short TypeId
        {
            get { return Id; }
        }
        public sbyte Install { get; set; }
        public sbyte Technology { get; set; }

        public VersionExtended(sbyte major, sbyte minor, sbyte release, int revision, sbyte patch, sbyte buildType, sbyte install, sbyte technology)
        {
            this.Major = major;
            this.Minor = minor;
            this.Release = release;
            this.Revision = revision;
            this.Patch = patch;
            this.BuildType = buildType;
            this.Install = install;
            this.Technology = technology;
        }

        public VersionExtended() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Install);
            writer.WriteSByte(Technology);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Install = reader.ReadSByte();
            Technology = reader.ReadSByte();
        }

    }
}
