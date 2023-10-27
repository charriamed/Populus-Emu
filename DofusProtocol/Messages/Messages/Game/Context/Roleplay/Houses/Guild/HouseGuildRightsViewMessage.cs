namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseGuildRightsViewMessage : Message
    {
        public const uint Id = 5700;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint HouseId { get; set; }
        public int InstanceId { get; set; }

        public HouseGuildRightsViewMessage(uint houseId, int instanceId)
        {
            this.HouseId = houseId;
            this.InstanceId = instanceId;
        }

        public HouseGuildRightsViewMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(HouseId);
            writer.WriteInt(InstanceId);
        }

        public override void Deserialize(IDataReader reader)
        {
            HouseId = reader.ReadVarUInt();
            InstanceId = reader.ReadInt();
        }

    }
}
