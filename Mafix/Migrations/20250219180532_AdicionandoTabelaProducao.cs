using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mafix.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTabelaProducao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Producao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperadorId = table.Column<int>(type: "int", nullable: false),
                    MaquinaId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    QuantidadeProduzida = table.Column<int>(type: "int", nullable: false),
                    DataProducao = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraDeInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraDeFim = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producao_Maquinas_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producao_Operadores_OperadorId",
                        column: x => x.OperadorId,
                        principalTable: "Operadores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producao_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Producao_MaquinaId",
                table: "Producao",
                column: "MaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Producao_OperadorId",
                table: "Producao",
                column: "OperadorId");

            migrationBuilder.CreateIndex(
                name: "IX_Producao_ProdutoId",
                table: "Producao",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Producao");
        }
    }
}
