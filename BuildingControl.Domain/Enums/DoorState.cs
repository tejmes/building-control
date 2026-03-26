using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingControl.Domain.Enums
{
    [Flags]
    public enum DoorState
    {
        None = 0,
        Locked = 1,
        Open = 2,
        OpenForTooLong = 4,
        OpenedForcibly = 8
    }
}
