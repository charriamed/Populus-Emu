using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.DofusProtocol.D2oClasses.Tools.Bin
{
    public enum TransitionTypeEnum
    {
        UNSPECIFIED = 0,
        SCROLL = 1,
        SCROLL_ACTION = 2,
        MAP_EVENT = 4,
        MAP_ACTION = 8,
        MAP_OBSTACLE = 16,
        INTERACTIVE = 32,
        NPC_ACTION = 64
    }

    public static class TransitionType
    {

        public static TransitionTypeEnum FromName(string name)
        {
            switch (name)
            {
                case "SCROLL":
                    return TransitionTypeEnum.SCROLL;
                case "SCROLL_ACTION":
                    return TransitionTypeEnum.SCROLL_ACTION;
                case "MAP_EVENT":
                    return TransitionTypeEnum.MAP_EVENT;
                case "MAP_ACTION":
                    return TransitionTypeEnum.MAP_ACTION;
                case "MAP_OBSTACLE":
                    return TransitionTypeEnum.MAP_OBSTACLE;
                case "INTERACTIVE":
                    return TransitionTypeEnum.INTERACTIVE;
                case "NPC_ACTION":
                    return TransitionTypeEnum.NPC_ACTION;
                default:
                    return TransitionTypeEnum.UNSPECIFIED;
            }
        }
    }
}
