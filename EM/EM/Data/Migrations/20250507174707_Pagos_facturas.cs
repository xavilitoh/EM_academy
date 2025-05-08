using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EM.Data.Migrations
{
    /// <inheritdoc />
    public partial class Pagos_facturas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PagosFacturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdFactura = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAtleta = table.Column<int>(type: "INTEGER", nullable: false),
                    Monto = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Enable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagosFacturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagosFacturas_Atletas_IdAtleta",
                        column: x => x.IdAtleta,
                        principalTable: "Atletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagosFacturas_FacturasAtletas_IdFactura",
                        column: x => x.IdFactura,
                        principalTable: "FacturasAtletas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PagosFacturas_IdAtleta",
                table: "PagosFacturas",
                column: "IdAtleta");

            migrationBuilder.CreateIndex(
                name: "IX_PagosFacturas_IdFactura",
                table: "PagosFacturas",
                column: "IdFactura");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PagosFacturas");
        }
    }
}
