//using Stump.Core.IO;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Stump.DofusProtocol.D2oClasses.Tools.Dlm.Cells
//{
//    public class TacticalModeTemplate
//    {
//     #region Variables
//        public int id;

//        public int backgroundColor;

//        public List<TacticalModeCell> groundCells;

//        public List<TacticalModeCell> lineOfSightCells;
      
//     #endregion

//        public TacticalModeTemplate(BigEndianReader _reader)
//        {
//            int i = 0;
//            TacticalModeCell cell = null;
//            int numGroundCells = 0;
//            int numLineOfSightCells = 0;

//            id = _reader.ReadShort();
//            backgroundColor = _reader.ReadInt();
//            numGroundCells = _reader.ReadSByte();
//            groundCells = new List<TacticalModeCell>();
//            for (i = 0; i < numGroundCells; i++)
//            {
//                cell = new TacticalModeCell(_reader, 11);
//                this.groundCells.Add(cell);
//            }
//            numLineOfSightCells = _reader.ReadSByte();
//            this.lineOfSightCells = new List<TacticalModeCell>();
//            for (i = 0; i < numLineOfSightCells; i++)
//            {
//                cell = new TacticalModeCell(_reader, 11);
//                this.lineOfSightCells.Add(cell);
//            }


//        }
//    }
//}
