namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class AlliancePartialListMessage : AllianceListMessage
    {
        public new const uint Id = 6427;
        public override uint MessageId
        {
            get { return Id; }
        }

        public AlliancePartialListMessage(AllianceFactSheetInformations[] alliances)
        {
            this.Alliances = alliances;
        }

        public AlliancePartialListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }

    }
}
