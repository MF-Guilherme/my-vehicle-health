using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyVehicleHealth.Migrations
{
    public partial class AddUserIdToMaintenances : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Adicionar coluna UserId à tabela Maintenances
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Maintenances",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000001")); // Valor padrão temporário

            // Criar índice para UserId
            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_UserId",
                table: "Maintenances",
                column: "UserId");

            // Atualizar registros na tabela Maintenances
            migrationBuilder.Sql(@"
                UPDATE Maintenances SET UserId = '00000000-0000-0000-0000-000000000001'
                WHERE UserId = '00000000-0000-0000-0000-000000000001'");

            // Adicionar constraint de chave estrangeira com ON DELETE NO ACTION
            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_Users_UserId",
                table: "Maintenances",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_Users_UserId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_UserId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Maintenances");
        }
    }
}