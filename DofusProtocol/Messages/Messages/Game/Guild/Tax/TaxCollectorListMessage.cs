namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorListMessage : AbstractTaxCollectorListMessage
    {
        public new const uint Id = 5930;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte NbcollectorMax { get; set; }
        public TaxCollectorFightersInformation[] FightersInformations { get; set; }
        public sbyte InfoType { get; set; }

        public TaxCollectorListMessage(TaxCollectorInformations[] informations, sbyte nbcollectorMax, TaxCollectorFightersInformation[] fightersInformations, sbyte infoType)
        {
            this.Informations = informations;
            this.NbcollectorMax = nbcollectorMax;
            this.FightersInformations = fightersInformations;
            this.InfoType = infoType;
        }

        public TaxCollectorListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(NbcollectorMax);
            writer.WriteShort((short)FightersInformations.Count());
            for (var fightersInformationsIndex = 0; fightersInformationsIndex < FightersInformations.Count(); fightersInformationsIndex++)
            {
                var objectToSend = FightersInformations[fightersInformationsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteSByte(InfoType);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            NbcollectorMax = reader.ReadSByte();
            var fightersInformationsCount = reader.ReadUShort();
            FightersInformations = new TaxCollectorFightersInformation[fightersInformationsCount];
            for (var fightersInformationsIndex = 0; fightersInformationsIndex < fightersInformationsCount; fightersInformationsIndex++)
            {
                var objectToAdd = new TaxCollectorFightersInformation();
                objectToAdd.Deserialize(reader);
                FightersInformations[fightersInformationsIndex] = objectToAdd;
            }
            InfoType = reader.ReadSByte();
        }

    }
}
