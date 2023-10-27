namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Enums;
    using Stump.Core.IO;

    [Serializable]
    public class DungeonPartyFinderPlayer
    {
        public const short Id  = 373;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }
        public string PlayerName { get; set; }
        public sbyte Breed { get; set; }
        public bool Sex { get; set; }
        public ushort Level { get; set; }

        public DungeonPartyFinderPlayer(ulong playerId, string playerName, sbyte breed, bool sex, ushort level)
        {
            this.PlayerId = playerId;
            this.PlayerName = playerName;
            this.Breed = breed;
            this.Sex = sex;
            this.Level = level;
        }

        public DungeonPartyFinderPlayer() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(PlayerId);
            writer.WriteUTF(PlayerName);
            writer.WriteSByte(Breed);
            writer.WriteBoolean(Sex);
            writer.WriteVarUShort(Level);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            PlayerId = reader.ReadVarULong();
            PlayerName = reader.ReadUTF();
            Breed = reader.ReadSByte();
            Sex = reader.ReadBoolean();
            Level = reader.ReadVarUShort();
        }

    }
}
