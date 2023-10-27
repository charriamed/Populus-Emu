namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class TaxCollectorInformations
    {
        public const short Id  = 167;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double UniqueId { get; set; }
        public ushort FirtNameId { get; set; }
        public ushort LastNameId { get; set; }
        public AdditionalTaxCollectorInformations AdditionalInfos { get; set; }
        public short WorldX { get; set; }
        public short WorldY { get; set; }
        public ushort SubAreaId { get; set; }
        public sbyte State { get; set; }
        public EntityLook Look { get; set; }
        public TaxCollectorComplementaryInformations[] Complements { get; set; }

        public TaxCollectorInformations(double uniqueId, ushort firtNameId, ushort lastNameId, AdditionalTaxCollectorInformations additionalInfos, short worldX, short worldY, ushort subAreaId, sbyte state, EntityLook look, TaxCollectorComplementaryInformations[] complements)
        {
            this.UniqueId = uniqueId;
            this.FirtNameId = firtNameId;
            this.LastNameId = lastNameId;
            this.AdditionalInfos = additionalInfos;
            this.WorldX = worldX;
            this.WorldY = worldY;
            this.SubAreaId = subAreaId;
            this.State = state;
            this.Look = look;
            this.Complements = complements;
        }

        public TaxCollectorInformations() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(UniqueId);
            writer.WriteVarUShort(FirtNameId);
            writer.WriteVarUShort(LastNameId);
            AdditionalInfos.Serialize(writer);
            writer.WriteShort(WorldX);
            writer.WriteShort(WorldY);
            writer.WriteVarUShort(SubAreaId);
            writer.WriteSByte(State);
            Look.Serialize(writer);
            writer.WriteShort((short)Complements.Count());
            for (var complementsIndex = 0; complementsIndex < Complements.Count(); complementsIndex++)
            {
                var objectToSend = Complements[complementsIndex];
                writer.WriteShort(objectToSend.TypeId);
                objectToSend.Serialize(writer);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            UniqueId = reader.ReadDouble();
            FirtNameId = reader.ReadVarUShort();
            LastNameId = reader.ReadVarUShort();
            AdditionalInfos = new AdditionalTaxCollectorInformations();
            AdditionalInfos.Deserialize(reader);
            WorldX = reader.ReadShort();
            WorldY = reader.ReadShort();
            SubAreaId = reader.ReadVarUShort();
            State = reader.ReadSByte();
            Look = new EntityLook();
            Look.Deserialize(reader);
            var complementsCount = reader.ReadUShort();
            Complements = new TaxCollectorComplementaryInformations[complementsCount];
            for (var complementsIndex = 0; complementsIndex < complementsCount; complementsIndex++)
            {
                var objectToAdd = ProtocolTypeManager.GetInstance<TaxCollectorComplementaryInformations>(reader.ReadShort());
                objectToAdd.Deserialize(reader);
                Complements[complementsIndex] = objectToAdd;
            }
        }

    }
}
