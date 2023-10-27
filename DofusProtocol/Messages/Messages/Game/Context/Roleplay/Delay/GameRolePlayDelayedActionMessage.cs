namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayDelayedActionMessage : Message
    {
        public const uint Id = 6153;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double DelayedCharacterId { get; set; }
        public sbyte DelayTypeId { get; set; }
        public double DelayEndTime { get; set; }

        public GameRolePlayDelayedActionMessage(double delayedCharacterId, sbyte delayTypeId, double delayEndTime)
        {
            this.DelayedCharacterId = delayedCharacterId;
            this.DelayTypeId = delayTypeId;
            this.DelayEndTime = delayEndTime;
        }

        public GameRolePlayDelayedActionMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(DelayedCharacterId);
            writer.WriteSByte(DelayTypeId);
            writer.WriteDouble(DelayEndTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            DelayedCharacterId = reader.ReadDouble();
            DelayTypeId = reader.ReadSByte();
            DelayEndTime = reader.ReadDouble();
        }

    }
}
