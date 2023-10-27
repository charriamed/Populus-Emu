namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ObtainedItemWithBonusMessage : ObtainedItemMessage
    {
        public new const uint Id = 6520;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint BonusQuantity { get; set; }

        public ObtainedItemWithBonusMessage(ushort genericId, uint baseQuantity, uint bonusQuantity)
        {
            this.GenericId = genericId;
            this.BaseQuantity = baseQuantity;
            this.BonusQuantity = bonusQuantity;
        }

        public ObtainedItemWithBonusMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(BonusQuantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            BonusQuantity = reader.ReadVarUInt();
        }

    }
}
