namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameRolePlayDelayedActionFinishedMessage : Message
    {
        public const uint Id = 6150;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double DelayedCharacterId { get; set; }
        public sbyte DelayTypeId { get; set; }

        public GameRolePlayDelayedActionFinishedMessage(double delayedCharacterId, sbyte delayTypeId)
        {
            this.DelayedCharacterId = delayedCharacterId;
            this.DelayTypeId = delayTypeId;
        }

        public GameRolePlayDelayedActionFinishedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(DelayedCharacterId);
            writer.WriteSByte(DelayTypeId);
        }

        public override void Deserialize(IDataReader reader)
        {
            DelayedCharacterId = reader.ReadDouble();
            DelayTypeId = reader.ReadSByte();
        }

    }
}
