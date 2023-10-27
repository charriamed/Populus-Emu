namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyInvitationDungeonDetailsMessage : PartyInvitationDetailsMessage
    {
        public new const uint Id = 6262;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }
        public bool[] PlayersDungeonReady { get; set; }

        public PartyInvitationDungeonDetailsMessage(uint partyId, sbyte partyType, string partyName, ulong fromId, string fromName, ulong leaderId, PartyInvitationMemberInformations[] members, PartyGuestInformations[] guests, ushort dungeonId, bool[] playersDungeonReady)
        {
            this.PartyId = partyId;
            this.PartyType = partyType;
            this.PartyName = partyName;
            this.FromId = fromId;
            this.FromName = fromName;
            this.LeaderId = leaderId;
            this.Members = members;
            this.Guests = guests;
            this.DungeonId = dungeonId;
            this.PlayersDungeonReady = playersDungeonReady;
        }

        public PartyInvitationDungeonDetailsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(DungeonId);
            writer.WriteShort((short)PlayersDungeonReady.Count());
            for (var playersDungeonReadyIndex = 0; playersDungeonReadyIndex < PlayersDungeonReady.Count(); playersDungeonReadyIndex++)
            {
                writer.WriteBoolean(PlayersDungeonReady[playersDungeonReadyIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            DungeonId = reader.ReadVarUShort();
            var playersDungeonReadyCount = reader.ReadUShort();
            PlayersDungeonReady = new bool[playersDungeonReadyCount];
            for (var playersDungeonReadyIndex = 0; playersDungeonReadyIndex < playersDungeonReadyCount; playersDungeonReadyIndex++)
            {
                PlayersDungeonReady[playersDungeonReadyIndex] = reader.ReadBoolean();
            }
        }

    }
}
