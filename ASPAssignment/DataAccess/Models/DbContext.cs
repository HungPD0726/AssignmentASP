using ASPAssignment.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPAssignment.DataAccess.Context
{
    public class FuHouseFinderContext : DbContext
    {
        public FuHouseFinderContext(DbContextOptions<FuHouseFinderContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<House>()
                .Property(h => h.PowerPrice)
                .HasColumnType("decimal(18, 2)"); 

            modelBuilder.Entity<House>()
                .Property(h => h.WaterPrice)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Room>()
                .Property(r => r.Area)
                .HasColumnType("float"); 
        }
    }
}