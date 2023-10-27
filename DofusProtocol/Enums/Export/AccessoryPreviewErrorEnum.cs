using System;

namespace Stump.DofusProtocol.Enums {
    [Flags]
    public enum AccessoryPreviewErrorEnum {
        PREVIEW_ERROR = 0,
        PREVIEW_COOLDOWN = 1,
        PREVIEW_BAD_ITEM = 2
    }
}