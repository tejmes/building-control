using BuildingControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingControl.Domain.Events
{
    public class DeviceChangedEventArgs : EventArgs
    {
        public Device Device { get; }

        public DeviceChangedEventArgs(Device device)
        {
            Device = device;
        }
    }
}
