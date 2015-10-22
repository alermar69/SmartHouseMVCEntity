using System.Data.Entity;
using SmartHouseDll;

namespace SmartHouseMVCEntity.Models
{
    public class HouseContext : DbContext
    {
        public HouseContext()
            //: base(@"data source=(localDb)\v11.0;initial catalog=MyDB6;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework")
        {

        }
        public DbSet<House> Houses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

             modelBuilder.Configurations.Add(new HouseConfig());
             modelBuilder.Configurations.Add(new RoomConfig());
             modelBuilder.Configurations.Add(new DeviceConfig());
        }
    }
}