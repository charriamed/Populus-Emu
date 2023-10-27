using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

using D2pReader.GeneralInformations;
using Stump.Core.IO;

namespace D2pReader.MapInformations
{
    public class Map
    {
        #region Vars
        private BigEndianReader _reader;

        public const string DefaultEncryptionKeyString = "649ae451ca33ec53bbcbcc33becf15f4";
        private byte[] _encryptionKey;
        private byte[] m_compressedCells;


        public const int MAP_CELLS_COUNT = 560;

        public MapManager map
        {
            get;
            set;
        }

        private int _mapVersion;
        private bool _encrypted;
        private uint _encryptionVersion;
        private int _groundCRC;
        private int _zoomScale = 1;
        private int _zoomOffsetX;
        private int _zoomOffsetY;
        private int _groundCacheCurrentlyUsed = 0;
        private uint _id;
        private uint _relativeId;
        private int _mapType;
        private int _backgroundsCount;
        private List<Fixture> _backgroundFixtures;
        private int _foregroundsCount;
        private List<Fixture> _foregroundFixtures;
        private int _subareaId;
        private uint _shadowBonusOnEntities;
        private long _backgroundAlpha;
        private uint _backgroundColor;
        private int _backgroundRed;
        private int _backgroundGreen;
        private int _backgroundBlue;
        private long _gridColor;
        private int _topNeighbourId;
        private int _bottomNeighbourId;
        private int _leftNeighbourId;
        private int _rightNeighbourId;
        private bool _useLowPassFilter;
        private bool _useReverb;
        private int _presetId;
        private int _cellsCount;
        private int _layersCount;
        private bool _isUsingNewMovementSystem = false;
        private List<Layer> _layers;
        private List<CellData> _cells;
        private WorldPoint _position;
        private string _hashCode;
        private int _tacticalModeTemplateId;
        #endregion

        #region Properties
        public int MapVersion
        {
            get { return _mapVersion; }
        }

        public bool Encrypted
        {
            get { return _encrypted; }
        }

        public uint EncryptionVersion
        {
            get { return _encryptionVersion; }
        }

        public int GroundCRC
        {
            get { return _groundCRC; }
        }

        public int ZoomScale
        {
            get { return _zoomScale; }
        }

        public int ZoomOffsetX
        {
            get { return _zoomOffsetX; }
        }

        public int ZoomOffsetY
        {
            get { return _zoomOffsetY; }
        }

        public int GroundCacheCurrentlyUsed
        {
            get { return _groundCacheCurrentlyUsed; }
        }

        public uint Id
        {
            get { return _id; }
        }

        public uint RelativeId
        {
            get { return _relativeId; }
        }

        public int MapType
        {
            get { return _mapType; }
        }

        public int BackgroundsCount
        {
            get { return _backgroundsCount; }
        }

        public List<Fixture> BackgroundFixtures
        {
            get { return _backgroundFixtures; }
        }

        public int ForegroundsCount
        {
            get { return _foregroundsCount; }
        }

        public List<Fixture> ForegroundFixtures
        {
            get { return _foregroundFixtures; }
        }

        public int SubareaId
        {
            get { return _subareaId; }
        }

        public uint ShadowBonusOnEntities
        {
            get { return _shadowBonusOnEntities; }
        }

        public uint BackgroundColor
        {
            get { return _backgroundColor; }
        }

        public int BackgroundRed
        {
            get { return _backgroundRed; }
        }

        public int BackgroundGreen
        {
            get { return _backgroundGreen; }
        }

        public int BackgroundBlue
        {
            get { return _backgroundBlue; }
        }

        public int TopNeighbourId
        {
            get { return _topNeighbourId; }
        }

        public int BottomNeighbourId
        {
            get { return _bottomNeighbourId; }
        }

        public int LeftNeighbourId
        {
            get { return _leftNeighbourId; }
        }

        public int RightNeighbourId
        {
            get { return _rightNeighbourId; }
        }

