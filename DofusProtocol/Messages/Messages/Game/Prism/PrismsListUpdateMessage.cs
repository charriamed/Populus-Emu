namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PrismsListUpdateMessage : PrismsListMessage
    {
        public new const uint Id = 6438;
        public override uint MessageId
        {
            get { return Id; }
        }

        public PrismsListUpdateMessage(PrismSubareaEmptyInfo[] prisms)
        {
            this.Prisms = prisms;
        }

        public PrismsListUpdateMessage() { }

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
