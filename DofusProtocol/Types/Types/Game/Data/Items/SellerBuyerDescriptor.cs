namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class SellerBuyerDescriptor
    {
        public const short Id  = 121;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint[] Quantities { get; set; }
        public uint[] Types { get; set; }
        public float TaxPercentage { get; set; }
        public float TaxModificationPercentage { get; set; }
        public byte MaxItemLevel { get; set; }
        public uint MaxItemPerAccount { get; set; }
        public int NpcContextualId { get; set; }
        public ushort UnsoldDelay { get; set; }

        public SellerBuyerDescriptor(uint[] quantities, uint[] types, float taxPercentage, float taxModificationPercentage, byte maxItemLevel, uint maxItemPerAccount, int npcContextualId, ushort unsoldDelay)
        {
            this.Quantities = quantities;
            this.Types = types;
            this.TaxPercentage = taxPercentage;
            this.TaxModificationPercentage = taxModificationPercentage;
            this.MaxItemLevel = maxItemLevel;
            this.MaxItemPerAccount = maxItemPerAccount;
            this.NpcContextualId = npcContextualId;
            this.UnsoldDelay = unsoldDelay;
        }

        public SellerBuyerDescriptor() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Quantities.Count());
            for (var quantitiesIndex = 0; quantitiesIndex < Quantities.Count(); quantitiesIndex++)
            {
                writer.WriteVarUInt(Quantities[quantitiesIndex]);
            }
            writer.WriteShort((short)Types.Count());
            for (var typesIndex = 0; typesIndex < Types.Count(); typesIndex++)
            {
                writer.WriteVarUInt(Types[typesIndex]);
            }
            writer.WriteFloat(TaxPercentage);
            writer.WriteFloat(TaxModificationPercentage);
            writer.WriteByte(MaxItemLevel);
            writer.WriteVarUInt(MaxItemPerAccount);
            writer.WriteInt(NpcContextualId);
            writer.WriteVarUShort(UnsoldDelay);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            var quantitiesCount = reader.ReadUShort();
            Quantities = new uint[quantitiesCount];
            for (var quantitiesIndex = 0; quantitiesIndex < quantitiesCount; quantitiesIndex++)
            {
                Quantities[quantitiesIndex] = reader.ReadVarUInt();
            }
            var typesCount = reader.ReadUShort();
            Types = new uint[typesCount];
            for (var typesIndex = 0; typesIndex < typesCount; typesIndex++)
            {
                Types[typesIndex] = reader.ReadVarUInt();
            }
            TaxPercentage = reader.ReadFloat();
            TaxModificationPercentage = reader.ReadFloat();
            MaxItemLevel = reader.ReadByte();
            MaxItemPerAccount = reader.ReadVarUInt();
            NpcContextualId = reader.ReadInt();
            UnsoldDelay = reader.ReadVarUShort();
        }

    }
}
