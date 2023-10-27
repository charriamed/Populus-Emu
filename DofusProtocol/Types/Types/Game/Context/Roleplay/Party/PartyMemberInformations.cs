namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyMemberInformations : CharacterBaseInformations
    {
        public new const short Id = 90;
        public override short TypeId
        {
            get { return Id; }
        }
        public uint LifePoints { get; set; }
        public uint MaxLifePoints { get; set; }
        public ushort Prospecting { get; set; }
        public byte RegenRate { get; set; }
        public ushort Initiative { get; set; }
        public sbyte AlignmentSide { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public double MapId { get; set; }
        public ushort SubAreaId { get; set; }
        public PlayerStatus Status { get; set; }
        public PartyEntityBaseInformation[] Entities { get; set; }

        public PartyMemberInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, bool sex, uint lifePoints, uint maxLifePoints, ushort prospecting, byte regenRate, ushort initiative, sbyte alignmentSide, short worldX, short worldY, double mapId, ushort subAreaId, PlayerStatus status, PartyEntityBaseInformation[] entities)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.EntityLook = entityLook;
            this.Breed = breed;
            this.Sex = sex;
            this.LifePoints = lifePoints;
            this.MaxLifePoints = maxLifePoints;
            this.Prospecting = prospecting;
            this.RegenRate = regenRate;
            this.Initiative = initiative;
            this.AlignmentSide = alignmentSide;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
            this.Status = status;
            this.Entities = entities;
        }

        public PartyMemberInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUInt(LifePoints);
            writer.WriteVarUInt(MaxLifePoints);
            writer.WriteVarUShort(Prospecting);
            writer.WriteByte(RegenRate);
            writer.WriteVarUShort(Initiative);
            writer.WriteSByte(AlignmentSide);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteDouble(MapId);
            writer.WriteVarUShort(SubAreaId);
            writer.WriteShort(Status.TypeId);
            Status.Serialize(writer);
            writer.WriteShort((short)Entities.Count());
            for (var entitiesIndex = 0; entitiesIndex < Entities.Count(); entitiesIndex++)
            {
                var objectToSend = Entities[entitiesIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            LifePoints = reader.ReadVarUInt();
            MaxLifePoints = reader.ReadVarUInt();
            Prospecting = reader.ReadVarUShort();
            RegenRate = reader.ReadByte();
            Initiative = reader.ReadVarUShort();
            AlignmentSide = reader.ReadSByte();
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            MapId = reader.ReadDouble();
            SubAreaId = reader.ReadVarUShort();
            Status = ProtocolTypeManager.GetInstance<PlayerStatus>(reader.ReadShort());
            Status.Deserialize(reader);
            var entitiesCount = reader.ReadUShort();
            Entities = new PartyEntityBaseInformation[entitiesCount];
            for (var entitiesIndex = 0; entitiesIndex < entitiesCount; entitiesIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<PartyEntityBaseInformation>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Entities[entitiesIndex] = objectToAdd;
            }
        }

    }
}
