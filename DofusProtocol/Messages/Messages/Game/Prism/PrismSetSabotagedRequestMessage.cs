namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismSetSabotagedRequestMessage : Message
    {
        public const uint Id = 6468;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }

        public PrismSetSabotagedRequestMessage(ushort subAreaId)
        {
            this.SubAreaId = subAreaId;
        }

        public PrismSetSabotagedRequestMessage() { }

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
