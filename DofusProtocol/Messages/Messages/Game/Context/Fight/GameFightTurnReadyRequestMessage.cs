namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class GameFightTurnReadyRequestMessage : Message
    {
        public const uint Id = 715;
        public override uint MessageId
        {
            get { return Id; }
        }
        public double ObjectId { get; set; }

        public GameFightTurnReadyRequestMessage(double objectId)
        {
            this.ObjectId = objectId;
        }

        public GameFightTurnReadyRequestMessage() { }

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
