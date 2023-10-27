using Stump.Core.IO;
using System;

namespace D2pReader.MapInformations
{
    public class CellData
    {
        #region Vars
        private int _floor;
        private int _losmov;
        private int _speed;
        private uint _mapChangeData;
        private uint _moveZone;
        private int _arrow;
        private int _linkedZone;
        private int _id;
        private bool _los;
        private bool _mov;
        private bool _visible;
        private bool _farmCell;
        private bool _blue;
        private bool _red;
        private bool _nonWalkableDuringRP;
        private bool _nonWalkableDuringFight;
        private bool _havenbagCell;
        #endregion

        #region Properties
        public int Id
        {
            get { return _id; }
        }
        public int Speed
        {
            get { return _speed; }
        }

        public uint MapChangeData
        {
            get { return _mapChangeData; }
        }

        public uint MoveZone
        {
            get { return _moveZone; }
        }

        public int Losmov
        {
            get { return _losmov; }
        }

        public int Floor
        {
            get { return _floor; }
        }

        public int Arrow
        {
            get { return _arrow; }
        }


        public bool Los
        {
            get { return _los; }
        }

        public bool Mov
        {
            get { return _mov; }
        }

        public bool Visible
        {
            get { return _visible; }
        }

        public bool FarmCell
        {
            get { return _farmCell; }
        }

        public bool Blue
        {
            get { return _blue; }
        }

        public bool Red
        {
            get { return _red; }
        }

        public bool NonWalkableDuringRP
        {
            get { return _nonWalkableDuringRP; }
        }

        public bool NonWalkableDuringFight
        {
            get { return _nonWalkableDuringFight; }
        }
        public bool HavenBagCell
        {
            get { return _havenbagCell; }
        }
        public bool HasLinkedZoneRP()
      {
         return this._mov && !this._farmCell;
      }
        public int LinkedZoneRP()
      {
         return (this._linkedZone & 240) >> 4;
        }
        public bool HasLinkedZoneFight()
      {
         return this._mov && !this._nonWalkableDuringFight && !this._farmCell && !this._havenbagCell;
      }

        public int LinkedZoneFight()
      {
         return this._linkedZone & 15;
      }
    #endregion

    public byte[] Serialize()
        {

            return new byte[]
            {
                (byte)(this._id >> 8),
                (byte)(this._id & 255),
                (byte)(this.Floor >> 8),
                (byte)(this.Floor & 255),
                (byte)this.Losmov,
                (byte)this.MapChangeData,
                (byte)this.Speed,
                (byte)(this.MoveZone >> 24),
                (byte)(this.MoveZone >> 16),
                (byte)(this.MoveZone >> 8),
                (byte)(this.MoveZone & 255u)
            };
        }
        public CellData(BigEndianReader _reader, sbyte mapVersion, int id)
        {
            int tmpbytesv9 = 0;
            bool topArrow = false;
            bool bottomArrow = false;
            bool rightArrow = false;
            bool leftArrow = false;
            int tmpBits = 0;
            _floor = _reader.ReadSByte() * 10;
            _id = id;

            if (_floor == -1280)
            {
                return;
            }
            if (mapVersion >= 9)
            {
                _losmov = tmpbytesv9 = _reader.ReadShort();
                this._mov = (tmpbytesv9 & 1) == 0;
                this._nonWalkableDuringFight = (tmpbytesv9 & 2) != 0;
                this._nonWalkableDuringRP = (tmpbytesv9 & 4) != 0;
                this._los = (tmpbytesv9 & 8) == 0;
                this._blue = (tmpbytesv9 & 16) != 0;
                this._red = (tmpbytesv9 & 32) != 0;
                this._visible = (tmpbytesv9 & 64) != 0;
                this._farmCell = (tmpbytesv9 & 128) != 0;
                if (mapVersion >= 10)
                {
                    this._havenbagCell = (tmpbytesv9 & 256) != 0;
                    topArrow = (tmpbytesv9 & 512) != 0;
                    bottomArrow = (tmpbytesv9 & 1024) != 0;
                    rightArrow = (tmpbytesv9 & 2048) != 0;
                    leftArrow = (tmpbytesv9 & 4096) != 0;
                }
                else
                {
                    topArrow = (tmpbytesv9 & 256) != 0;
                    bottomArrow = (tmpbytesv9 & 512) != 0;
                    rightArrow = (tmpbytesv9 & 1024) != 0;
                    leftArrow = (tmpbytesv9 & 2048) != 0;
                }

            }
            else
            {
                this._losmov = (int)_reader.ReadByte();
                this._los = (this._losmov & 2) >> 1 == 1;
                this._mov = (this._losmov & 1) == 1;
                this._visible = (this._losmov & 64) >> 6 == 1;
                this._farmCell = (this._losmov & 32) >> 5 == 1;
                this._blue = (this._losmov & 16) >> 4 == 1;
                this._red = (this._losmov & 8) >> 3 == 1;
                this._nonWalkableDuringRP = (this._losmov & 128) >> 7 == 1;
                this._nonWalkableDuringFight = (this._losmov & 4) >> 2 == 1;
            }
            this._speed = _reader.ReadSByte();
            this._mapChangeData = (uint)_reader.ReadSByte();
            if (mapVersion > 5)
            {
                this._moveZone = (uint)_reader.ReadByte();
            }
            if(mapVersion > 10 && (this.HasLinkedZoneRP() || this.HasLinkedZoneFight()))
            {
                this._linkedZone = _reader.ReadByte();
            }
            if (mapVersion > 7 && mapVersion < 9)
            {
                tmpBits = _reader.ReadByte();
                this._arrow = 15 & tmpBits;
            }
        }
    }
}
