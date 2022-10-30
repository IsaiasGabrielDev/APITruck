using Microsoft.EntityFrameworkCore.Migrations;

namespace APITruck.Migrations
{
    public partial class initdsbagdabsd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Caminhoes_Modelos_ModeloId",
                table: "Caminhoes");

            migrationBuilder.DropIndex(
                name: "IX_Caminhoes_ModeloId",
                table: "Caminhoes");

            migrationBuilder.DropColumn(
                name: "ModeloId",
                table: "Caminhoes");

            migrationBuilder.AddColumn<string>(
                name: "NomeModelo",
                table: "Caminhoes",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NomeModelo",
                table: "Caminhoes");

            migrationBuilder.AddColumn<int>(
                name: "ModeloId",
                table: "Caminhoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Caminhoes_ModeloId",
                table: "Caminhoes",
                column: "ModeloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Caminhoes_Modelos_ModeloId",
                table: "Caminhoes",
                column: "ModeloId",
                principalTable: "Modelos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
