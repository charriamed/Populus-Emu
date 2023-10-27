using System;

namespace Stump.Server.WorldServer.Database.World
{
    [Serializable]
    public class Cell
    {
        public const int StructSize = 11;
        public int versionMap;
        /// <summary>
        ///     Give a cell with Id = -1. This avoid using a class that take more memory space
        /// </summary>
        public static Cell Null = new Cell(0)
        {
            Id = -1
        };
        public Cell(int versionMap)
        {
            this.versionMap = versionMap;
        }

        public short Floor;
        public short Id;
        public byte LosMov;
        public byte MapChangeData;
        public uint MoveZone;
        public byte Speed;

        public bool Walkable => this.versionMap >= 9 ? (LosMov & 1) == 0 : (LosMov & 1) == 1;

        public bool LineOfSight => this.versionMap >= 9 ? (LosMov & 8) == 0 : (LosMov & 2) >> 1 == 1;

        public bool NonWalkableDuringFight => this.versionMap >= 9 ? (LosMov & 2) != 0 : (LosMov & 4) >> 2 == 1;
        //public bool NonWalkableDuringFight
        //{
        //    get
        //    {
        //        return (this.LosMov & 4) == 4;
        //    }
        //}
        public bool Red => this.versionMap >= 9 ? (LosMov & 32) != 0 : (LosMov & 8) >> 3 == 1;

        public bool Blue => this.versionMap >= 9 ? (LosMov & 16) != 0 : (LosMov & 16) >> 4 == 1;

        public bool FarmCell => this.versionMap >= 9 ? (LosMov & 128) != 0 : (LosMov & 32) >> 5 == 1;

        public bool Visible => this.versionMap >= 9 ? (LosMov & 64) != 0 : (LosMov & 64) >> 6 == 1;

        public bool NonWalkableDuringRP => this.versionMap >= 9 ? (LosMov & 4) != 0 : (LosMov & 128) >> 7 == 1;

        public bool IsRightChange => (MapChangeData & 1) > 0;

        public bool IsLeftChange => (MapChangeData & 16) > 0;

        public bool IsTopChange => (MapChangeData & 64) > 0;

        public bool IsBotChange => (MapChangeData & 4) > 0;

        public byte[] Serialize()
        {
            return new[]
            {
                (byte) (Id >> 8),
                (byte) (Id & 255),
                (byte) (Floor >> 8),
                (byte) (Floor & 255),
                LosMov,
                MapChangeData,
                Speed,
                (byte) (MoveZone >> 24),
                (byte) (MoveZone >> 16),
                (byte) (MoveZone >> 8),
                (byte) (MoveZone & 255u)
            };
        }

        public void Deserialize(byte[] data, int index = 0)
        {
            Id = (short)(data[index] << 8 | data[index + 1]);
            Floor = (short)(data[index + 2] << 8 | data[index + 3]);
            LosMov = data[index + 4];
            MapChangeData = data[index + 5];
            Speed = data[index + 6];
            MoveZone = (uint)(data[index + 7] << 24 | data[index + 8] << 16 | data[index + 9] << 8 | data[index + 10]);
        }
    }
}