using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleManagement.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    Website = table.Column<string>(maxLength: 250, nullable: true),
                    AppVersion = table.Column<string>(maxLength: 50, nullable: true),
                    AboutUS = table.Column<string>(nullable: true),
                    HeaderImages = table.Column<string>(nullable: true),
                    PrivacyPolicy = table.Column<string>(nullable: true),
                    UserTerms = table.Column<string>(nullable: true),
                    FacebookUrl = table.Column<string>(maxLength: 250, nullable: true),
                    TwitterUrl = table.Column<string>(maxLength: 250, nullable: true),
                    YoutubeUrl = table.Column<string>(maxLength: 250, nullable: true),
                    InstagramUrl = table.Column<string>(maxLength: 250, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PasswordReset",
                columns: table => new
                {
                    Token = table.Column<string>(maxLength: 250, nullable: false),
                    Email = table.Column<string>(maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasswordReset", x => x.Token);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(maxLength: 250, nullable: false),
                    LastName = table.Column<string>(maxLength: 250, nullable: false),
                    ImageName = table.Column<string>(maxLength: 250, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 250, nullable: true),
                    Address = table.Column<string>(maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: false),
                    Username = table.Column<string>(maxLength: 250, nullable: false),
                    PasswordHash = table.Column<byte[]>(maxLength: 128, nullable: false),
                    PasswordSalt = table.Column<byte[]>(maxLength: 128, nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false),
                    IsEmployee = table.Column<bool>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    user_firebase_token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleManufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleManufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInRole",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_UserInRoles",
                        column: x => x.RoleId,
                        principalTable: "UserRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_UserInRoles",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ModelYear = table.Column<string>(nullable: true),
                    ManufacturerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manufacturer_vehicleModels",
                        column: x => x.ManufacturerId,
                        principalTable: "VehicleManufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Address", "CreatedDate", "Email", "FirstName", "ImageName", "IsAdmin", "IsEmployee", "LastName", "PasswordHash", "PasswordSalt", "PhoneNumber", "UpdatedDate", "Username", "user_firebase_token" },
                values: new object[] { 1, null, new DateTime(2023, 5, 13, 11, 55, 51, 650, DateTimeKind.Local).AddTicks(4368), "admin@abc.com", "Vehicle ", null, true, false, "Master", new byte[] { 28, 45, 217, 79, 16, 3, 131, 13, 0, 47, 156, 46, 149, 251, 13, 53, 62, 43, 218, 123, 185, 163, 117, 200, 132, 226, 134, 27, 79, 85, 186, 6, 87, 73, 112, 218, 224, 74, 108, 39, 74, 202, 223, 139, 10, 42, 255, 69, 54, 205, 122, 51, 78, 142, 207, 140, 173, 53, 250, 227, 165, 154, 97, 148 }, new byte[] { 44, 30, 82, 182, 104, 215, 46, 121, 58, 71, 221, 54, 171, 205, 36, 147, 156, 105, 159, 232, 4, 201, 151, 243, 122, 121, 133, 65, 243, 155, 137, 158, 176, 110, 113, 13, 19, 56, 107, 139, 163, 219, 243, 231, 79, 2, 228, 97, 58, 21, 244, 95, 245, 111, 198, 246, 255, 136, 245, 157, 210, 76, 227, 236, 173, 50, 254, 222, 108, 62, 140, 146, 37, 74, 16, 56, 79, 112, 184, 137, 103, 4, 237, 152, 244, 222, 248, 103, 121, 134, 255, 133, 162, 200, 150, 193, 167, 151, 232, 171, 130, 46, 23, 93, 161, 209, 44, 123, 13, 123, 11, 166, 135, 85, 112, 146, 203, 44, 175, 210, 57, 11, 45, 48, 244, 46, 5, 49 }, null, null, "admin@abc.com", null });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "RoleName" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "VehicleManufacturers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Toyota" },
                    { 2, "BMW Group" },
                    { 3, "Honda Motor Co." },
                    { 4, "Suzuki Motor Corporation" },
                    { 5, "Nissan Motor Co." }
                });

            migrationBuilder.InsertData(
                table: "UserInRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "VehicleModels",
                columns: new[] { "Id", "ManufacturerId", "ModelYear", "Name" },
                values: new object[,]
                {
                    { 1, 1, "2000", "Toyota Corolla" },
                    { 6, 1, "2000", "Toyota Hilux" },
                    { 2, 2, "2000", "BMW 7 Series" },
                    { 3, 3, "2000", "Honda City" },
                    { 4, 4, "2000", "Suzuki Vitara" },
                    { 5, 5, "2000", "Nissan Altima" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PasswordReset",
                table: "PasswordReset",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Email",
                table: "User",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInRole_RoleId",
                table: "UserInRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModels_ManufacturerId",
                table: "VehicleModels",
                column: "ManufacturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppSettings");

            migrationBuilder.DropTable(
                name: "PasswordReset");

            migrationBuilder.DropTable(
                name: "UserInRole");

            migrationBuilder.DropTable(
                name: "VehicleModels");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "VehicleManufacturers");
        }
    }
}
