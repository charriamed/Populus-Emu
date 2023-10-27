using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum PlayerLifeStatusEnum {
        STATUS_ALIVE_AND_KICKING = 0,
        STATUS_TOMBSTONE = 1,
        STATUS_PHANTOM = 2
    }
}