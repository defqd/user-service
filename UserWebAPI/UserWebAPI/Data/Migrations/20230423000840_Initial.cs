using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserWebAPI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Login = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    BirthDay = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Admin = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ModifiedBy = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    RevokedOn = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RevokedBy = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Admin", "BirthDay", "CreatedBy", "CreatedOn", "Gender", "Login", "ModifiedBy", "ModifiedOn", "Name", "Password", "RevokedBy", "RevokedOn" },
                values: new object[] { new Guid("ad86aa94-5417-4438-b34a-d37bfaf032e7"), true, new DateTime(2023, 4, 23, 3, 8, 40, 457, DateTimeKind.Local).AddTicks(3465), "GOD", new DateTime(2023, 4, 23, 3, 8, 40, 457, DateTimeKind.Local).AddTicks(3477), 2, "admin", null, null, "Админ", "admin01", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
