using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum FightDispellableEnum {
        DISPELLABLE = 1,
        DISPELLABLE_BY_DEATH = 2,
        DISPELLABLE_BY_STRONG_DISPEL = 3,
        REALLY_NOT_DISPELLABLE = 4,
        NOT_DISPELLABLE = 5
    }
}