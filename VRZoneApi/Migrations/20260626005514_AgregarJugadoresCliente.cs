using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VRZoneApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarJugadoresCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Jugadores",
                table: "Clientes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jugadores",
                table: "Clientes");
        }
    }
}
