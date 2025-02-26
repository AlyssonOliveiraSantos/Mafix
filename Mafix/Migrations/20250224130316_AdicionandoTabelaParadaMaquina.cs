using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mafix.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTabelaParadaMaquina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParadaMaquinaId",
                table: "Producao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ParadaMaquina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContabilizaHoraParada = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParadaMaquina", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producao_ParadaMaquinaId",
                table: "Producao",
                column: "ParadaMaquinaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Producao_ParadaMaquina_ParadaMaquinaId",
                table: "Producao",
                column: "ParadaMaquinaId",
                principalTable: "ParadaMaquina",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Producao_ParadaMaquina_ParadaMaquinaId",
                table: "Producao");

            migrationBuilder.DropTable(
                name: "ParadaMaquina");

            migrationBuilder.DropIndex(
                name: "IX_Producao_ParadaMaquinaId",
                table: "Producao");

            migrationBuilder.DropColumn(
                name: "ParadaMaquinaId",
                table: "Producao");
        }
    }
}
