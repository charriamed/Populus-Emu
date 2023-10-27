namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameContextKickMessage : Message
    {
        public const uint Id = 6081;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double TargetId { get; set; }

        public GameContextKickMessage(double targetId)
        {
            this.TargetId = targetId;
        }

        public GameContextKickMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(TargetId);
        }

        public override void Deserialize(IDataReader reader)
        {
            TargetId = reader.ReadDouble();
        }

    }
}
