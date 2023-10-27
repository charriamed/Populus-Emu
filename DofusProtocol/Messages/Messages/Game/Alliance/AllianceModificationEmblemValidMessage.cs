namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AllianceModificationEmblemValidMessage : Message
    {
        public const uint Id = 6447;
        public override uint MessageId
        {
            get { return Id; }
        }
        public GuildEmblem Alliancemblem { get; set; }

        public AllianceModificationEmblemValidMessage(GuildEmblem alliancemblem)
        {
            this.Alliancemblem = alliancemblem;
        }

        public AllianceModificationEmblemValidMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            Alliancemblem.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Alliancemblem = new GuildEmblem();
            Alliancemblem.Deserialize(reader);
        }

    }
}
