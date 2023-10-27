namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightTurnEndMessage : Message
    {
        public const uint Id = 719;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double ObjectId { get; set; }

        public GameFightTurnEndMessage(double objectId)
        {
            this.ObjectId = objectId;
        }

        public GameFightTurnEndMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(ObjectId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadDouble();
        }

    }
}
