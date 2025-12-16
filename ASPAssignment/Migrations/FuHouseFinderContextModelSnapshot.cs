using System;
using ASPAssignment.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASPAssignment.Migrations
{
    [DbContext(typeof(FuHouseFinderContext))]
    partial class FuHouseFinderContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.Admin", b =>
                {
                    b.Property<int>("AdminID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminID"));

                    b.Property<string>("AdminName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.House", b =>
                {
                    b.Property<int>("HouseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HouseID"));

                    b.Property<string>("HouseName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LandlordID")
                        .HasColumnType("int");

                    b.Property<int?>("LandlordID1")
                        .HasColumnType("int");

                    b.Property<decimal>("PowerPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("WaterPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("HouseID");

                    b.HasIndex("LandlordID");

                    b.HasIndex("LandlordID1");

                    b.ToTable("Houses");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.Landlord", b =>
                {
                    b.Property<int>("LandlordID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LandlordID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("VerificationStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("LandlordID");

                    b.ToTable("Landlords");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.RefreshToken", b =>
                {
                    b.Property<int>("TokenID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TokenID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("bit");

                    b.Property<string>("NameRole")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("RequestNameRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TokenID");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleID"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleID");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.Room", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomID"));

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<int>("CurrentAmountOfPeople")
                        .HasColumnType("int");

                    b.Property<int>("HouseID")
                        .HasColumnType("int");

                    b.Property<string>("Information")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxAmountOfPeople")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("RoomID");

                    b.HasIndex("HouseID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("IdentityCardBackSideImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdentityCardFrontSideImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("RoleID")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.HasIndex("RoleID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.House", b =>
                {
                    b.HasOne("ASPAssignment.DataAccess.Models.User", "Landlord")
                        .WithMany("Houses")
                        .HasForeignKey("LandlordID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ASPAssignment.DataAccess.Models.Landlord", null)
                        .WithMany("Houses")
                        .HasForeignKey("LandlordID1");

                    b.Navigation("Landlord");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.Room", b =>
                {
                    b.HasOne("ASPAssignment.DataAccess.Models.House", "House")
                        .WithMany("Rooms")
                        .HasForeignKey("HouseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("House");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.User", b =>
                {
                    b.HasOne("ASPAssignment.DataAccess.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.House", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.Landlord", b =>
                {
                    b.Navigation("Houses");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ASPAssignment.DataAccess.Models.User", b =>
                {
                    b.Navigation("Houses");
                });
#pragma warning restore 612, 618
        }
    }
}
