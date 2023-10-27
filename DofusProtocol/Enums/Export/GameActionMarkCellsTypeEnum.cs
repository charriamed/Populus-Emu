using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum GameActionMarkCellsTypeEnum {
        CELLS_CIRCLE = 0,
        CELLS_CROSS = 1,
        CELLS_SQUARE = 2
    }
}