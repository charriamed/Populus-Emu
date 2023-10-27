using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum GameActionFightInvisibilityStateEnum {
        INVISIBLE = 1,
        DETECTED = 2,
        VISIBLE = 3
    }
}