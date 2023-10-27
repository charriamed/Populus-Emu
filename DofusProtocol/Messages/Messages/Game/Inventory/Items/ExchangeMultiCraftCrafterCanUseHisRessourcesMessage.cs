namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class ExchangeMultiCraftCrafterCanUseHisRessourcesMessage : Message
    {
        public const uint Id = 6020;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Allowed { get; set; }

        public ExchangeMultiCraftCrafterCanUseHisRessourcesMessage(bool allowed)
        {
            this.Allowed = allowed;
        }

        public ExchangeMultiCraftCrafterCanUseHisRessourcesMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(Allowed);
        }

        public override void Deserialize(IDataReader reader)
        {
            Allowed = reader.ReadBoolean();
        }

    }
}
