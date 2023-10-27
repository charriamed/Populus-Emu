using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum HaapiSessionTypeEnum {
        HAAPI_ACCOUNT_SESSION = 0,
        HAAPI_GAME_SESSION = 1
    }
}