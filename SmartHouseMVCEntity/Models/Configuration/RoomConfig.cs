using System.Data.Entity.ModelConfiguration;
using SmartHouseDll;

namespace SmartHouseMVCEntity
{
    public class RoomConfig : EntityTypeConfiguration<Room>
    {
        public RoomConfig()
        {
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(20);
            Property(p => p.NameId).HasMaxLength(20);           

        }
    }
}