namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachRewardBoughtMessage : Message
    {
        public const uint Id = 6797;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint ObjectId { get; set; }
        public bool Bought { get; set; }

        public BreachRewardBoughtMessage(uint objectId, bool bought)
        {
            this.ObjectId = objectId;
            this.Bought = bought;
        }

        public BreachRewardBoughtMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectId);
            writer.WriteBoolean(Bought);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadVarUInt();
            Bought = reader.ReadBoolean();
        }

    }
}
