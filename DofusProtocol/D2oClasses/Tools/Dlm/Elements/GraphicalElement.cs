using Stump.Core.IO;

namespace D2pReader.MapInformations.Elements
{
    public class GraphicalElement : BasicElement
    {
        #region Vars
        public const uint CELL_HALF_WIDTH = 43;
        public const float CELL_HALF_HEIGHT = 21.5F;

        private uint _elementId;
        private int _hue1;
        private int _hue2;
        private int _hue3;
        private int _shadow1;
        private int _shadow2;
        private int _shadow3;
        private int _offsetX;
        private int _offsetY;
        private int _pixelOffsetX;
        private int _pixelOffsetY;
        private int _altitude;
        private uint _identifier;
        #endregion

        #region Properties
        public uint ElementId
        {
            get { return _elementId; }
        }

        public int Hue1
        {
            get { return _hue1; }
        }

        public int Hue2
        {
            get { return _hue2; }
        }

        public int Hue3
        {
            get { return _hue3; }
        }

        public int Shadow1
        {
            get { return _shadow1; }
        }

        public int Shadow2
        {
            get { return _shadow2; }
        }

        public int Shadow3
        {
            get { return _shadow3; }
        }

        public int OffsetX
        {
            get { return _offsetX; }
        }

        public int OffsetY
        {
            get { return _offsetY; }
        }

        public int PixelOffsetX
        {
            get { return _pixelOffsetX; }
        }

        public int PixelOffsetY
        {
            get { return _pixelOffsetY; }
        }

        public int Altitude
        {
            get { return _altitude; }
        }

        public uint Identifier
        {
            get { return _identifier; }
        }


        #endregion


        public GraphicalElement(BigEndianReader _reader, sbyte mapVersion)
        {
            _elementId = _reader.ReadUInt();
            _hue1 = _reader.ReadSByte();
            _hue2 = _reader.ReadSByte();
            _hue3 = _reader.ReadSByte();
            _shadow1 = _reader.ReadSByte();
            _shadow2 = _reader.ReadSByte();
            _shadow3 = _reader.ReadSByte();

            if (mapVersion <= 4)
            {
                _offsetX = _reader.ReadSByte();
                _offsetY = _reader.ReadSByte();

                _pixelOffsetX = (int)(_offsetX * CELL_HALF_WIDTH);
                _pixelOffsetY = (int)(_offsetY * CELL_HALF_HEIGHT);
            }

            else
            {
                _pixelOffsetX = _reader.ReadShort();
                _pixelOffsetY = _reader.ReadShort();

                _offsetX = (int)(_pixelOffsetX / CELL_HALF_WIDTH);
                _offsetY = (int)(_pixelOffsetY / CELL_HALF_HEIGHT);
            }

            _altitude = _reader.ReadSByte();
            _identifier = _reader.ReadUInt();
            
        }
    }
}
