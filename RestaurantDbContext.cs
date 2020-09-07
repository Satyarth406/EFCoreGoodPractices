using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreCommandAndQueries
{
    public class RestaurantDbContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SampleRestaurantDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging();
        }

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>().HasMany<FoodItem>(t => t.FoodItems)
                .WithOne(f => f.Restaurant)
                .HasForeignKey(f => f.RestaurantId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
