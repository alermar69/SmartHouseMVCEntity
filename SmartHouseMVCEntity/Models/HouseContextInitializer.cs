using System.Data.Entity;
using SmartHouseDll;

namespace SmartHouseMVCEntity.Models
{
    public class HouseContextInitializer : CreateDatabaseIfNotExists<HouseContext>
    {
        protected override void Seed(HouseContext context)
        {
            base.Seed(context);

            var house = new House("Дом");
 
            var room = new Room("Зал"){House = house};
            context.Devices.Add(new ClimatControl("Климат") { Room = room });
            context.Devices.Add(new Tv("TV1"){Room = room});
            context.Devices.Add(new Tv("TV2") { Room = room });
            context.Devices.Add(new Lamp("Лампа1") { Room = room });
            context.Devices.Add(new Lamp("Лампа2") { Room = room });
            context.Rooms.Add(room);

            room = new Room("Кухня") { House = house };
            context.Devices.Add(new ClimatControl("Климат") { Room = room });
            context.Devices.Add(new Tv("TV1") { Room = room });
            context.Devices.Add(new Tv("TV2") { Room = room });
            context.Devices.Add(new Lamp("Лампа1") { Room = room });
            context.Devices.Add(new Lamp("Лампа2") { Room = room });
            context.Rooms.Add(room);

            context.SaveChanges();
        }

    }
}