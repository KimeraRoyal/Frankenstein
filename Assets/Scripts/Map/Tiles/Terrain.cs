using System;

namespace Bodybuilding.Map.Tiles
{
    [Flags]
    public enum Terrain
    {
        Ground = 0x0,
        Water = 0x1,
        Hill = 0x2,
        Wall = 0x4
    }
}