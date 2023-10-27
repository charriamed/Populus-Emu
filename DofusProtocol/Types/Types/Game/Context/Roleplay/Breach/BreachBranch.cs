namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class BreachBranch
    {
        public const short Id  = 558;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public sbyte Room { get; set; }
        public int Element { get; set; }
        public MonsterInGroupLightInformations[] Bosses { get; set; }
        public double Map { get; set; }

        public BreachBranch(sbyte room, int element, MonsterInGroupLightInformations[] bosses, double map)
        {
            this.Room = room;
            this.Element = element;
            this.Bosses = bosses;
            this.Map = map;
        }

        public BreachBranch() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteSByte(Room);
            writer.WriteInt(Element);
            writer.WriteShort((short)Bosses.Count());
            for (var bossesIndex = 0; bossesIndex < Bosses.Count(); bossesIndex++)
            {
                var objectToSend = Bosses[bossesIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteDouble(Map);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            Room = reader.ReadSByte();
            Element = reader.ReadInt();
            var bossesCount = reader.ReadUShort();
            Bosses = new MonsterInGroupLightInformations[bossesCount];
            for (var bossesIndex = 0; bossesIndex < bossesCount; bossesIndex++)
            {
                var objectToAdd = new MonsterInGroupLightInformations();
                objectToAdd.Deserialize(reader);
                Bosses[bossesIndex] = objectToAdd;
            }
            Map = reader.ReadDouble();
        }

    }
}
