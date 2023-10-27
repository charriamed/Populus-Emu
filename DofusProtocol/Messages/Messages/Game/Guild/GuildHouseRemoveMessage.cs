namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GuildHouseRemoveMessage : Message
    {
        public const uint Id = 6180;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint HouseId { get; set; }
        public int InstanceId { get; set; }
        public bool SecondHand { get; set; }

        public GuildHouseRemoveMessage(uint houseId, int instanceId, bool secondHand)
        {
            this.HouseId = houseId;
            this.InstanceId = instanceId;
            this.SecondHand = secondHand;
        }

        public GuildHouseRemoveMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(HouseId);
            writer.WriteInt(InstanceId);
            writer.WriteBoolean(SecondHand);
        }

        public override void Deserialize(IDataReader reader)
        {
            HouseId = reader.ReadVarUInt();
            InstanceId = reader.ReadInt();
            SecondHand = reader.ReadBoolean();
        }

    }
}
