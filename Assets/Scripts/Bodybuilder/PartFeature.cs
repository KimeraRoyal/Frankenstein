using System;

namespace Bodybuilder.Bodybuilder
{
    [Flags]
    public enum PartFeatures
    {
        None = 0x0,
        Walking = 0x1,
        Grabbing = 0x2,
        Climbing = 0x4,
        Head = 0x8,
        Swimming = 0x10,
        Flying = 0x20
    }
}