namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyLocateMembersMessage : AbstractPartyMessage
    {
        public new const uint Id = 5595;
        public override uint MessageId
        {
            get { return Id; }
        }
        public PartyMemberGeoPosition[] Geopositions { get; set; }

        public PartyLocateMembersMessage(uint partyId, PartyMemberGeoPosition[] geopositions)
        {
            this.PartyId = partyId;
            this.Geopositions = geopositions;
        }

        public PartyLocateMembersMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)Geopositions.Count());
            for (var geopositionsIndex = 0; geopositionsIndex < Geopositions.Count(); geopositionsIndex++)
            {
                var objectToSend = Geopositions[geopositionsIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var geopositionsCount = reader.ReadUShort();
            Geopositions = new PartyMemberGeoPosition[geopositionsCount];
            for (var geopositionsIndex = 0; geopositionsIndex < geopositionsCount; geopositionsIndex++)
            {
                var objectToAdd = new PartyMemberGeoPosition();
                objectToAdd.Deserialize(reader);
                Geopositions[geopositionsIndex] = objectToAdd;
            }
        }

    }
}
