using System;
using System.Collections.Generic;
using System.Linq;


namespace SmartHouseDll
{
    [Serializable]
    public class Room : Authentication
    {
        public Room(string name)
            : base(name)
        {
            Devices = new List<Device>();
        }

        public Room()
        {
            Devices = new List<Device>();
        }

        public virtual ICollection<Device> Devices { get; set; }

        
        public virtual House House { get; set; }


        public void AddDevice(Device device)
        {
            if (IsNotDevice(device.NameId))
            {
                Devices.Add(device);
            }
        }
        public void DeleteDevice(string device)
        {
            if (IsDevice(device))
            {
                Devices.Remove(Devices.First(c => c.NameId == device.ToLower()));
            }
        }

        public Device GetDevice(string device)
        {
            if (IsDevice(device))
                return Devices.First(c => c.NameId == device.ToLower());

            throw new Exception("Такого устройства не существует!");
        }

        private bool IsNotDevice(string device)
        {
            if (Devices.All(c => c.NameId != device.ToLower()))
            {
                return true;
            }
            return false;
        }
        private bool IsDevice(string device)
        {
            if (Devices.Any(c => c.NameId == device.ToLower()))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}