        public bool UseLowPassFilter
        {
            get { return _useLowPassFilter; }
        }

        public bool UseReverb
        {
            get { return _useReverb; }
        }

        public int PresetId
        {
            get { return _presetId; }
        }

        public int CellsCount
        {
            get { return _cellsCount; }
        }

        public int LayersCount
        {
            get { return _layersCount; }
        }

        public bool IsUsingNewMovementSystem
        {
            get { return _isUsingNewMovementSystem; }
        }

        public List<Layer> Layers
        {
            get { return _layers; }
        }

        public List<CellData> Cells
        {
            get { return _cells; }
        }

        public WorldPoint Position
        {
            get { return _position; }
        }

        public string HashCode
        {
            get { return _hashCode; }
        }
        public int TacticalModeTemplateId
        {
            get { return _tacticalModeTemplateId; }
        }
           
        #endregion


        public Map(CompressedMap compressedMap, MapManager map)
        {
            InitializeReader(compressedMap);

            _encryptionKey = Encoding.UTF8.GetBytes(DefaultEncryptionKeyString);
            InitializeMap(_encryptionKey, map);

            _reader.Dispose();
        }



        private void InitializeReader(CompressedMap compressedMap)
        {
            FileStream compressedMapStream = new FileStream(compressedMap.D2pFilePath, FileMode.Open, FileAccess.Read);
            BinaryReader compressedMapReader = new BinaryReader(compressedMapStream);
            compressedMapReader.BaseStream.Position = compressedMap.Offset;

            byte[] compressedMapBuffer = compressedMapReader.ReadBytes((int)compressedMap.BytesCount);
            byte[] compressedMapBufferWithoutHeader = new byte[compressedMapBuffer.Length - 2];
            Array.Copy(compressedMapBuffer, 2, compressedMapBufferWithoutHeader, 0, compressedMapBuffer.Length - 2);

            StringBuilder mapHashCodeBuilder = new StringBuilder();
            byte[] compressedMapMd5Buffer = MD5.Create().ComputeHash(compressedMapBufferWithoutHeader);

            for (int i = 0; i < compressedMapMd5Buffer.Length; i++)
                mapHashCodeBuilder.Append(compressedMapMd5Buffer[i].ToString("X2"));

            _hashCode = mapHashCodeBuilder.ToString();


            MemoryStream decompressMapStream = new MemoryStream(compressedMapBufferWithoutHeader);
            DeflateStream mapDeflateStream = new DeflateStream(decompressMapStream, CompressionMode.Decompress);

            _reader = new BigEndianReader(mapDeflateStream);
            compressedMapStream.Close();
        }

