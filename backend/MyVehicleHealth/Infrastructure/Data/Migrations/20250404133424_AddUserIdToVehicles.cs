using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVehicleHealth.Migrations
{
    public partial class AddUserIdToVehicles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adicionar coluna UserId à tabela Vehicles
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Vehicles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000001")); // Valor padrão temporário

            // Criar índice para UserId
            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles",
                column: "UserId");

            // Atualizar registros na tabela Vehicles
            migrationBuilder.Sql(@"
                UPDATE Vehicles SET UserId = '00000000-0000-0000-0000-000000000001'
                WHERE UserId = '00000000-0000-0000-0000-000000000001'");

            // Adicionar constraint de chave estrangeira
            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_Users_UserId",
                table: "Vehicles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_Users_UserId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_UserId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vehicles");
        }
    }
}