using System;

namespace SmartHouseDll
{
    [Serializable]
    public class Humidifier : Device
    {
        public Humidifier(string name)
            : base(name)
        {
        }

        public Humidifier()
        {
        }
    }
}
