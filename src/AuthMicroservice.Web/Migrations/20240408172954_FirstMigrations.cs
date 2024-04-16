using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthMicroservice.Web.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Salt = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: true, defaultValue: true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Age = table.Column<int>(type: "INTEGER", maxLength: 80, nullable: true),
                    Contacts_Email = table.Column<string>(type: "TEXT", maxLength: 38, nullable: true),
                    Contacts_Phone = table.Column<string>(type: "TEXT", maxLength: 14, nullable: true),
                    Birthday = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: true, defaultValueSql: "datetime('now')"),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true, defaultValueSql: "datetime('now')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
