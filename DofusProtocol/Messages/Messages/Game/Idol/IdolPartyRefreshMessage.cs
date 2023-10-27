namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class IdolPartyRefreshMessage : Message
    {
        public const uint Id = 6583;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PartyIdol PartyIdol { get; set; }

        public IdolPartyRefreshMessage(PartyIdol partyIdol)
        {
            this.PartyIdol = partyIdol;
        }

        public IdolPartyRefreshMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            PartyIdol.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            PartyIdol = new PartyIdol();
            PartyIdol.Deserialize(reader);
        }

    }
}
