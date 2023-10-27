namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TitleGainedMessage : Message
    {
        public const uint Id = 6364;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort TitleId { get; set; }

        public TitleGainedMessage(ushort titleId)
        {
            this.TitleId = titleId;
        }

        public TitleGainedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(TitleId);
        }

        public override void Deserialize(IDataReader reader)
        {
            TitleId = reader.ReadVarUShort();
        }

    }
}
