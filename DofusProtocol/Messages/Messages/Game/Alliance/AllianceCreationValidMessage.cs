namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceCreationValidMessage : Message
    {
        public const uint Id = 6393;
        public override uint MessageId
        {
            get { return Id; }
        }
        public string AllianceName { get; set; }
        public string AllianceTag { get; set; }
        public GuildEmblem AllianceEmblem { get; set; }

        public AllianceCreationValidMessage(string allianceName, string allianceTag, GuildEmblem allianceEmblem)
        {
            this.AllianceName = allianceName;
            this.AllianceTag = allianceTag;
            this.AllianceEmblem = allianceEmblem;
        }

        public AllianceCreationValidMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(AllianceName);
            writer.WriteUTF(AllianceTag);
            AllianceEmblem.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            AllianceName = reader.ReadUTF();
            AllianceTag = reader.ReadUTF();
            AllianceEmblem = new GuildEmblem();
            AllianceEmblem.Deserialize(reader);
        }

    }
}
