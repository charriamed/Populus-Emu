namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Enums;
    using Stump.Core.IO;

    [Serializable]
    public class IgnoredOnlineInformations : IgnoredInformations
    {
        public new const short Id = 105;
        public override short TypeId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }
        public string PlayerName { get; set; }
        public sbyte Breed { get; set; }
        public bool Sex { get; set; }

        public IgnoredOnlineInformations(int accountId, string accountName, ulong playerId, string playerName, sbyte breed, bool sex)
        {
            this.AccountId = accountId;
            this.AccountName = accountName;
            this.PlayerId = playerId;
            this.PlayerName = playerName;
            this.Breed = breed;
            this.Sex = sex;
        }

        public IgnoredOnlineInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarULong(PlayerId);
            writer.WriteUTF(PlayerName);
            writer.WriteSByte(Breed);
            writer.WriteBoolean(Sex);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            PlayerId = reader.ReadVarULong();
            PlayerName = reader.ReadUTF();
            Breed = reader.ReadSByte();
            Sex = reader.ReadBoolean();
        }

    }
}
