using Stump.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace D2pReader.MapInformations
{
    public class Layer
    {
        #region Vars
        private int _layerId;
        private int _cellsCount;
        private List<Cell> _cells;
        private int ElementId;
        #endregion

        #region Properties
        public int LayerId
        {
            get { return _layerId; }
        }

        public int CellsCount
        {
            get { return _cellsCount; }
        }

        public List<Cell> Cells
        {
            get { return _cells; }
        }
        #endregion


        public Layer(BigEndianReader _reader, sbyte mapVersion, MapManager map)
        {
            if (mapVersion >= 9)
            {
                this._layerId = _reader.ReadSByte();
            }
            else
            {
                this._layerId = _reader.ReadInt();
            }
            //_layerId = _reader.ReadInt();
            _cellsCount = _reader.ReadShort();

            _cells = new List<Cell>();
            List<byte> elements = new List<byte>();
            for (int i = 0; i < _cellsCount; i++)
            { 
                var cell = new Cell(_reader, mapVersion, map, elements);
                _cells.Add(cell);
               // elements = cell.ElementCompressed;
            }
            //map.m_compressedElements.AddRange(ZipHelper.Compress(elements.ToArray()));
        }
    }
}
