namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LivingObjectDissociateMessage : Message
    {
        public const uint Id = 5723;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint LivingUID { get; set; }
        public byte LivingPosition { get; set; }

        public LivingObjectDissociateMessage(uint livingUID, byte livingPosition)
        {
            this.LivingUID = livingUID;
            this.LivingPosition = livingPosition;
        }

        public LivingObjectDissociateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(LivingUID);
            writer.WriteByte(LivingPosition);
        }

        public override void Deserialize(IDataReader reader)
        {
            LivingUID = reader.ReadVarUInt();
            LivingPosition = reader.ReadByte();
        }

    }
}
