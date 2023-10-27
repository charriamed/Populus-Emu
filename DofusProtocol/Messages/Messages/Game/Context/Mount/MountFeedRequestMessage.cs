namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class MountFeedRequestMessage : Message
    {
        public const uint Id = 6189;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint MountUid { get; set; }
        public sbyte MountLocation { get; set; }
        public uint MountFoodUid { get; set; }
        public uint Quantity { get; set; }

        public MountFeedRequestMessage(uint mountUid, sbyte mountLocation, uint mountFoodUid, uint quantity)
        {
            this.MountUid = mountUid;
            this.MountLocation = mountLocation;
            this.MountFoodUid = mountFoodUid;
            this.Quantity = quantity;
        }

        public MountFeedRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(MountUid);
            writer.WriteSByte(MountLocation);
            writer.WriteVarUInt(MountFoodUid);
            writer.WriteVarUInt(Quantity);
        }

        public override void Deserialize(IDataReader reader)
        {
            MountUid = reader.ReadVarUInt();
            MountLocation = reader.ReadSByte();
            MountFoodUid = reader.ReadVarUInt();
            Quantity = reader.ReadVarUInt();
        }

    }
}
