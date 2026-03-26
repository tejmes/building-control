using BuildingControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingControl.Domain.Entities
{
    public class LedPanel : Device
    {
        public override DeviceType Type => DeviceType.LedPanel;
        public string Message { get; private set; }

        public LedPanel(string id, string name, string message = "") : base(id, name)
        {
            Message = message;
        }

        public void SetMessage(string newMessage)
        {
            if (newMessage == null) newMessage = "";

            if (Message == newMessage) return;

            Message = newMessage;
        }

        public override string GetCurrentState()
        {
            return $"Type: {Type}, Id: {Id}, Name: {Name}, Message: {Message}";
        }
    }
}
