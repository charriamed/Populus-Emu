namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismFightRemovedMessage : Message
    {
        public const uint Id = 6453;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }

        public PrismFightRemovedMessage(ushort subAreaId)
        {
            this.SubAreaId = subAreaId;
        }

        public PrismFightRemovedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
        }

    }
}
