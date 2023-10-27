namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class LivingObjectChangeSkinRequestMessage : Message
    {
        public const uint Id = 5725;
        public override uint MessageId
        {
            get { return Id; }
        }
        public uint LivingUID { get; set; }
        public byte LivingPosition { get; set; }
        public uint SkinId { get; set; }

        public LivingObjectChangeSkinRequestMessage(uint livingUID, byte livingPosition, uint skinId)
        {
            this.LivingUID = livingUID;
            this.LivingPosition = livingPosition;
            this.SkinId = skinId;
        }

        public LivingObjectChangeSkinRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(LivingUID);
            writer.WriteByte(LivingPosition);
            writer.WriteVarUInt(SkinId);
        }

        public override void Deserialize(IDataReader reader)
        {
            LivingUID = reader.ReadVarUInt();
            LivingPosition = reader.ReadByte();
            SkinId = reader.ReadVarUInt();
        }

    }
}
