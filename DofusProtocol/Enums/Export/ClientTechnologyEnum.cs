using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum ClientTechnologyEnum {
        CLIENT_TECHNOLOGY_UNKNOWN = 0,
        CLIENT_AIR = 1,
        CLIENT_FLASH = 2
    }
}