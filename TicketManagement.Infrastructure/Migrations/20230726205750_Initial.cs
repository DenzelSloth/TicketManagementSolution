using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resumen",
                columns: table => new
                {
                    Id_Tienda = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Id_Registradora = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Tickets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id_Tienda = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Id_Registradora = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ticket = table.Column<int>(type: "int", nullable: false),
                    Impuesto = table.Column<decimal>(type: "money", nullable: false),
                    Total = table.Column<decimal>(type: "money", nullable: false),
                    FechaHora_Creacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Resumen_Id_Tienda_Id_Registradora",
                table: "Resumen",
                columns: new[] { "Id_Tienda", "Id_Registradora" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Ticket",
                table: "Tickets",
                column: "Ticket",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resumen");

            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
