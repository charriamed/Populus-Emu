namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PrismFightStateUpdateMessage : Message
    {
        public const uint Id = 6040;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte State { get; set; }

        public PrismFightStateUpdateMessage(sbyte state)
        {
            this.State = state;
        }

        public PrismFightStateUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(State);
        }

        public override void Deserialize(IDataReader reader)
        {
            State = reader.ReadSByte();
        }

    }
}
