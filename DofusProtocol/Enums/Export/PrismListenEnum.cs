using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum PrismListenEnum {
        PRISM_LISTEN_NONE = 0,
        PRISM_LISTEN_MINE = 1,
        PRISM_LISTEN_ALL = 2
    }
}