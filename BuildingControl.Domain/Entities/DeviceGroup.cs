using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingControl.Domain.Entities
{
    public class DeviceGroup
    {
        private readonly List<Device> devices = new();
        public IReadOnlyList<Device> Devices => devices;

        public string Name { get; private set; }

        public DeviceGroup(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Group name cannot be empty!", nameof(name));

            Name = name;
        }

        public void SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Group name cannot be empty!", nameof(newName));

            if (Name == newName) return;

            Name = newName;
        }

        public void AddDevice(Device device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            if (devices.Any(d => d.Id == device.Id)) throw new InvalidOperationException($"Device with id '{device.Id}' already exists in group '{Name}'!");

            devices.Add(device);
        }

        public Device RemoveDevice(string deviceId)
        {
            Device? device = devices.FirstOrDefault(d => d.Id == deviceId);

            if (device == null) throw new InvalidOperationException($"Device with id '{deviceId}' was not found in group '{Name}'!");

            devices.Remove(device);
            return device;
        }

        public Device? FindDevice(string deviceId)
        {
            return devices.FirstOrDefault(d => d.Id == deviceId);
        }
    }
}
