using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Part1.ConsoleApp.Migrations
{
    /// <inheritdoc />
    public partial class AgregarDatosCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NombreCliente",
                table: "OrdenDeCompra",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "NumeroCliente",
                table: "OrdenDeCompra",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreCliente",
                table: "OrdenDeCompra");

            migrationBuilder.DropColumn(
                name: "NumeroCliente",
                table: "OrdenDeCompra");
        }
    }
}
