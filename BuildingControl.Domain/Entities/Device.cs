using BuildingControl.Domain.Enums;
using BuildingControl.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BuildingControl.Domain.Entities
{
    public abstract class Device
    {
        public abstract DeviceType Type { get; }
        public string Id { get; }
        public string Name { get; private set; }

        public event EventHandler<DeviceChangedEventArgs>? Changed;

        protected Device(string id, string name)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Device id cannot be empty!", nameof(id));

            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Device name cannot be empty!", nameof(name));

            Id = id;
            Name = name;
        }

        public void SetName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName)) throw new ArgumentException("Device name cannot be empty!", nameof(newName));

            if (Name == newName) return;

            Name = newName;
            OnChanged();
        }

        protected void OnChanged()
        {
            Changed?.Invoke(this, new DeviceChangedEventArgs(this));
        }

        public abstract string GetCurrentState();
    }
}
