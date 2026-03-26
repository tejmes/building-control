using BuildingControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingControl.Domain.Entities
{
    public  class CardReader : Device
    {
        public override DeviceType Type => DeviceType.CardReader;
        public string AccessCardNumber { get; private set; }

        public CardReader(string id, string name, string accessCardNumber = "") : base(id, name)
        {
            AccessCardNumber = "";

            SetAccessCardNumber(accessCardNumber);
        }

        public void SetAccessCardNumber(string newAccessCardNumber)
        {
            ValidateAccessCardNumber(newAccessCardNumber);

            string newAccessCardNumberReversed = ReverseBytesAndPad(newAccessCardNumber.ToUpper());

            if (AccessCardNumber == newAccessCardNumberReversed) return;

            AccessCardNumber = newAccessCardNumberReversed;
            OnChanged();
        }

        public void ValidateAccessCardNumber(string newAccessCardNumber)
        {
            if (newAccessCardNumber == null) throw new ArgumentNullException(nameof(newAccessCardNumber), "Access card number cannot be null!");

            if (newAccessCardNumber.Length == 0) throw new ArgumentException("Access card number cannot be empty", nameof(newAccessCardNumber));

            if (newAccessCardNumber.Length % 2 != 0) throw new ArgumentException("Access card number length must be even!", nameof(newAccessCardNumber));

            if (newAccessCardNumber.Length > 16) throw new ArgumentException("Access card number length cannot be longer than 16 characters", nameof(newAccessCardNumber));

            bool allHex = newAccessCardNumber.All(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'));

            if (!allHex) throw new ArgumentException("Access card number must contain only hexadecimal characters", nameof(newAccessCardNumber));
        }

        private string ReverseBytesAndPad(string newAccessCardNumber)
        {
            int byteCount = newAccessCardNumber.Length / 2;
            string[] pairs = new string[byteCount];

            for (int i = 0; i < byteCount; i++)
            {
                pairs[i] = newAccessCardNumber.Substring(i * 2, 2);
            }

            Array.Reverse(pairs);

            string reversed = string.Concat(pairs);
            return reversed.PadLeft(16, '0');
        }

        public override string GetCurrentState()
        {
            return $"Type: {Type}, Id: {Id}, Name: {Name}, AccessCardNumber: {AccessCardNumber}";
        }
    }
}
