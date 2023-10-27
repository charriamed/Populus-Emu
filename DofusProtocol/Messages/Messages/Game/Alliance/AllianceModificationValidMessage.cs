namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceModificationValidMessage : Message
    {
        public const uint Id = 6450;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string AllianceName { get; set; }
        public string AllianceTag { get; set; }
        public GuildEmblem Alliancemblem { get; set; }

        public AllianceModificationValidMessage(string allianceName, string allianceTag, GuildEmblem alliancemblem)
        {
            this.AllianceName = allianceName;
            this.AllianceTag = allianceTag;
            this.Alliancemblem = alliancemblem;
        }

        public AllianceModificationValidMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(AllianceName);
            writer.WriteUTF(AllianceTag);
            Alliancemblem.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            AllianceName = reader.ReadUTF();
            AllianceTag = reader.ReadUTF();
            Alliancemblem = new GuildEmblem();
            Alliancemblem.Deserialize(reader);
        }

    }
}
