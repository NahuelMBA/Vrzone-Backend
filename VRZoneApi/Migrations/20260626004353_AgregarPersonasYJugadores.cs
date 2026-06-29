using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VRZoneApi.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPersonasYJugadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Jugadores",
                table: "Reservas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "CantidadPersonas",
                table: "Clientes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jugadores",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "CantidadPersonas",
                table: "Clientes");
        }
    }
}
