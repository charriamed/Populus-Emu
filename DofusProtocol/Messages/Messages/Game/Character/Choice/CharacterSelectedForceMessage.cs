namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class CharacterSelectedForceMessage : Message
    {
        public const uint Id = 6068;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int ObjectId { get; set; }

        public CharacterSelectedForceMessage(int objectId)
        {
            this.ObjectId = objectId;
        }

        public CharacterSelectedForceMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(ObjectId);
        }

        public override void Deserialize(IDataReader reader)
        {
            ObjectId = reader.ReadInt();
        }

    }
}
