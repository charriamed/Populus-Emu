using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stump.DofusProtocol.D2oClasses.Tools.Bin
{
    public enum DirectionEnum
    {
        INVALID = -1,
        EAST = 0,
        SOUTH_EAST = 1,
        SOUTH = 2,
        SOUTH_WEST = 3,
        WEST = 4,
        NORTH_WEST = 5,
        NORTH = 6,
        NORTH_EAST = 7,
        INVALID_2 = 255,

    }

    public static class Direction
    {
        public static bool IsValidDirection(int direction)
        {
            return (DirectionEnum)direction >= DirectionEnum.EAST && (DirectionEnum)direction <= DirectionEnum.NORTH_EAST;
        }

        public static DirectionEnum FromName(string name)
        {
            switch (name)
            {
                case "EAST":
                    return DirectionEnum.EAST;
                case "SOUTH_EAST":
                    return DirectionEnum.SOUTH_EAST;
                case "SOUTH":
                    return DirectionEnum.SOUTH;
                case "SOUTH_WEST":
                    return DirectionEnum.SOUTH_WEST;
                case "WEST":
                    return DirectionEnum.WEST;
                case "NORTH_WEST":
                    return DirectionEnum.NORTH_WEST;
                case "NORTH":
                    return DirectionEnum.NORTH;
                case "NORTH_EAST":
                    return DirectionEnum.NORTH_EAST;
                default:
                    return DirectionEnum.INVALID;
            }
        }
    }
}
