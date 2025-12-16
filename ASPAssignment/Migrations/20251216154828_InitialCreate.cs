using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPAssignment.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Admins table
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            // Create Landlords table
            migrationBuilder.CreateTable(
                name: "Landlords",
                columns: table => new
                {
                    LandlordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landlords", x => x.LandlordID);
                });

            // Create RefreshTokens table
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    TokenID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RequestNameRoleId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.TokenID);
                });

            // Add LandlordID1 column to Houses table for new Landlord relationship
            migrationBuilder.AddColumn<int>(
                name: "LandlordID1",
                table: "Houses",
                type: "int",
                nullable: true);

            // Create index and foreign key for Houses-Landlords relationship
            migrationBuilder.CreateIndex(
                name: "IX_Houses_LandlordID1",
                table: "Houses",
                column: "LandlordID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Landlords_LandlordID1",
                table: "Houses",
                column: "LandlordID1",
                principalTable: "Landlords",
                principalColumn: "LandlordID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign key and index for Houses-Landlords relationship
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Landlords_LandlordID1",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_LandlordID1",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "LandlordID1",
                table: "Houses");

            // Drop new tables
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Landlords");
        }
    }
}
