namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class HouseInformationsForSell
    {
        public const short Id  = 221;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public int InstanceId { get; set; }
        public bool SecondHand { get; set; }
        public uint ModelId { get; set; }
        public string OwnerName { get; set; }
        public bool OwnerConnected { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public ushort SubAreaId { get; set; }
        public sbyte NbRoom { get; set; }
        public sbyte NbChest { get; set; }
        public int[] SkillListIds { get; set; }
        public bool IsLocked { get; set; }
        public ulong Price { get; set; }

        public HouseInformationsForSell(int instanceId, bool secondHand, uint modelId, string ownerName, bool ownerConnected, short worldX, short worldY, ushort subAreaId, sbyte nbRoom, sbyte nbChest, int[] skillListIds, bool isLocked, ulong price)
        {
            this.InstanceId = instanceId;
            this.SecondHand = secondHand;
            this.ModelId = modelId;
            this.OwnerName = ownerName;
            this.OwnerConnected = ownerConnected;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.SubAreaId = subAreaId;
            this.NbRoom = nbRoom;
            this.NbChest = nbChest;
            this.SkillListIds = skillListIds;
            this.IsLocked = isLocked;
            this.Price = price;
        }

        public HouseInformationsForSell() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteInt(InstanceId);
            writer.WriteBoolean(SecondHand);
            writer.WriteVarUInt(ModelId);
            writer.WriteUTF(OwnerName);
            writer.WriteBoolean(OwnerConnected);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteVarUShort(SubAreaId);
            writer.WriteSByte(NbRoom);
            writer.WriteSByte(NbChest);
            writer.WriteShort((short)SkillListIds.Count());
            for (var skillListIdsIndex = 0; skillListIdsIndex < SkillListIds.Count(); skillListIdsIndex++)
            {
                writer.WriteInt(SkillListIds[skillListIdsIndex]);
            }
            writer.WriteBoolean(IsLocked);
            writer.WriteVarULong(Price);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            InstanceId = reader.ReadInt();
            SecondHand = reader.ReadBoolean();
            ModelId = reader.ReadVarUInt();
            OwnerName = reader.ReadUTF();
            OwnerConnected = reader.ReadBoolean();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            SubAreaId = reader.ReadVarUShort();
            NbRoom = reader.ReadSByte();
            NbChest = reader.ReadSByte();
            var skillListIdsCount = reader.ReadUShort();
            SkillListIds = new int[skillListIdsCount];
            for (var skillListIdsIndex = 0; skillListIdsIndex < skillListIdsCount; skillListIdsIndex++)
            {
                SkillListIds[skillListIdsIndex] = reader.ReadInt();
            }
            IsLocked = reader.ReadBoolean();
            Price = reader.ReadVarULong();
        }

    }
}
