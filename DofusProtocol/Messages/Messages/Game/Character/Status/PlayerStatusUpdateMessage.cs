namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class PlayerStatusUpdateMessage : Message
    {
        public const uint Id = 6386;
        public override uint MessageId
        {
            get { return Id; }
        }
        public int AccountId { get; set; }
        public ulong PlayerId { get; set; }
        public PlayerStatus Status { get; set; }

        public PlayerStatusUpdateMessage(int accountId, ulong playerId, PlayerStatus status)
        {
            this.AccountId = accountId;
            this.PlayerId = playerId;
            this.Status = status;
        }

        public PlayerStatusUpdateMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(AccountId);
            writer.WriteVarULong(PlayerId);
            writer.WriteShort(Status.TypeId);
            Status.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            AccountId = reader.ReadInt();
            PlayerId = reader.ReadVarULong();
            Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
            Status.Deserialize(reader);
        }

    }
}
