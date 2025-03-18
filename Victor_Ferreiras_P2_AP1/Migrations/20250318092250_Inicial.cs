using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Victor_Ferreiras_P2_AP1.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ciudades",
                columns: table => new
                {
                    CiudadId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CiudadNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.CiudadId);
                });

            migrationBuilder.CreateTable(
                name: "Encuestas",
                columns: table => new
                {
                    EncuestaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaEncuesta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Asignatura = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    MontoTotal = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encuestas", x => x.EncuestaId);
                });

            migrationBuilder.CreateTable(
                name: "EncuestasDetalle",
                columns: table => new
                {
                    DetalleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CiudadId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    EncuestaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncuestasDetalle", x => x.DetalleId);
                    table.ForeignKey(
                        name: "FK_EncuestasDetalle_Ciudades_CiudadId",
                        column: x => x.CiudadId,
                        principalTable: "Ciudades",
                        principalColumn: "CiudadId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncuestasDetalle_Encuestas_EncuestaId",
                        column: x => x.EncuestaId,
                        principalTable: "Encuestas",
                        principalColumn: "EncuestaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ciudades",
                columns: new[] { "CiudadId", "CiudadNombre", "Monto" },
                values: new object[,]
                {
                    { 1, "San Francisco", 0.0 },
                    { 2, "Santiago", 0.0 },
                    { 3, "Santo Domingo", 0.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncuestasDetalle_CiudadId",
                table: "EncuestasDetalle",
                column: "CiudadId");

            migrationBuilder.CreateIndex(
                name: "IX_EncuestasDetalle_EncuestaId",
                table: "EncuestasDetalle",
                column: "EncuestaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncuestasDetalle");

            migrationBuilder.DropTable(
                name: "Ciudades");

            migrationBuilder.DropTable(
                name: "Encuestas");
        }
    }
}
