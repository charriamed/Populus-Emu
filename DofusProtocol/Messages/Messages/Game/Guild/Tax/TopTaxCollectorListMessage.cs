namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class TopTaxCollectorListMessage : AbstractTaxCollectorListMessage
    {
        public new const uint Id = 6565;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool IsDungeon { get; set; }

        public TopTaxCollectorListMessage(TaxCollectorInformations[] informations, bool isDungeon)
        {
            this.Informations = informations;
            this.IsDungeon = isDungeon;
        }

        public TopTaxCollectorListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(IsDungeon);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            IsDungeon = reader.ReadBoolean();
        }

    }
}
