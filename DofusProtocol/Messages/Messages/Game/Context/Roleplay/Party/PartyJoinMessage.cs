namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyJoinMessage : AbstractPartyMessage
    {
        public new const uint Id = 5576;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte PartyType { get; set; }
        public ulong PartyLeaderId { get; set; }
        public sbyte MaxParticipants { get; set; }
        public PartyMemberInformations[] Members { get; set; }
        public PartyGuestInformations[] Guests { get; set; }
        public bool Restricted { get; set; }
        public string PartyName { get; set; }

        public PartyJoinMessage(uint partyId, sbyte partyType, ulong partyLeaderId, sbyte maxParticipants, PartyMemberInformations[] members, PartyGuestInformations[] guests, bool restricted, string partyName)
        {
            this.PartyId = partyId;
            this.PartyType = partyType;
            this.PartyLeaderId = partyLeaderId;
            this.MaxParticipants = maxParticipants;
            this.Members = members;
            this.Guests = guests;
            this.Restricted = restricted;
            this.PartyName = partyName;
        }

        public PartyJoinMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(PartyType);
            writer.WriteVarULong(PartyLeaderId);
            writer.WriteSByte(MaxParticipants);
            writer.WriteShort((short)Members.Count());
            for (var membersIndex = 0; membersIndex < Members.Count(); membersIndex++)
            {
                var objectToSend = Members[membersIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
            writer.WriteShort((short)Guests.Count());
            for (var guestsIndex = 0; guestsIndex < Guests.Count(); guestsIndex++)
            {
                var objectToSend = Guests[guestsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteBoolean(Restricted);
            writer.WriteUTF(PartyName);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PartyType = reader.ReadSByte();
            PartyLeaderId = reader.ReadVarULong();
            MaxParticipants = reader.ReadSByte();
            var membersCount = reader.ReadUShort();
            Members = new PartyMemberInformations[membersCount];
            for (var membersIndex = 0; membersIndex < membersCount; membersIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<PartyMemberInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Members[membersIndex] = objectToAdd;
            }
            var guestsCount = reader.ReadUShort();
            Guests = new PartyGuestInformations[guestsCount];
            for (var guestsIndex = 0; guestsIndex < guestsCount; guestsIndex++)
            {
                var objectToAdd = new PartyGuestInformations();
                objectToAdd.Deserialize(reader);
                Guests[guestsIndex] = objectToAdd;
            }
            Restricted = reader.ReadBoolean();
            PartyName = reader.ReadUTF();
        }

    }
}
