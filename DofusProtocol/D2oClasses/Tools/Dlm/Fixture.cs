using Stump.Core.IO;

namespace D2pReader.MapInformations
{
    public class Fixture
    {
        #region Vars
        private int _fixtureId;
        private int _offsetX;
        private int _offsetY;
        private int _rotation;
        private int _xScale;
        private int _yScale;
        private int _redMultiplier;
        private int _greenMultiplier;
        private int _blueMultiplier;
        private int _hue;
        private uint _alpha;
        #endregion

        #region Properties
        public int FixtureId
        {
            get { return _fixtureId; }
        }

        public int OffsetX
        {
            get { return _offsetX; }
        }

        public int OffsetY
        {
            get { return _offsetY; }
        }

        public int Rotation
        {
            get { return _rotation; }
        }

        public int XScale
        {
            get { return _xScale; }
        }

        public int YScale
        {
            get { return _yScale; }
        }

        public int RedMultiplier
        {
            get { return _redMultiplier; }
        }

        public int GreenMultiplier
        {
            get { return _greenMultiplier; }
        }

        public int BlueMultiplier
        {
            get { return _blueMultiplier; }
        }

        public int Hue
        {
            get { return _hue; }
        }

        public uint Alpha
        {
            get { return _alpha; }
        }
        #endregion


        public Fixture(BigEndianReader _reader)
        {
            _fixtureId = _reader.ReadInt();
            _offsetX = _reader.ReadShort();
            _offsetY = _reader.ReadShort();
            _rotation = _reader.ReadShort();
            _xScale = _reader.ReadShort();
            _yScale = _reader.ReadShort();
            _redMultiplier = _reader.ReadSByte();
            _greenMultiplier = _reader.ReadSByte();
            _blueMultiplier = _reader.ReadSByte();
            _hue = _redMultiplier | _greenMultiplier | _blueMultiplier;
            _alpha = _reader.ReadByte();
        }
    }
}
