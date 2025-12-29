using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademico.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModule4SchemaFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preseleccion_Asignatura",
                table: "Preseleccion");

            migrationBuilder.DropIndex(
                name: "IX_Preseleccion_ID_Asignatura",
                table: "Preseleccion");

            migrationBuilder.DropColumn(
                name: "ID_Asignatura",
                table: "Preseleccion");

            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "PeriodoConfig");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Tarifario",
                type: "decimal(10)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,30)",
                oldPrecision: 10);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaConfirmacion",
                table: "Seleccion",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "VieneDePreseleccion",
                table: "Seleccion",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AsignaturaId",
                table: "Preseleccion",
                type: "varchar(255)",
                nullable: true,
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddColumn<int>(
                name: "ID_Seccion",
                table: "Preseleccion",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Procesada",
                table: "Preseleccion",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PermitirModificarEnSeleccion",
                table: "PeriodoConfig",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.CreateIndex(
                name: "IX_Preseleccion_AsignaturaId",
                table: "Preseleccion",
                column: "AsignaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Preseleccion_ID_Seccion",
                table: "Preseleccion",
                column: "ID_Seccion");

            migrationBuilder.AddForeignKey(
                name: "FK_Preseleccion_Asignatura_AsignaturaId",
                table: "Preseleccion",
                column: "AsignaturaId",
                principalTable: "Asignatura",
                principalColumn: "Asignatura_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Preseleccion_Seccion",
                table: "Preseleccion",
                column: "ID_Seccion",
                principalTable: "Seccion",
                principalColumn: "Seccion_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Preseleccion_Asignatura_AsignaturaId",
                table: "Preseleccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Preseleccion_Seccion",
                table: "Preseleccion");

            migrationBuilder.DropIndex(
                name: "IX_Preseleccion_AsignaturaId",
                table: "Preseleccion");

            migrationBuilder.DropIndex(
                name: "IX_Preseleccion_ID_Seccion",
                table: "Preseleccion");

            migrationBuilder.DropColumn(
                name: "FechaConfirmacion",
                table: "Seleccion");

            migrationBuilder.DropColumn(
                name: "VieneDePreseleccion",
                table: "Seleccion");

            migrationBuilder.DropColumn(
                name: "AsignaturaId",
                table: "Preseleccion");

            migrationBuilder.DropColumn(
                name: "ID_Seccion",
                table: "Preseleccion");

            migrationBuilder.DropColumn(
                name: "Procesada",
                table: "Preseleccion");

            migrationBuilder.DropColumn(
                name: "PermitirModificarEnSeleccion",
                table: "PeriodoConfig");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Tarifario",
                type: "decimal(10,30)",
                precision: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10)",
                oldPrecision: 10);

            migrationBuilder.AddColumn<string>(
                name: "ID_Asignatura",
                table: "Preseleccion",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "",
                collation: "utf8_general_ci")
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.AddColumn<int>(
                name: "Estatus",
                table: "PeriodoConfig",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_Preseleccion_ID_Asignatura",
                table: "Preseleccion",
                column: "ID_Asignatura");

            migrationBuilder.AddForeignKey(
                name: "FK_Preseleccion_Asignatura",
                table: "Preseleccion",
                column: "ID_Asignatura",
                principalTable: "Asignatura",
                principalColumn: "Asignatura_ID");
        }
    }
}
