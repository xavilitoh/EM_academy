using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EM.Migrations
{
    /// <inheritdoc />
    public partial class Alergias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AlrgicoA",
                table: "Atletas",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "PresentaAlergias",
                table: "Atletas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlrgicoA",
                table: "Atletas");

            migrationBuilder.DropColumn(
                name: "PresentaAlergias",
                table: "Atletas");
        }
    }
}
