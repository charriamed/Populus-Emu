namespace Stump.DofusProtocol.Types
{
    using System;
    using System.Linq;
    using System.Text;
    using Stump.DofusProtocol.Types;
    using System.Collections.Generic;
    using Stump.Core.IO;

    [Serializable]
    public class GameActionMark
    {
        public const short Id  = 351;
        public virtual short TypeId
        {
            get { return Id; }
        }
        public double MarkAuthorId { get; set; }
        public sbyte MarkTeamId { get; set; }
        public int MarkSpellId { get; set; }
        public short MarkSpellLevel { get; set; }
        public short MarkId { get; set; }
        public sbyte MarkType { get; set; }
        public short MarkimpactCell { get; set; }
        public GameActionMarkedCell[] Cells { get; set; }
        public bool Active { get; set; }

        public GameActionMark(double markAuthorId, sbyte markTeamId, int markSpellId, short markSpellLevel, short markId, sbyte markType, short markimpactCell, GameActionMarkedCell[] cells, bool active)
        {
            this.MarkAuthorId = markAuthorId;
            this.MarkTeamId = markTeamId;
            this.MarkSpellId = markSpellId;
            this.MarkSpellLevel = markSpellLevel;
            this.MarkId = markId;
            this.MarkType = markType;
            this.MarkimpactCell = markimpactCell;
            this.Cells = cells;
            this.Active = active;
        }

        public GameActionMark() { }

        public virtual void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(MarkAuthorId);
            writer.WriteSByte(MarkTeamId);
            writer.WriteInt(MarkSpellId);
            writer.WriteShort(MarkSpellLevel);
            writer.WriteShort(MarkId);
            writer.WriteSByte(MarkType);
            writer.WriteShort(MarkimpactCell);
            writer.WriteShort((short)Cells.Count());
            for (var cellsIndex = 0; cellsIndex < Cells.Count(); cellsIndex++)
            {
                var objectToSend = Cells[cellsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteBoolean(Active);
        }

        public virtual void Deserialize(IDataReader reader)
        {
            MarkAuthorId = reader.ReadDouble();
            MarkTeamId = reader.ReadSByte();
            MarkSpellId = reader.ReadInt();
            MarkSpellLevel = reader.ReadShort();
            MarkId = reader.ReadShort();
            MarkType = reader.ReadSByte();
            MarkimpactCell = reader.ReadShort();
            var cellsCount = reader.ReadUShort();
            Cells = new GameActionMarkedCell[cellsCount];
            for (var cellsIndex = 0; cellsIndex < cellsCount; cellsIndex++)
            {
                var objectToAdd = new GameActionMarkedCell();
                objectToAdd.Deserialize(reader);
                Cells[cellsIndex] = objectToAdd;
            }
            Active = reader.ReadBoolean();
        }

    }
}
