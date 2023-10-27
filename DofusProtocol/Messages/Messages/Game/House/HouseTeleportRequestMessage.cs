namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseTeleportRequestMessage : Message
    {
        public const uint Id = 6726;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint HouseId { get; set; }
        public int HouseInstanceId { get; set; }

        public HouseTeleportRequestMessage(uint houseId, int houseInstanceId)
        {
            this.HouseId = houseId;
            this.HouseInstanceId = houseInstanceId;
        }

        public HouseTeleportRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(HouseId);
            writer.WriteInt(HouseInstanceId);
        }

        public override void Deserialize(IDataReader reader)
        {
            HouseId = reader.ReadVarUInt();
            HouseInstanceId = reader.ReadInt();
        }

    }
}
