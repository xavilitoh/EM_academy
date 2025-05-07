﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EM.Data.Migrations
{
    /// <inheritdoc />
    public partial class Factura_resto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Resto",
                table: "FacturasAtletas",
                type: "TEXT",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resto",
                table: "FacturasAtletas");
        }
    }
}
