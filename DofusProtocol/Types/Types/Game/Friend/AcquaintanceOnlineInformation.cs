namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Stump.Core.IO;

    [Serializable]
    public class AcquaintanceOnlineInformation : AcquaintanceInformation
    {
        public new const short Id = 562;
        public override short TypeId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }
        public string PlayerName { get; set; }
        public ushort MoodSmileyId { get; set; }
        public PlayerStatus Status { get; set; }

        public AcquaintanceOnlineInformation(int accountId, string accountName, sbyte playerState, ulong playerId, string playerName, ushort moodSmileyId, PlayerStatus status)
        {
            this.AccountId = accountId;
            this.AccountName = accountName;
            this.PlayerState = playerState;
            this.PlayerId = playerId;
            this.PlayerName = playerName;
            this.MoodSmileyId = moodSmileyId;
            this.Status = status;
        }

        public AcquaintanceOnlineInformation() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(PlayerId);
            writer.WriteUTF(PlayerName);
            writer.WriteVarUShort(MoodSmileyId);
            writer.WriteShort(Status.TypeId);
            Status.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerId = reader.ReadVarULong();
            PlayerName = reader.ReadUTF();
            MoodSmileyId = reader.ReadVarUShort();
            Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
            Status.Deserialize(reader);
        }

    }
}
