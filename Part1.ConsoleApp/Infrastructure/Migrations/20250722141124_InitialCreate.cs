using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Part1.ConsoleApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Distribuidor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<long>(type: "bigint", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Distribuidor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoMaterial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoMaterial", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DistribuidorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Marca_Distribuidor_DistribuidorId",
                        column: x => x.DistribuidorId,
                        principalTable: "Distribuidor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Producto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Peso = table.Column<float>(type: "real", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    TipoProducto = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoMaterialId = table.Column<int>(type: "int", nullable: true),
                    MarcaId = table.Column<int>(type: "int", nullable: true),
                    Filamento_DistribuidorId = table.Column<int>(type: "int", nullable: true),
                    Filamento_DistribuidorId1 = table.Column<int>(type: "int", nullable: true),
                    DistribuidorId = table.Column<int>(type: "int", nullable: true),
                    DistribuidorId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Producto_Distribuidor_DistribuidorId",
                        column: x => x.DistribuidorId,
                        principalTable: "Distribuidor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Producto_Distribuidor_DistribuidorId1",
                        column: x => x.DistribuidorId1,
                        principalTable: "Distribuidor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Producto_Distribuidor_Filamento_DistribuidorId",
                        column: x => x.Filamento_DistribuidorId,
                        principalTable: "Distribuidor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Producto_Distribuidor_Filamento_DistribuidorId1",
                        column: x => x.Filamento_DistribuidorId1,
                        principalTable: "Distribuidor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Producto_Marca_MarcaId",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Producto_TipoMaterial_TipoMaterialId",
                        column: x => x.TipoMaterialId,
                        principalTable: "TipoMaterial",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marca_DistribuidorId",
                table: "Marca",
                column: "DistribuidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_DistribuidorId",
                table: "Producto",
                column: "DistribuidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_DistribuidorId1",
                table: "Producto",
                column: "DistribuidorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Filamento_DistribuidorId",
                table: "Producto",
                column: "Filamento_DistribuidorId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_Filamento_DistribuidorId1",
                table: "Producto",
                column: "Filamento_DistribuidorId1");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_MarcaId",
                table: "Producto",
                column: "MarcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Producto_TipoMaterialId",
                table: "Producto",
                column: "TipoMaterialId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Producto");

            migrationBuilder.DropTable(
                name: "Marca");

            migrationBuilder.DropTable(
                name: "TipoMaterial");

            migrationBuilder.DropTable(
                name: "Distribuidor");
        }
    }
}
