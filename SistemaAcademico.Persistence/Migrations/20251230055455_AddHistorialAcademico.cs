using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademico.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddHistorialAcademico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calificacion",
                table: "Seleccion");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Tarifario",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AddColumn<bool>(
                name: "Activa",
                table: "Preseleccion",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "Factura",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "MontoTotal",
                table: "Factura",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Descuento",
                table: "Factura",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "DetalleFactura",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "MontoTotal",
                table: "DetalleFactura",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Monto",
                table: "DetalleFactura",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Descuento",
                table: "DetalleFactura",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Descuento",
                table: "CuentaPorPagar",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "CantidadTotal",
                table: "CuentaPorPagar",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "CantidadRestante",
                table: "CuentaPorPagar",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.CreateTable(
                name: "HistorialAcademico",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    ID_Asignatura = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    ID_Periodo = table.Column<int>(type: "int", nullable: false),
                    Calificacion = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false),
                    Fecha_Registro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Historial_Asignatura",
                        column: x => x.ID_Asignatura,
                        principalTable: "Asignatura",
                        principalColumn: "Asignatura_ID");
                    table.ForeignKey(
                        name: "FK_Historial_Periodo",
                        column: x => x.ID_Periodo,
                        principalTable: "PeriodoConfig",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Historial_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "ID_Usuario");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAcademico_ID_Asignatura",
                table: "HistorialAcademico",
                column: "ID_Asignatura");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAcademico_ID_Periodo",
                table: "HistorialAcademico",
                column: "ID_Periodo");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAcademico_ID_Usuario",
                table: "HistorialAcademico",
                column: "ID_Usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistorialAcademico");

            migrationBuilder.DropColumn(
                name: "Activa",
                table: "Preseleccion");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Tarifario",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AddColumn<decimal>(
                name: "Calificacion",
                table: "Seleccion",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "Factura",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "MontoTotal",
                table: "Factura",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Descuento",
                table: "Factura",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "DetalleFactura",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "MontoTotal",
                table: "DetalleFactura",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Monto",
                table: "DetalleFactura",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Descuento",
                table: "DetalleFactura",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Descuento",
                table: "CuentaPorPagar",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "CantidadTotal",
                table: "CuentaPorPagar",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "CantidadRestante",
                table: "CuentaPorPagar",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);
        }
    }
}
