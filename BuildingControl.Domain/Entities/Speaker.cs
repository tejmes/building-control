using BuildingControl.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingControl.Domain.Entities
{
    public class Speaker : Device
    {
        public override DeviceType Type => DeviceType.Speaker;
        public SpeakerSound Sound { get; private set; }
        public double Volume { get; private set; }

        public Speaker(string id, string name, SpeakerSound sound = SpeakerSound.None, double volume = 0.0) : base(id, name)
        {
            if (volume < 0.0) throw new ArgumentOutOfRangeException(nameof(volume), "Volume cannot be negative!");

            Sound = sound;
            Volume = volume;
        }

        public void SetSound(SpeakerSound newSound)
        {
            if (Sound == newSound) return;

            Sound = newSound;
        }

        public void SetVolume(double newVolume)
        {
            if (newVolume <  0.0) throw new ArgumentOutOfRangeException(nameof(newVolume), "Volume cannot be negative!");

            if (Volume == newVolume) return;

            Volume = newVolume;
        }

        public override string GetCurrentState()
        {
            return $"Type: {Type}, Id: {Id}, Name: {Name}, Sound: {Sound}, Volume: {Volume}";
        }
    }
}
