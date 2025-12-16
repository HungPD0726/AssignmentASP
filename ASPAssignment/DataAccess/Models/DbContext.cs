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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // --- CẤU HÌNH CHO TIỀN TỆ (DECIMAL) ---

            // Bảng House: Giá điện và Giá nước
            modelBuilder.Entity<House>()
                .Property(h => h.PowerPrice)
                .HasColumnType("decimal(18, 2)"); // Ví dụ: 12345.67

            modelBuilder.Entity<House>()
                .Property(h => h.WaterPrice)
                .HasColumnType("decimal(18, 2)");

            // Bảng Room: Giá phòng
            modelBuilder.Entity<Room>()
                .Property(r => r.Price)
                .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Room>()
                .Property(r => r.Area)
                .HasColumnType("float"); // Diện tích có thể dùng float
        }
    }
}