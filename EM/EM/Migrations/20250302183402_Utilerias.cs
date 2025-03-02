using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EM.Migrations
{
    /// <inheritdoc />
    public partial class Utilerias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Utilerias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<Guid>(type: "TEXT", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<decimal>(type: "TEXT", nullable: false),
                    IdTipo = table.Column<int>(type: "INTEGER", nullable: false),
                    IdMarca = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utilerias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Utilerias_Marcas_IdMarca",
                        column: x => x.IdMarca,
                        principalTable: "Marcas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Utilerias_TiposUtileria_IdTipo",
                        column: x => x.IdTipo,
                        principalTable: "TiposUtileria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Utilerias_IdMarca",
                table: "Utilerias",
                column: "IdMarca");

            migrationBuilder.CreateIndex(
                name: "IX_Utilerias_IdTipo",
                table: "Utilerias",
                column: "IdTipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Utilerias");
        }
    }
}
