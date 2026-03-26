using BuildingControl.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingControl.Domain.Entities
{
    public class DeviceTree
    {
        private readonly List<DeviceGroup> groups = new();
        public IReadOnlyList<DeviceGroup> Groups => groups;

        public event EventHandler<TreeStructureChangedEventArgs>? StructureChanged;
        public event EventHandler<DeviceChangedEventArgs>? DeviceChanged;

        public void AddGroup(DeviceGroup group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));

            if (groups.Any(g => string.Equals(g.Name, group.Name, StringComparison.OrdinalIgnoreCase))) throw new InvalidOperationException($"Group '{group.Name}' already exists!");

            groups.Add(group);
            RaiseStructureChanged($"Group '{group.Name}' added.");
        }

        public void RemoveGroup(string groupName)
        {
            DeviceGroup group = FindGroup(groupName) ?? throw new InvalidOperationException($"Group '{groupName}' was not found!");

            foreach (Device device in group.Devices)
            {
                UnsubscribeFromDevice(device);
            }

            groups.Remove(group);
            RaiseStructureChanged($"Group '{groupName}' removed.");
        }

        public void AddDeviceToGroup(string groupName, Device device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            if (FindDevice(device.Id) != null) throw new InvalidOperationException($"Device with id '{device.Id}' already exists in the tree!");

            DeviceGroup group = FindGroup(groupName) ?? throw new InvalidOperationException($"Group '{groupName}' was not found!");

            group.AddDevice(device);
            SubscribeToDevice(device);

            RaiseStructureChanged($"Device '{device.Id}' added to group '{groupName}'.");
        }

        public void RemoveDevice(string deviceId)
        {
            DeviceGroup group = FindGroupContainingDevice(deviceId) ?? throw new InvalidOperationException($"Device '{deviceId}' was not found!");

            Device device = group.RemoveDevice(deviceId);
            UnsubscribeFromDevice(device);

            RaiseStructureChanged($"Device '{deviceId}' removed.");
        }

        public void MoveDevice(string deviceId, string targetGroupName)
        {
            DeviceGroup sourceGroup = FindGroupContainingDevice(deviceId) ?? throw new InvalidOperationException($"Device '{deviceId}' was not found!");

            DeviceGroup targetGroup = FindGroup(targetGroupName) ?? throw new InvalidOperationException($"Target group '{targetGroupName}' was not found!");

            if (ReferenceEquals(sourceGroup, targetGroup)) return;

            Device device = sourceGroup.RemoveDevice(deviceId);
            targetGroup.AddDevice(device);

            RaiseStructureChanged($"Device '{deviceId}' moved from group '{sourceGroup.Name}' to '{targetGroup.Name}'.");
        }

        public DeviceGroup? FindGroup(string groupName)
        {
            return groups.FirstOrDefault(g => string.Equals(g.Name, groupName, StringComparison.OrdinalIgnoreCase));
        }

        public Device? FindDevice(string deviceId)
        {
            return groups
                .SelectMany(g => g.Devices)
                .FirstOrDefault(d => d.Id == deviceId);
        }

        public DeviceGroup? FindGroupContainingDevice(string deviceId)
        {
            return groups.FirstOrDefault(g => g.Devices.Any(d => d.Id == deviceId));
        }

        private void SubscribeToDevice(Device device)
        {
            device.Changed += HandleDeviceChanged;
        }

        private void UnsubscribeFromDevice(Device device)
        {
            device.Changed -= HandleDeviceChanged;
        }

        private void HandleDeviceChanged(object? sender, DeviceChangedEventArgs e)
        {
            DeviceChanged?.Invoke(this, e);
        }

        private void RaiseStructureChanged(string description)
        {
            StructureChanged?.Invoke(this, new TreeStructureChangedEventArgs(description));
        }
    }
}
