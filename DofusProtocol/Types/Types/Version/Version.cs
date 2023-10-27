namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class Version
    {
        public const short Id  = 11;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte Major { get; set; }
        public sbyte Minor { get; set; }
        public sbyte Release { get; set; }
        public int Revision { get; set; }
        public sbyte Patch { get; set; }
        public sbyte BuildType { get; set; }

        public Version(sbyte major, sbyte minor, sbyte release, int revision, sbyte patch, sbyte buildType)
        {
            this.Major = major;
            this.Minor = minor;
            this.Release = release;
            this.Revision = revision;
            this.Patch = patch;
            this.BuildType = buildType;
        }

        public Version() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Major);
            writer.WriteSByte(Minor);
            writer.WriteSByte(Release);
            writer.WriteInt(Revision);
            writer.WriteSByte(Patch);
            writer.WriteSByte(BuildType);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Major = reader.ReadSByte();
            Minor = reader.ReadSByte();
            Release = reader.ReadSByte();
            Revision = reader.ReadInt();
            Patch = reader.ReadSByte();
            BuildType = reader.ReadSByte();
        }

    }
}
