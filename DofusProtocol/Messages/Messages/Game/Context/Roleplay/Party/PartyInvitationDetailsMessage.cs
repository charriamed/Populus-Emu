namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyInvitationDetailsMessage : AbstractPartyMessage
    {
        public new const uint Id = 6263;
        public override uint MessageId
        {
            get { return Id; }
        }
        public sbyte PartyType { get; set; }
        public string PartyName { get; set; }
        public ulong FromId { get; set; }
        public string FromName { get; set; }
        public ulong LeaderId { get; set; }
        public PartyInvitationMemberInformations[] Members { get; set; }
        public PartyGuestInformations[] Guests { get; set; }

        public PartyInvitationDetailsMessage(uint partyId, sbyte partyType, string partyName, ulong fromId, string fromName, ulong leaderId, PartyInvitationMemberInformations[] members, PartyGuestInformations[] guests)
        {
            this.PartyId = partyId;
            this.PartyType = partyType;
            this.PartyName = partyName;
            this.FromId = fromId;
            this.FromName = fromName;
            this.LeaderId = leaderId;
            this.Members = members;
            this.Guests = guests;
        }

        public PartyInvitationDetailsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(PartyType);
            writer.WriteUTF(PartyName);
            writer.WriteVarULong(FromId);
            writer.WriteUTF(FromName);
            writer.WriteVarULong(LeaderId);
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
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PartyType = reader.ReadSByte();
            PartyName = reader.ReadUTF();
            FromId = reader.ReadVarULong();
            FromName = reader.ReadUTF();
            LeaderId = reader.ReadVarULong();
            var membersCount = reader.ReadUShort();
            Members = new PartyInvitationMemberInformations[membersCount];
            for (var membersIndex = 0; membersIndex < membersCount; membersIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<PartyInvitationMemberInformations>(reader.ReadShort());
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
        }

    }
}
