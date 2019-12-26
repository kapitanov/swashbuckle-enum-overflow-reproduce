using System;

namespace Swashbuckle.AspNetCore.Bug
{
    [Flags]
    public enum Flags : uint
    {
        None = 0,

        Flag1 = 1,
        Flag2 = 1 << 1,
        Flag3 = 1 << 2,

        Max = uint.MaxValue,
    }
}