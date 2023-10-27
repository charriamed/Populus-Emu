namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class DisplayNumericalValuePaddockMessage : Message
    {
        public const uint Id = 6563;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int RideId { get; set; }
        public int Value { get; set; }
        public sbyte Type { get; set; }

        public DisplayNumericalValuePaddockMessage(int rideId, int value, sbyte type)
        {
            this.RideId = rideId;
            this.Value = value;
            this.Type = type;
        }

        public DisplayNumericalValuePaddockMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(RideId);
            writer.WriteInt(Value);
            writer.WriteSByte(Type);
        }

        public override void Deserialize(IDataReader reader)
        {
            RideId = reader.ReadInt();
            Value = reader.ReadInt();
            Type = reader.ReadSByte();
        }

    }
}
