//using D2pReader;
//using D2pReader.MapInformations;
//using D2pReader.MapInformations.Elements;
//using Stump.Core.IO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Stump.DofusProtocol.D2oClasses.Tools.Dlm.Cells
//{
//    public class TacticalModeCell : Cell
//    {
//        public const uint CELL_HALF_WIDTH = 43;
//        public const float CELL_HALF_HEIGHT = 21.5F;
//        public TacticalModeCell(BigEndianReader _reader, sbyte mapVersion) : base(_reader, mapVersion)
//        {

//            GraphicalElement ge = null;
//            int i = 0;
//            _elementsCount = _reader.ReadShort();
//            for (i = 0; i < _elementsCount; i++)
//            {
//                ge = new GraphicalElement();
//                ge._elementId = _reader.ReadUInt();
//                ge._hue1 = _reader.ReadSByte();
//                ge._hue2 = _reader.ReadSByte();
//                ge._hue3 = _reader.ReadSByte();
//                ge._shadow1 = _reader.ReadSByte();
//                ge._shadow2 = _reader.ReadSByte();
//                ge._shadow3 = _reader.ReadSByte();

//                if (mapVersion <= 4)
//                {
//                    ge._offsetX = _reader.ReadSByte();
//                    ge._offsetY = _reader.ReadSByte();

//                    ge._pixelOffsetX = (int)(ge._offsetX * CELL_HALF_WIDTH);
//                    ge._pixelOffsetY = (int)(ge._offsetY * CELL_HALF_HEIGHT);
//                }

//                else
//                {
//                    ge._pixelOffsetX = _reader.ReadShort();
//                    ge._pixelOffsetY = _reader.ReadShort();

//                    ge._offsetX = (int)(ge._pixelOffsetX / CELL_HALF_WIDTH);
//                    ge._offsetY = (int)(ge._pixelOffsetY / CELL_HALF_HEIGHT);
//                }

//                ge._altitude = _reader.ReadSByte();
//                _elements.Add(ge);
//            }

//        }
//    }
//}
