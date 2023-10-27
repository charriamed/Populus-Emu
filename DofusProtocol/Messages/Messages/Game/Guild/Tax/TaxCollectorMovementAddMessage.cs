namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorMovementAddMessage : Message
    {
        public const uint Id = 5917;
        public override uint MessageId
        {
            get { return Id; }
        }
        public TaxCollectorInformations Informations { get; set; }

        public TaxCollectorMovementAddMessage(TaxCollectorInformations informations)
        {
            this.Informations = informations;
        }

        public TaxCollectorMovementAddMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(Informations.TypeId);
            Informations.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            Informations = ProtocolTypeManager.GetInstance<TaxCollectorInformations>(reader.ReadShort());
            Informations.Deserialize(reader);
        }

    }
}
