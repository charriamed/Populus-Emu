namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeCraftResultWithObjectIdMessage : ExchangeCraftResultMessage
    {
        public new const uint Id = 6000;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort ObjectGenericId { get; set; }

        public ExchangeCraftResultWithObjectIdMessage(sbyte craftResult, ushort objectGenericId)
        {
            this.CraftResult = craftResult;
            this.ObjectGenericId = objectGenericId;
        }

        public ExchangeCraftResultWithObjectIdMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(ObjectGenericId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            ObjectGenericId = reader.ReadVarUShort();
        }

    }
}
