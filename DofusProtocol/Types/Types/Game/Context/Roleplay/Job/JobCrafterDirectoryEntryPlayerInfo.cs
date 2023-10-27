namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using Enums;
    using Stump.Core.IO;

    [Serializable]
    public class JobCrafterDirectoryEntryPlayerInfo
    {
        public const short Id  = 194;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public ulong PlayerId { get; set; }
        public string PlayerName { get; set; }
        public sbyte AlignmentSide { get; set; }
        public sbyte Breed { get; set; }
        public bool Sex { get; set; }
        public bool IsInWorkshop { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public double MapId { get; set; }
        public ushort SubAreaId { get; set; }
        public PlayerStatus Status { get; set; }

        public JobCrafterDirectoryEntryPlayerInfo(ulong playerId, string playerName, sbyte alignmentSide, sbyte breed, bool sex, bool isInWorkshop, short worldX, short worldY, double mapId, ushort subAreaId, PlayerStatus status)
        {
            this.PlayerId = playerId;
            this.PlayerName = playerName;
            this.AlignmentSide = alignmentSide;
            this.Breed = breed;
            this.Sex = sex;
            this.IsInWorkshop = isInWorkshop;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
            this.Status = status;
        }

        public JobCrafterDirectoryEntryPlayerInfo() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarULong(PlayerId);
            writer.WriteUTF(PlayerName);
            writer.WriteSByte(AlignmentSide);
            writer.WriteSByte(Breed);
            writer.WriteBoolean(Sex);
            writer.WriteBoolean(IsInWorkshop);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteDouble(MapId);
            writer.WriteVarUShort(SubAreaId);
            writer.WriteShort(Status.TypeId);
            Status.Serialize(writer);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            PlayerId = reader.ReadVarULong();
            PlayerName = reader.ReadUTF();
            AlignmentSide = reader.ReadSByte();
            Breed = reader.ReadSByte();
            Sex = reader.ReadBoolean();
            IsInWorkshop = reader.ReadBoolean();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            MapId = reader.ReadDouble();
            SubAreaId = reader.ReadVarUShort();
            Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
            Status.Deserialize(reader);
        }

    }
}
