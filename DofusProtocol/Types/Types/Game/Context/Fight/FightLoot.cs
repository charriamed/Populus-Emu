namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class FightLoot
    {
        public const short Id  = 41;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public uint[] Objects { get; set; }
        public ulong Kamas { get; set; }

        public FightLoot(uint[] objects, ulong kamas)
        {
            this.Objects = objects;
            this.Kamas = kamas;
        }

        public FightLoot() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Objects.Count());
            for (var objectsIndex = 0; objectsIndex < Objects.Count(); objectsIndex++)
            {
                writer.WriteVarUInt(Objects[objectsIndex]);
            }
            writer.WriteVarULong(Kamas);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            var objectsCount = reader.ReadUShort();
            Objects = new uint[objectsCount];
            for (var objectsIndex = 0; objectsIndex < objectsCount; objectsIndex++)
            {
                Objects[objectsIndex] = reader.ReadVarUInt();
            }
            Kamas = reader.ReadVarULong();
        }

    }
}
