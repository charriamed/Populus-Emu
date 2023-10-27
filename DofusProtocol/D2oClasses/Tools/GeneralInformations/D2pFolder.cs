using System;
using System.Collections.Generic;
using System.IO;

namespace D2pReader.GeneralInformations
{
    public class D2pFolder
    {
        #region Vars
        private string _d2pFolderPath;
        private List<D2pFile> _d2pFolderContent;
        private uint _d2pFilesCount;
        #endregion

        #region Properties
        public string FolderPath
        {
            get { return _d2pFolderPath; }
        }

        public List<D2pFile> FolderContent
        {
            get { return _d2pFolderContent; }
        }

        public uint D2pFilesCount
        {
            get { return _d2pFilesCount; }
        }
        #endregion

        public D2pFolder(string d2pFolderPath)
        {
            _d2pFolderPath = d2pFolderPath;
            _d2pFolderContent = new List<D2pFile>();
            _d2pFilesCount = 0;

            Initialize();
        }


        private void Initialize()
        {
            if (!Directory.Exists(_d2pFolderPath))
                throw new IOException("Directory " + _d2pFolderPath + " does not exist");

            foreach (string d2pFile in Directory.GetFiles(_d2pFolderPath))
            {
                if (Path.GetExtension(d2pFile) != ".d2p")
                    continue;

                _d2pFolderContent.Add(new D2pFile(d2pFile));
                _d2pFilesCount++;
            }

            if (_d2pFilesCount == 0)
                new ArgumentException("Invalid folder, no .d2p files found in " + _d2pFolderPath);
        }
    }
}
