using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVehicleHealth.Migrations
{
    public partial class CreateUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Criar tabela Users
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            // Criar índice único para Email
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            // Inserir usuário admin
            migrationBuilder.Sql(@"
                INSERT INTO Users (Id, Name, Email, PasswordHash, Role, CreatedAt)
                VALUES ('00000000-0000-0000-0000-000000000001', 'Admin', 'admin@myvehiclehealth.com',
                        '$2a$11$N9qo8uLOickgx2ZMRZoMy.MQUvFQtpEE6YJ1Wf5D7A1ebjjZ8D5uO', 'Admin', GETUTCDATE())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}