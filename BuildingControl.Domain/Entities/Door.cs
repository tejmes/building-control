using BuildingControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingControl.Domain.Entities
{
    public class Door : Device
    {
        public override DeviceType Type => DeviceType.Door;
        public DoorState State { get; private set; }
        public bool Locked
        {
            get => HasFlag(DoorState.Locked);
            set => SetFlag(DoorState.Locked, value);
        }
        public bool Open
        {
            get => HasFlag(DoorState.Open);
            set => SetFlag(DoorState.Open, value);
        }
        public bool OpenForTooLong
        {
            get => HasFlag(DoorState.OpenForTooLong);
            set => SetFlag(DoorState.OpenForTooLong, value);
        }
        public bool OpenedForcibly
        {
            get => HasFlag(DoorState.OpenedForcibly);
            set => SetFlag(DoorState.OpenedForcibly, value);
        }

        public Door(string id, string name, DoorState initialState = DoorState.None) : base(id, name)
        {
            State = initialState;
        }

        private bool HasFlag(DoorState flag)
        {
            return (State & flag) == flag;
        }

        private void SetFlag(DoorState flag, bool enabled)
        {
            DoorState newState = enabled ? State | flag : State & ~flag;

            if (newState == State) return;

            State = newState;
        }

        public override string GetCurrentState()
        {
            return $"Type: {Type}, Id: {Id}, Name: {Name}, State: {State}, Locked: {Locked}, Open: {Open}, OpenForTooLong: {OpenForTooLong}, OpenedForcibly: {OpenedForcibly}";
        }
    }
}
