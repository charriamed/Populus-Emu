namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismSettingsRequestMessage : Message
    {
        public const uint Id = 6437;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort SubAreaId { get; set; }
        public sbyte StartDefenseTime { get; set; }

        public PrismSettingsRequestMessage(ushort subAreaId, sbyte startDefenseTime)
        {
            this.SubAreaId = subAreaId;
            this.StartDefenseTime = startDefenseTime;
        }

        public PrismSettingsRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(SubAreaId);
            writer.WriteSByte(StartDefenseTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            SubAreaId = reader.ReadVarUShort();
            StartDefenseTime = reader.ReadSByte();
        }

    }
}
