namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class StatsUpgradeRequestMessage : Message
    {
        public const uint Id = 5610;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool UseAdditionnal { get; set; }
        public sbyte StatId { get; set; }
        public ushort BoostPoint { get; set; }

        public StatsUpgradeRequestMessage(bool useAdditionnal, sbyte statId, ushort boostPoint)
        {
            this.UseAdditionnal = useAdditionnal;
            this.StatId = statId;
            this.BoostPoint = boostPoint;
        }

        public StatsUpgradeRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(UseAdditionnal);
            writer.WriteSByte(StatId);
            writer.WriteVarUShort(BoostPoint);
        }

        public override void Deserialize(IDataReader reader)
        {
            UseAdditionnal = reader.ReadBoolean();
            StatId = reader.ReadSByte();
            BoostPoint = reader.ReadVarUShort();
        }

    }
}
