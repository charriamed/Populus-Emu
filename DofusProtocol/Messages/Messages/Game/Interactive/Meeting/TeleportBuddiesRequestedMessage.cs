namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class TeleportBuddiesRequestedMessage : Message
    {
        public const uint Id = 6302;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort DungeonId { get; set; }
        public ulong InviterId { get; set; }
        public ulong[] InvalidBuddiesIds { get; set; }

        public TeleportBuddiesRequestedMessage(ushort dungeonId, ulong inviterId, ulong[] invalidBuddiesIds)
        {
            this.DungeonId = dungeonId;
            this.InviterId = inviterId;
            this.InvalidBuddiesIds = invalidBuddiesIds;
        }

        public TeleportBuddiesRequestedMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarUShort(DungeonId);
            writer.WriteVarULong(InviterId);
            writer.WriteShort((short)InvalidBuddiesIds.Count());
            for (var invalidBuddiesIdsIndex = 0; invalidBuddiesIdsIndex < InvalidBuddiesIds.Count(); invalidBuddiesIdsIndex++)
            {
                writer.WriteVarULong(InvalidBuddiesIds[invalidBuddiesIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            DungeonId = reader.ReadVarUShort();
            InviterId = reader.ReadVarULong();
            var invalidBuddiesIdsCount = reader.ReadUShort();
            InvalidBuddiesIds = new ulong[invalidBuddiesIdsCount];
            for (var invalidBuddiesIdsIndex = 0; invalidBuddiesIdsIndex < invalidBuddiesIdsCount; invalidBuddiesIdsIndex++)
            {
                InvalidBuddiesIds[invalidBuddiesIdsIndex] = reader.ReadVarULong();
            }
        }

    }
}
