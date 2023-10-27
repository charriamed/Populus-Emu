using Stump.Core.IO;

namespace D2pReader.MapInformations.Elements
{
    public class SoundElement : BasicElement
    {
        #region Vars
        private int _soundId;
        private int _baseVolume;
        private int _fullVolumeDistance;
        private int _nullVolumeDistance;
        private int _minDelayBetweenLoops;
        private int _maxDelayBetweenLoops;
        #endregion

        #region Properties
        public int SoundId
        {
            get { return _soundId; }
        }

        public int BaseVolume
        {
            get { return _baseVolume; }
        }

        public int FullVolumeDistance
        {
            get { return _fullVolumeDistance; }
        }

        public int NullVolumeDistance
        {
            get { return _nullVolumeDistance; }
        }

        public int MinDelayBetweenLoops
        {
            get { return _minDelayBetweenLoops; }
        }

        public int MaxDelayBetweenLoops
        {
            get { return _maxDelayBetweenLoops; }
        }


        #endregion


        public SoundElement(BigEndianReader _reader)
        {
            _soundId = _reader.ReadInt();
            _baseVolume = _reader.ReadShort();
            _fullVolumeDistance = _reader.ReadInt();
            _nullVolumeDistance = _reader.ReadInt();
            _minDelayBetweenLoops = _reader.ReadShort();
            _maxDelayBetweenLoops = _reader.ReadShort();
        }
    }
}
