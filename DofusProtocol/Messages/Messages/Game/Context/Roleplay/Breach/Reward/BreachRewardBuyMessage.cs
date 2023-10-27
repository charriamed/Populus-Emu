namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachRewardBuyMessage : Message
    {
        public const uint Id = 6803;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectId { get; set; }

        public BreachRewardBuyMessage(uint objectId)
        {
            this.ObjectId = objectId;
        }

        public BreachRewardBuyMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUInt();
        }

    }
}
