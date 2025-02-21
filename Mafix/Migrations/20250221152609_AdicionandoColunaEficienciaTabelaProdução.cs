using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mafix.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoColunaEficienciaTabelaProdução : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Eficiencia",
                table: "Producao",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Eficiencia",
                table: "Producao");
        }
    }
}
