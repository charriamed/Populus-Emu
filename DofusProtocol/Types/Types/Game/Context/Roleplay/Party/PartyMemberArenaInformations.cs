namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class PartyMemberArenaInformations : PartyMemberInformations
    {
        public new const short Id = 391;
        public override short TypeId
        {
            get { return Id; }
        }
        public ushort Rank { get; set; }

        public PartyMemberArenaInformations(ulong objectId, string name, ushort level, EntityLook entityLook, sbyte breed, bool sex, uint lifePoints, uint maxLifePoints, ushort prospecting, byte regenRate, ushort initiative, sbyte alignmentSide, short worldX, short worldY, double mapId, ushort subAreaId, PlayerStatus status, PartyEntityBaseInformation[] entities, ushort rank)
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
            this.Rank = rank;
        }

        public PartyMemberArenaInformations() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarUShort(Rank);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Rank = reader.ReadVarUShort();
        }

    }
}
