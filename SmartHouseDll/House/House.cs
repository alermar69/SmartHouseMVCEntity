using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHouseDll
{
    [Serializable]
    public class House : Authentication
    {
        public House(string name)
            : base(name)
        {
            Rooms = new List<Room>();
        }
        public House()
        {
            Rooms = new List<Room>();
        }


        [NonSerialized]
        private RemoteControl _remote;

        public virtual ICollection<Room> Rooms { get; set; }
        
        public RemoteControl Remote
        {
            get { return _remote; }
            set { _remote = value; }
        }


        public void AddRoom(Room room)
        {
            if (IsNotRoom(room.NameId))
            {
                Rooms.Add(room);
            }
        }
        public void DeleteRoom(string room)
        {
            if (Rooms.Any(c => c.NameId == room.ToLower()))
            {
                Rooms.Remove(Rooms.First(c => c.NameId == room.ToLower()));
            }
        }

        public void AddDevice(string room, Device device)
        {
            if (IsRoom(room))
                Rooms.First(c => c.NameId == room.ToLower()).AddDevice(device);
        }
        public void DeleteDevice(string room, string device)
        {
            if (IsRoom(room))
                Rooms.First(c => c.NameId == room.ToLower()).DeleteDevice(device);
        }

        public Room GetRoom(string room)
        {
            if (IsRoom(room))
                return Rooms.First(c => c.NameId == room.ToLower());

            throw new Exception("Такой комнаты не существует!");
        }
        public Device GetDevice(string room, string device)
        {
            return GetRoom(room).GetDevice(device);
        }

        public void ChangeNameRoom(string oldRoom, string newRoom)
        {
            Room room = GetRoom(oldRoom);
            room.Name = newRoom;
            DeleteRoom(oldRoom);
            AddRoom(room);
        }
        public void ChangeNameDevice(string room, string oldDevice, string newDevice)
        {
            Device device = GetDevice(room, oldDevice);
            device.Name = newDevice;
            DeleteDevice(room, oldDevice);
            AddDevice(room, device);
        }

        private bool IsNotRoom(string room)
        {
            if (Rooms.All(c => c.NameId != room.ToLower()))
            {
                return true;
            }
            return false;
        }
        private bool IsRoom(string room)
        {
            if (Rooms.Any(c => c.NameId == room.ToLower()))
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