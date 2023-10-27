namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeObjectMessage : Message
    {
        public const uint Id = 5515;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Remote { get; set; }

        public ExchangeObjectMessage(bool remote)
        {
            this.Remote = remote;
        }

        public ExchangeObjectMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Remote);
        }

        public override void Deserialize(IDataReader reader)
        {
            Remote = reader.ReadBoolean();
        }

    }
}
