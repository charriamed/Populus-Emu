namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PartyFollowStatusUpdateMessage : AbstractPartyMessage
    {
        public new const uint Id = 5581;
        public override uint MessageId
        {
            get { return Id; }
        }
        public bool Success { get; set; }
        public bool IsFollowed { get; set; }
        public ulong FollowedId { get; set; }

        public PartyFollowStatusUpdateMessage(uint partyId, bool success, bool isFollowed, ulong followedId)
        {
            this.PartyId = partyId;
            this.Success = success;
            this.IsFollowed = isFollowed;
            this.FollowedId = followedId;
        }

        public PartyFollowStatusUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            var flag = new byte();
            flag = BooleanByteWrapper.SetFlag(flag, 0, Success);
            flag = BooleanByteWrapper.SetFlag(flag, 1, IsFollowed);
            writer.WriteByte(flag);
            writer.WriteVarULong(FollowedId);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            var flag = reader.ReadByte();
            Success = BooleanByteWrapper.GetFlag(flag, 0);
            IsFollowed = BooleanByteWrapper.GetFlag(flag, 1);
            FollowedId = reader.ReadVarULong();
        }

    }
}
