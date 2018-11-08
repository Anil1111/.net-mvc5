using SportsStore.Domain.Entities;
using SportsStore.Domain.Inital;
using System.Data.Entity;

namespace SportsStore.Domain.Concrete
{
    public  class EFDbContext: DbContext
    {
        public EFDbContext():base("name=EFDbContext")
        {
            Database.SetInitializer(new EfDbInitializer());
        }

        public DbSet<Product> Products { get; set; }
    }
}
