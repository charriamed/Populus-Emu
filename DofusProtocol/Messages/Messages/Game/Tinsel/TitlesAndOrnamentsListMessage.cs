namespace Stump.DofusProtocol.Messages
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class TitlesAndOrnamentsListMessage : Message
    {
        public const uint Id = 6367;
        public override uint MessageId
        {
            get { return Id; }
        }
        public ushort[] Titles { get; set; }
        public ushort[] Ornaments { get; set; }
        public ushort ActiveTitle { get; set; }
        public ushort ActiveOrnament { get; set; }

        public TitlesAndOrnamentsListMessage(ushort[] titles, ushort[] ornaments, ushort activeTitle, ushort activeOrnament)
        {
            this.Titles = titles;
            this.Ornaments = ornaments;
            this.ActiveTitle = activeTitle;
            this.ActiveOrnament = activeOrnament;
        }

        public TitlesAndOrnamentsListMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Titles.Count());
            for (var titlesIndex = 0; titlesIndex < Titles.Count(); titlesIndex++)
            {
                writer.WriteVarUShort(Titles[titlesIndex]);
            }
            writer.WriteShort((short)Ornaments.Count());
            for (var ornamentsIndex = 0; ornamentsIndex < Ornaments.Count(); ornamentsIndex++)
            {
                writer.WriteVarUShort(Ornaments[ornamentsIndex]);
            }
            writer.WriteVarUShort(ActiveTitle);
            writer.WriteVarUShort(ActiveOrnament);
        }

        public override void Deserialize(IDataReader reader)
        {
            var titlesCount = reader.ReadUShort();
            Titles = new ushort[titlesCount];
            for (var titlesIndex = 0; titlesIndex < titlesCount; titlesIndex++)
            {
                Titles[titlesIndex] = reader.ReadVarUShort();
            }
            var ornamentsCount = reader.ReadUShort();
            Ornaments = new ushort[ornamentsCount];
            for (var ornamentsIndex = 0; ornamentsIndex < ornamentsCount; ornamentsIndex++)
            {
                Ornaments[ornamentsIndex] = reader.ReadVarUShort();
            }
            ActiveTitle = reader.ReadVarUShort();
            ActiveOrnament = reader.ReadVarUShort();
        }

    }
}
