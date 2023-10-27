namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class HouseGuildShareRequestMessage : Message
    {
        public const uint Id = 5704;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint HouseId { get; set; }
        public int InstanceId { get; set; }
        public bool Enable { get; set; }
        public uint Rights { get; set; }

        public HouseGuildShareRequestMessage(uint houseId, int instanceId, bool enable, uint rights)
        {
            this.HouseId = houseId;
            this.InstanceId = instanceId;
            this.Enable = enable;
            this.Rights = rights;
        }

        public HouseGuildShareRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(HouseId);
            writer.WriteInt(InstanceId);
            writer.WriteBoolean(Enable);
            writer.WriteVarUInt(Rights);
        }

        public override void Deserialize(IDataReader reader)
        {
            HouseId = reader.ReadVarUInt();
            InstanceId = reader.ReadInt();
            Enable = reader.ReadBoolean();
            Rights = reader.ReadVarUInt();
        }

    }
}
