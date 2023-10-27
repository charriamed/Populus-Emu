namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AbstractTaxCollectorListMessage : Message
    {
        public const uint Id = 6568;
        public override uint MessageId
        {
            get { return Id; }
        }
        public TaxCollectorInformations[] Informations { get; set; }

        public AbstractTaxCollectorListMessage(TaxCollectorInformations[] informations)
        {
            this.Informations = informations;
        }

        public AbstractTaxCollectorListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Informations.Count());
            for (var informationsIndex = 0; informationsIndex < Informations.Count(); informationsIndex++)
            {
                var objectToSend = Informations[informationsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            var informationsCount = reader.ReadUShort();
            Informations = new TaxCollectorInformations[informationsCount];
            for (var informationsIndex = 0; informationsIndex < informationsCount; informationsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<TaxCollectorInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Informations[informationsIndex] = objectToAdd;
            }
        }

    }
}
