namespace Skyline.DataMiner.SDM.Ticketing.Models
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class MandatoryAttribute : Attribute
    {
        public int[] States { get; }

        // Constructor for specifying states
        public MandatoryAttribute(params int[] states)
        {
            States = states;
        }

        // Constructor for all states
        public MandatoryAttribute()
        {
            States = new int[0];
        }
    }
}
