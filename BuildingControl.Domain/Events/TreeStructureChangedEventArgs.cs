using System;
using System.Collections.Generic;
using System.Text;

namespace BuildingControl.Domain.Events
{
    public class TreeStructureChangedEventArgs : EventArgs
    {
        public string Description { get; }

        public TreeStructureChangedEventArgs(string description)
        {
            Description = description;
        }
    }
}
