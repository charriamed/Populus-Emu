namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LockableStateUpdateHouseDoorMessage : LockableStateUpdateAbstractMessage
    {
        public new const uint Id = 5668;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint HouseId { get; set; }
        public int InstanceId { get; set; }
        public bool SecondHand { get; set; }

        public LockableStateUpdateHouseDoorMessage(bool locked, uint houseId, int instanceId, bool secondHand)
        {
            this.Locked = locked;
            this.HouseId = houseId;
            this.InstanceId = instanceId;
            this.SecondHand = secondHand;
        }

        public LockableStateUpdateHouseDoorMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(HouseId);
            writer.WriteInt(InstanceId);
            writer.WriteBoolean(SecondHand);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            HouseId = reader.ReadVarUInt();
            InstanceId = reader.ReadInt();
            SecondHand = reader.ReadBoolean();
        }

    }
}
