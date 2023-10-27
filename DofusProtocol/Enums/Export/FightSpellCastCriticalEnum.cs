using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum FightSpellCastCriticalEnum {
        NORMAL = 1,
        CRITICAL_HIT = 2,
        CRITICAL_FAIL = 3
    }
}