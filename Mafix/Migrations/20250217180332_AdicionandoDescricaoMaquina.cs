using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mafix.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoDescricaoMaquina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Maquinas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Maquinas");
        }
    }
}