        private void InitializeMap(byte[] encryptionKey, MapManager map)
        {
            int readColor = 0;
            long gridAlpha = 0;
            int gridRed = 0;
            int gridGreen = 0;
            int gridBlue = 0;
            this.map = map;
            int header = _reader.ReadSByte();
            int dataLen = 0;
            int i = 0;
            byte[] decryptionKey = encryptionKey;

            if (header != 77)
                throw new FormatException("Unknown file header, first byte must be 77");


            _mapVersion = this._reader.ReadSByte();
            _id = this._reader.ReadUInt();

            if (_mapVersion >= 7)
            {
                _encrypted = _reader.ReadBoolean();
                _encryptionVersion = (uint)_reader.ReadSByte();
                dataLen = this._reader.ReadInt();

                if (_encrypted)
                {
                    byte[] encryptedData = _reader.ReadBytes(dataLen);

                    for (i = 0; i < encryptedData.Length; i++)
                        encryptedData[i] = (byte)(encryptedData[i] ^ decryptionKey[i % decryptionKey.Length]);

                    _reader = new BigEndianReader(new MemoryStream(encryptedData));
                }
            }

            _relativeId = _reader.ReadUInt();
            _position = new WorldPoint(_relativeId);

            _mapType = _reader.ReadSByte();
            _subareaId = _reader.ReadInt();
            _topNeighbourId = _reader.ReadInt();
            _bottomNeighbourId = _reader.ReadInt();
            _leftNeighbourId = _reader.ReadInt();
            _rightNeighbourId = _reader.ReadInt();
            _shadowBonusOnEntities = _reader.ReadUInt();

            if (_mapVersion >= 9)
            {
                readColor = _reader.ReadInt();
                this._backgroundAlpha = (readColor & 4278190080) >> 32;
                this._backgroundRed = (readColor & 16711680) >> 16;
                this._backgroundGreen = (readColor & 65280) >> 8;
                this._backgroundBlue = readColor & 255;
                readColor = (int)_reader.ReadUInt();
                gridAlpha = (readColor & 4278190080) >> 32;
                gridRed = (readColor & 16711680) >> 16;
                gridGreen = (readColor & 65280) >> 8;
                gridBlue = readColor & 255;
                this._gridColor = ((int)gridAlpha & 255) << 32 | (gridRed & 255) << 16 | (gridGreen & 255) << 8 | gridBlue & 255;
            }
            else if (_mapVersion >= 3)
            {
                _backgroundRed = _reader.ReadSByte();
                _backgroundGreen = _reader.ReadSByte();
                _backgroundBlue = _reader.ReadSByte();

            }
            _backgroundColor = (uint)((_backgroundRed & 255) << 16 | (_backgroundGreen & 255) << 8 | _backgroundBlue & 255);
            if (_mapVersion >= 4)
            {
                _zoomScale = (ushort)(_reader.ReadUShort() / 100);
                _zoomOffsetX = _reader.ReadShort();
                _zoomOffsetY = _reader.ReadShort();
                if (_zoomScale < 1)
                {
                    this._zoomScale = 1;
                    this._zoomOffsetX = this._zoomOffsetY = 0;
                }
            }
            if (_mapVersion > 10)
            {
                this._tacticalModeTemplateId = _reader.ReadInt();
            }

            _useLowPassFilter = _reader.ReadSByte() == 1;
            _useReverb = _reader.ReadSByte() == 1;

            if (_useReverb == true)
                _presetId = _reader.ReadInt();
            else
                _presetId = -1;


            _backgroundsCount = _reader.ReadSByte();

            _backgroundFixtures = new List<Fixture>();

            for (i = 0; i < _backgroundsCount; i++)
            {
                Fixture backgroundFixture = new Fixture(_reader);
                _backgroundFixtures.Add(backgroundFixture);
            }


            _foregroundsCount = _reader.ReadSByte();

            _foregroundFixtures = new List<Fixture>();

            for (i = 0; i < _foregroundsCount; i++)
            {
                Fixture foregroundFixture = new Fixture(_reader);
                _foregroundFixtures.Add(foregroundFixture);
            }


            _reader.ReadInt();

            _groundCRC = _reader.ReadInt();
            _layersCount = _reader.ReadSByte();
            _layers = new List<Layer>();
            map.m_compressedElements = new List<byte>();
            for (i = 0; i < _layersCount; i++)
            {
                Layer layer = new Layer(_reader, (sbyte)_mapVersion, map);
                _layers.Add(layer);
            }

            _cellsCount = MAP_CELLS_COUNT;
            _cells = new CellData[_cellsCount].ToList();
            this.m_compressedCells = new byte[_cells.ToArray().Length * 11];
            for (i = 0; i < _cellsCount; i++)
            {


                CellData cell = new CellData(_reader, (sbyte)_mapVersion, i);
                _cells[i] = cell;
                //System.Array.Copy(_cells[i].Serialize(), 0, m_compressedCells, i * 11, 11);
                // _cells.Add(cell);

            }
            /* for (i = 0; i < _cellsCount; i++)
             {
                 this.m_compressedCells = _cells.ToArray()[i].Serialize();
             }*/
           // this.map.m_compressedCells = ZipHelper.Compress(this.m_compressedCells);
        }
    }
}
