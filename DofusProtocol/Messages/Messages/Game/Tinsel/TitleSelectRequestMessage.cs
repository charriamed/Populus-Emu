namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TitleSelectRequestMessage : Message
    {
        public const uint Id = 6365;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort TitleId { get; set; }

        public TitleSelectRequestMessage(ushort titleId)
        {
            this.TitleId = titleId;
        }

        public TitleSelectRequestMessage() { }

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
