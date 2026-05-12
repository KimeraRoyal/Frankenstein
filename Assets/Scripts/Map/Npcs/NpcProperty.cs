using System;

namespace Bodybuilder.Map.Npcs
{
    [Flags]
    public enum NpcProperty
    {
        None = 0x0,
        Swimming = 0x1,
        Climbing = 0x2,
        Flying = 0x4
    }
}