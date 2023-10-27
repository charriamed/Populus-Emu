using Stump.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace D2pReader.GeneralInformations
{
    public class D2pFile
    {
        #region Vars
        public const long START_POSITION_END_OFFSET = 16;
        private string _d2pFilePath;
        private Dictionary<uint, CompressedMap> _compressedMaps = new Dictionary<uint, CompressedMap>();
        #endregion

        #region Properties
        public string D2pFilePath
        {
            get { return _d2pFilePath; }
        }

        public Dictionary<uint, CompressedMap> CompressedMaps
        {
            get { return _compressedMaps; }
        }
        #endregion

        public D2pFile(string d2pFilePath)
        {
            if (Path.GetExtension(d2pFilePath) != ".d2p")
                throw new ArgumentException("Invalid file type, " + d2pFilePath + " is not a .d2p file");

            _d2pFilePath = d2pFilePath;

            using (var _reader = new BigEndianReader(File.OpenRead(_d2pFilePath)))
            {
                byte param1 = _reader.ReadByte();
                byte param2 = _reader.ReadByte();

                if ((param1 != 2) || (param2 != 1))
                    throw new ArgumentException("Invalid file header, " + _d2pFilePath + " is not a valid .d2p file");


                _reader.BaseStream.Position = (_reader.BaseStream.Length - START_POSITION_END_OFFSET);
                uint position = _reader.ReadUInt();
                int compressedMapsCount = (int)(_reader.ReadUInt());
                _reader.BaseStream.Position = (position);


                CompressedMap compressedMap = null;

                for (int i = 0; i <= compressedMapsCount; i++)
                {
                    compressedMap = new CompressedMap(_reader, _d2pFilePath);

                    if (compressedMap.IsInvalidMap)
                        continue;

                    _compressedMaps.Add(compressedMap.GetMapId(), compressedMap);
                }
            }
        }

        
    }
}
