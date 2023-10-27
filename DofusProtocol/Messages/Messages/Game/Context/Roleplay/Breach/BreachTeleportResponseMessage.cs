namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachTeleportResponseMessage : Message
    {
        public const uint Id = 6816;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Teleported { get; set; }

        public BreachTeleportResponseMessage(bool teleported)
        {
            this.Teleported = teleported;
        }

        public BreachTeleportResponseMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Teleported);
        }

        public override void Deserialize(IDataReader reader)
        {
            Teleported = reader.ReadBoolean();
        }

    }
}
