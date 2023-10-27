namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachSavedMessage : Message
    {
        public const uint Id = 6798;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Saved { get; set; }

        public BreachSavedMessage(bool saved)
        {
            this.Saved = saved;
        }

        public BreachSavedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Saved);
        }

        public override void Deserialize(IDataReader reader)
        {
            Saved = reader.ReadBoolean();
        }

    }
}
