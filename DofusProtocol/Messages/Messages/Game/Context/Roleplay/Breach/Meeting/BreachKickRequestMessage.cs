namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class BreachKickRequestMessage : Message
    {
        public const uint Id = 6804;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ulong Target { get; set; }

        public BreachKickRequestMessage(ulong target)
        {
            this.Target = target;
        }

        public BreachKickRequestMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(Target);
        }

        public override void Deserialize(IDataReader reader)
        {
            Target = reader.ReadVarULong();
        }

    }
}
