using Stump.Core.IO;

namespace D2pReader.GeneralInformations
{
    public class CompressedMap
    {
        #region Vars
        private string _d2pFilePath;
        private string _indexName;
        private uint _offset;
        private uint _bytesCount;
        private bool _isInvalidMap;

        private BigEndianReader _reader;
        #endregion

        #region Properties
        public string D2pFilePath
        {
            get { return _d2pFilePath; }
        }

        public string IndexName
        {
            get { return _indexName; }
        }

        public uint Offset
        {
            get { return _offset; }
        }

        public uint BytesCount
        {
            get { return _bytesCount; }
        }

        public bool IsInvalidMap
        {
            get { return _isInvalidMap; }
        }
        #endregion


        public CompressedMap(BigEndianReader reader, string d2pFilePath)
        {
            _reader = reader;
            _d2pFilePath = d2pFilePath;

            ReadMapInformation();
        }



        public uint GetMapId()
        {
            return uint.Parse(_indexName.Substring(_indexName.IndexOf('/') + 1).Replace(".dlm", ""));
        }

        private void ReadMapInformation()
        {
            _indexName = _reader.ReadUTF();

            if (_indexName == "link" || _indexName == "")
            {
                _isInvalidMap = true;
                return;
            }

            _offset = _reader.ReadUInt() + 2;
            _bytesCount = _reader.ReadUInt();
        }
    }
}
