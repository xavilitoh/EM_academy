using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EM.Migrations
{
    /// <inheritdoc />
    public partial class Movimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovimientosUtilerias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<Guid>(type: "TEXT", nullable: false),
                    FechaMovimiento = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IdUtileria = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUsuario = table.Column<string>(type: "TEXT", nullable: false),
                    Cantidad = table.Column<decimal>(type: "TEXT", nullable: false),
                    TipoMovimiento = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacion = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosUtilerias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosUtilerias_AspNetUsers_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosUtilerias_Utilerias_IdUtileria",
                        column: x => x.IdUtileria,
                        principalTable: "Utilerias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosUtilerias_IdUsuario",
                table: "MovimientosUtilerias",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosUtilerias_IdUtileria",
                table: "MovimientosUtilerias",
                column: "IdUtileria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosUtilerias");
        }
    }
}
