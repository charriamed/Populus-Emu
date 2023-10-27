namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyInvitationMemberInformations : CharacterBaseInformations
    {
        public new const short Id = 376;
        public override short TypeId
        {
            get { return Id; }
        }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public double MapId { get; set; }
        public ushort SubAreaId { get; set; }
        public PartyEntityBaseInformation[] Entities { get; set; }

        public PartyInvitationMemberInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, bool sex, short worldX, short worldY, double mapId, ushort subAreaId, PartyEntityBaseInformation[] entities)
        {
            this.ObjectId = objectId;
            this.Name = name;
            this.Level = level;
            this.EntityLook = entityLook;
            this.Breed = breed;
            this.Sex = sex;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.MapId = mapId;
            this.SubAreaId = subAreaId;
            this.Entities = entities;
        }

        public PartyInvitationMemberInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteDouble(MapId);
            writer.WriteVarUShort(SubAreaId);
            writer.WriteShort((short)Entities.Count());
            for (var entitiesIndex = 0; entitiesIndex < Entities.Count(); entitiesIndex++)
            {
                var objectToSend = Entities[entitiesIndex];
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            MapId = reader.ReadDouble();
            SubAreaId = reader.ReadVarUShort();
            var entitiesCount = reader.ReadUShort();
            Entities = new PartyEntityBaseInformation[entitiesCount];
            for (var entitiesIndex = 0; entitiesIndex < entitiesCount; entitiesIndex++)
            {
                var objectToAdd = new PartyEntityBaseInformation();
                objectToAdd.Deserialize(reader);
                Entities[entitiesIndex] = objectToAdd;
            }
        }

    }
}
