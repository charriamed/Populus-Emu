namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class HouseInformationsForGuild : HouseInformations
    {
        public new const short Id = 170;
        public override short TypeId
        {
            get { return Id; }
        }
        public int InstanceId { get; set; }
        public bool SecondHand { get; set; }
        public string OwnerName { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public double MapId { get; set; }
        public ushort SubAreaId { get; set; }
        public int[] SkillListIds { get; set; }
        public uint GuildshareParams { get; set; }

        public HouseInformationsForGuild(uint houseId, ushort modelId, int instanceId, bool secondHand, string ownerName, short worldX, short worldY, double mapId, ushort subAreaId, int[] skillListIds, uint guildshareParams)
        {
            this.HouseId = houseId;
            this.ModelId = modelId;
            this.InstanceId = instanceId;
            this.SecondHand = secondHand;
            this.OwnerName = ownerName;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
            this.SkillListIds = skillListIds;
            this.GuildshareParams = guildshareParams;
        }

        public HouseInformationsForGuild() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(InstanceId);
            writer.WriteBoolean(SecondHand);
            writer.WriteUTF(OwnerName);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteDouble(MapId);
            writer.WriteVarUShort(SubAreaId);
            writer.WriteShort((short)SkillListIds.Count());
            for (var skillListIdsIndex = 0; skillListIdsIndex < SkillListIds.Count(); skillListIdsIndex++)
            {
                writer.WriteInt(SkillListIds[skillListIdsIndex]);
            }
            writer.WriteVarUInt(GuildshareParams);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            InstanceId = reader.ReadInt();
            SecondHand = reader.ReadBoolean();
            OwnerName = reader.ReadUTF();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            MapId = reader.ReadDouble();
            SubAreaId = reader.ReadVarUShort();
            var skillListIdsCount = reader.ReadUShort();
            SkillListIds = new int[skillListIdsCount];
            for (var skillListIdsIndex = 0; skillListIdsIndex < skillListIdsCount; skillListIdsIndex++)
            {
                SkillListIds[skillListIdsIndex] = reader.ReadInt();
            }
            GuildshareParams = reader.ReadVarUInt();
        }

    }
}
