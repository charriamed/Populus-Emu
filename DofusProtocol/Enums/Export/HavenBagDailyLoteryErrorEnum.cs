using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum HavenBagDailyLoteryErrorEnum {
        HAVENBAG_DAILY_LOTERY_OK = 0,
        HAVENBAG_DAILY_LOTERY_ALREADYUSED = 1,
        HAVENBAG_DAILY_LOTERY_ERROR = 2
    }
}