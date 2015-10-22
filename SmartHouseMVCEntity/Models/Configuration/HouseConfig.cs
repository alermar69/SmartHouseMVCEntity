using System.Data.Entity.ModelConfiguration;
using SmartHouseDll;

namespace SmartHouseMVCEntity
{
    public class HouseConfig : EntityTypeConfiguration<House>
    {
        public HouseConfig()
        {
            HasKey(p => p.Id);
            Property(p => p.Name).IsRequired().HasMaxLength(20);
            Property(p => p.NameId).HasMaxLength(20);
            Ignore(p => p.Remote);
            
        }
    }
}