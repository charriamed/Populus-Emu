namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class EntitiesPreset : Preset
    {
        public new const short Id = 545;
        public override short TypeId
        {
            get { return Id; }
        }
        public short IconId { get; set; }
        public ushort[] EntityIds { get; set; }

        public EntitiesPreset(short objectId, short iconId, ushort[] entityIds)
        {
            this.ObjectId = objectId;
            this.IconId = iconId;
            this.EntityIds = entityIds;
        }

        public EntitiesPreset() { }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(IconId);
            writer.WriteShort((short)EntityIds.Count());
            for (var entityIdsIndex = 0; entityIdsIndex < EntityIds.Count(); entityIdsIndex++)
            {
                writer.WriteVarUShort(EntityIds[entityIdsIndex]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            IconId = reader.ReadShort();
            var entityIdsCount = reader.ReadUShort();
            EntityIds = new ushort[entityIdsCount];
            for (var entityIdsIndex = 0; entityIdsIndex < entityIdsCount; entityIdsIndex++)
            {
                EntityIds[entityIdsIndex] = reader.ReadVarUShort();
            }
        }

    }
}
