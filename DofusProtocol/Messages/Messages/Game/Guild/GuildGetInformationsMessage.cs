namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildGetInformationsMessage : Message
    {
        public const uint Id = 5550;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte InfoType { get; set; }

        public GuildGetInformationsMessage(sbyte infoType)
        {
            this.InfoType = infoType;
        }

        public GuildGetInformationsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(InfoType);
        }

        public override void Deserialize(IDataReader reader)
        {
            InfoType = reader.ReadSByte();
        }

    }
}
