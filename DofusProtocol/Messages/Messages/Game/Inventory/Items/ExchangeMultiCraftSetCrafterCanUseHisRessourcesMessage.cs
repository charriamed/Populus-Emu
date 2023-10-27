namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage : Message
    {
        public const uint Id = 6021;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Allow { get; set; }

        public ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage(bool allow)
        {
            this.Allow = allow;
        }

        public ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Allow);
        }

        public override void Deserialize(IDataReader reader)
        {
            Allow = reader.ReadBoolean();
        }

    }
}
