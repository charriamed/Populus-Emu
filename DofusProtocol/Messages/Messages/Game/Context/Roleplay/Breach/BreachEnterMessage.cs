namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachEnterMessage : Message
    {
        public const uint Id = 6810;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Owner { get; set; }

        public BreachEnterMessage(ulong owner)
        {
            this.Owner = owner;
        }

        public BreachEnterMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(Owner);
        }

        public override void Deserialize(IDataReader reader)
        {
            Owner = reader.ReadVarULong();
        }

    }
}
