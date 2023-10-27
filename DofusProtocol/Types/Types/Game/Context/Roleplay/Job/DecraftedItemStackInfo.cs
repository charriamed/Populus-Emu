namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class DecraftedItemStackInfo
    {
        public const short Id  = 481;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint ObjectUID { get; set; }
        public float BonusMin { get; set; }
        public float BonusMax { get; set; }
        public ushort[] RunesId { get; set; }
        public uint[] RunesQty { get; set; }

        public DecraftedItemStackInfo(uint objectUID, float bonusMin, float bonusMax, ushort[] runesId, uint[] runesQty)
        {
            this.ObjectUID = objectUID;
            this.BonusMin = bonusMin;
            this.BonusMax = bonusMax;
            this.RunesId = runesId;
            this.RunesQty = runesQty;
        }

        public DecraftedItemStackInfo() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteVarUInt(ObjectUID);
            writer.WriteFloat(BonusMin);
            writer.WriteFloat(BonusMax);
            writer.WriteShort((short)RunesId.Count());
            for (var runesIdIndex = 0; runesIdIndex < RunesId.Count(); runesIdIndex++)
            {
                writer.WriteVarUShort(RunesId[runesIdIndex]);
            }
            writer.WriteShort((short)RunesQty.Count());
            for (var runesQtyIndex = 0; runesQtyIndex < RunesQty.Count(); runesQtyIndex++)
            {
                writer.WriteVarUInt(RunesQty[runesQtyIndex]);
            }
        }

        public virtual void Deserialize(IDataReader reader)
        {
            ObjectUID = reader.ReadVarUInt();
            BonusMin = reader.ReadFloat();
            BonusMax = reader.ReadFloat();
            var runesIdCount = reader.ReadUShort();
            RunesId = new ushort[runesIdCount];
            for (var runesIdIndex = 0; runesIdIndex < runesIdCount; runesIdIndex++)
            {
                RunesId[runesIdIndex] = reader.ReadVarUShort();
            }
            var runesQtyCount = reader.ReadUShort();
            RunesQty = new uint[runesQtyCount];
            for (var runesQtyIndex = 0; runesQtyIndex < runesQtyCount; runesQtyIndex++)
            {
                RunesQty[runesQtyIndex] = reader.ReadVarUInt();
            }
        }

    }
}
