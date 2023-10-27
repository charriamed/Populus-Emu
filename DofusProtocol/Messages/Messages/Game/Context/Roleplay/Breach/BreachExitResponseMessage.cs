namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachExitResponseMessage : Message
    {
        public const uint Id = 6814;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Exited { get; set; }

        public BreachExitResponseMessage(bool exited)
        {
            this.Exited = exited;
        }

        public BreachExitResponseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Exited);
        }

        public override void Deserialize(IDataReader reader)
        {
            Exited = reader.ReadBoolean();
        }

    }
}
