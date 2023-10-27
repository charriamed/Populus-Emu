namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachSaveBoughtMessage : Message
    {
        public const uint Id = 6788;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Bought { get; set; }

        public BreachSaveBoughtMessage(bool bought)
        {
            this.Bought = bought;
        }

        public BreachSaveBoughtMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Bought);
        }

        public override void Deserialize(IDataReader reader)
        {
            Bought = reader.ReadBoolean();
        }

    }
}
