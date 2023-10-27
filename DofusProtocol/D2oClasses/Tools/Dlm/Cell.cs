using System.Collections.Generic;
using D2pReader.MapInformations.Elements;
using System.Linq;
using Stump.DofusProtocol.D2oClasses.Tools.Ele;
using System.IO;
using System;
using Stump.Core.IO;

namespace D2pReader.MapInformations
{
    public class Cell
    {
        #region Vars
        private int _cellId;
        private int _elementsCount;
        private int counterElement;
        private List<byte> m_compressedElements;
        private List<BasicElement> _elements;

        #endregion

        #region Properties
        public int CellId// but the id is here on Cell.cs
        {
            get { return _cellId; }
        }

        public int ElementsCount
        {
            get { return counterElement; }
        }

        public List<BasicElement> Elements
        {
            get { return _elements; }
        }
        public List<byte> ElementCompressed
        {
            get
            {
                return m_compressedElements;
            }
        }
        #endregion


        public Cell(BigEndianReader _reader, sbyte mapVersion, MapManager map, List<byte> elements)
        {
            _cellId = _reader.ReadShort();
            _elementsCount = _reader.ReadShort();
            _elements = new List<BasicElement>();
            m_compressedElements = elements;

            for (int i = 0; i < _elementsCount; i++)
            {
                sbyte elementType = _reader.ReadSByte();
                var element = BasicElement.GetElementFromType(elementType, _reader, mapVersion);
                //_elements.Add(BasicElement.GetElementFromType(elementType, _reader, mapVersion));
                _elements.Add(element);
                if (BasicElement.ElementId != 0)
                {
                    counterElement++;
                    //this.m_compressedElements.AddRange(element.Serialize(_cellId, (int)BasicElement.ElementId));
                    //System.Array.Copy(element.Serialize(_cellId, (int)BasicElement.ElementId), 0, m_compressedElements, counter * 6, 6);
                }
            }
            
        }
        
    }
}
