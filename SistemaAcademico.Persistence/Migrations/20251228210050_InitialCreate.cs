using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaAcademico.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8");

            migrationBuilder.CreateTable(
                name: "AreaAcademica",
                columns: table => new
                {
                    AreaAcademica_ID = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    AreaAcademica_Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.AreaAcademica_ID);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Edificio",
                columns: table => new
                {
                    ID_Edificio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Nomenclatura = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Edificio);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "PeriodoConfig",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Codigo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    PreseleccionInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    PreseleccionFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SeleccionInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SeleccionFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Rol",
                columns: table => new
                {
                    Rol_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descripcion = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Rol_ID);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Tarifario",
                columns: table => new
                {
                    ID_Tarifario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Precio = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    PeriodoAcademico = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Estatus = table.Column<string>(type: "enum('Activo','Inactivo')", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Tarifario);
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Asignatura",
                columns: table => new
                {
                    Asignatura_ID = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Nombre = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Tipo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    IdAreaAcademica = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Asignatura_ID);
                    table.ForeignKey(
                        name: "FK_Asignatura_AreaAcademica",
                        column: x => x.IdAreaAcademica,
                        principalTable: "AreaAcademica",
                        principalColumn: "AreaAcademica_ID");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Carrera",
                columns: table => new
                {
                    Carrera_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Nomenclatura = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    IdAreaAcademica = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Carrera_ID);
                    table.ForeignKey(
                        name: "FK_Carrera_AreaAcademica",
                        column: x => x.IdAreaAcademica,
                        principalTable: "AreaAcademica",
                        principalColumn: "AreaAcademica_ID");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Aula",
                columns: table => new
                {
                    ID_Aula = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    ID_Edificio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Aula);
                    table.ForeignKey(
                        name: "FK_Aula_Edificio",
                        column: x => x.ID_Edificio,
                        principalTable: "Edificio",
                        principalColumn: "ID_Edificio");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    ID_Usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Apellido = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Nacionalidad = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Direccion = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Telefono = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Correo_Personal = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Correo_Institucional = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Fecha_Ingreso = table.Column<DateOnly>(type: "date", nullable: false),
                    Clave_Hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    ID_Rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Usuario);
                    table.ForeignKey(
                        name: "FK_Rol_Usuario",
                        column: x => x.ID_Rol,
                        principalTable: "Rol",
                        principalColumn: "Rol_ID");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Asignatura_ProgramaAcademico",
                columns: table => new
                {
                    Id_Asignatura = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Id_ProgramaAcademico = table.Column<int>(type: "int", nullable: false),
                    PreRequisitos = table.Column<string>(type: "json", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Corequisito = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Creditos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.Id_Asignatura, x.Id_ProgramaAcademico })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "CK_AsignaturaProgramaAcademico1",
                        column: x => x.Id_Asignatura,
                        principalTable: "Asignatura",
                        principalColumn: "Asignatura_ID");
                    table.ForeignKey(
                        name: "FK_AsignaturaProgramaAcademico_Corequisito",
                        column: x => x.Corequisito,
                        principalTable: "Asignatura",
                        principalColumn: "Asignatura_ID");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "ProgramaAcademico",
                columns: table => new
                {
                    ProgramaAcademico_ID = table.Column<int>(type: "int", nullable: false),
                    Periodo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Estatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Total_Creditos = table.Column<int>(type: "int", nullable: false),
                    TrimestresMaximos = table.Column<int>(type: "int", nullable: false),
                    ID_Carrera = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ProgramaAcademico_ID);
                    table.ForeignKey(
                        name: "FK_Carrera_ProgramaAcademico",
                        column: x => x.ID_Carrera,
                        principalTable: "Carrera",
                        principalColumn: "Carrera_ID");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "CuentaPorPagar",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Pagada = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tipo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    CantidadTotal = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    CantidadRestante = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    ID_Usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CPP_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "ID_Usuario");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Preseleccion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    ID_Asignatura = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    ID_Periodo = table.Column<int>(type: "int", nullable: false),
                    Fecha_Registro = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Preseleccion_Asignatura",
                        column: x => x.ID_Asignatura,
                        principalTable: "Asignatura",
                        principalColumn: "Asignatura_ID");
                    table.ForeignKey(
                        name: "FK_Preseleccion_Periodo",
                        column: x => x.ID_Periodo,
                        principalTable: "PeriodoConfig",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Preseleccion_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "ID_Usuario");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Profesor",
                columns: table => new
                {
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    Grado_Academico = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Especialidad = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Fecha_Contratacion = table.Column<DateTime>(type: "date", nullable: false),
                    Estatus = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Bio = table.Column<string>(type: "text", nullable: true, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Usuario);
                    table.ForeignKey(
                        name: "FK_Profesor_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "ID_Usuario");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Usuario_AreaAcademica",
                columns: table => new
                {
                    ID_AreaAcademica = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<string>(type: "enum('Activo','Inactivo')", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.ID_AreaAcademica, x.ID_Usuario })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "CK_UsuarioAreaAcademica1",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "ID_Usuario");
                    table.ForeignKey(
                        name: "CK_UsuarioAreaAcademica2",
                        column: x => x.ID_AreaAcademica,
                        principalTable: "AreaAcademica",
                        principalColumn: "AreaAcademica_ID");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Usuario_RefreshToken",
                columns: table => new
                {
                    ID_RefreshToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Fecha_Creacion = table.Column<DateOnly>(type: "date", nullable: false),
                    Fecha_Expiracion = table.Column<DateOnly>(type: "date", nullable: false),
                    ID_Usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_RefreshToken);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "ID_Usuario");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Usuario_Rol",
                columns: table => new
                {
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    ID_Rol = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<string>(type: "enum('Activo','Inactivo')", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.ID_Usuario, x.ID_Rol })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "CK_UsuarioRol1",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "ID_Usuario");
                    table.ForeignKey(
                        name: "CK_UsuarioRol2",
                        column: x => x.ID_Rol,
                        principalTable: "Rol",
                        principalColumn: "Rol_ID");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "UsuarioTarifario",
                columns: table => new
                {
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    ID_Tarifario = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Fecha_Inscripcion = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.ID_Usuario, x.ID_Tarifario })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "CK_UsuarioTarifario1",
                        column: x => x.ID_Tarifario,
                        principalTable: "Tarifario",
                        principalColumn: "ID_Tarifario");
                    table.ForeignKey(
                        name: "CK_UsuarioTarifario2",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "ID_Usuario");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Usuario_ProgramaAcademico",
                columns: table => new
                {
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    ID_ProgramaAcademico = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Fecha_Inscripcion = table.Column<DateOnly>(type: "date", nullable: false),
                    Permanencia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.ID_Usuario, x.ID_ProgramaAcademico })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "CK_Usuario_ProgramaAcademico1",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "ID_Usuario");
                    table.ForeignKey(
                        name: "CK_Usuario_ProgramaAcademico2",
                        column: x => x.ID_ProgramaAcademico,
                        principalTable: "ProgramaAcademico",
                        principalColumn: "ProgramaAcademico_ID");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Factura",
                columns: table => new
                {
                    ID_Factura = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    NumeroComprobante = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Estatus = table.Column<string>(type: "enum('Pagada','Anulada')", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Subtotal = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    ID_CuentaPorPagar = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_Factura);
                    table.ForeignKey(
                        name: "FK_Factura_CPP",
                        column: x => x.ID_CuentaPorPagar,
                        principalTable: "CuentaPorPagar",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Seccion",
                columns: table => new
                {
                    Seccion_ID = table.Column<int>(type: "int", nullable: false),
                    ID_Asignatura = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Numero_Seccion = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Cupo = table.Column<int>(type: "int", nullable: false),
                    Cupo_Disponible = table.Column<int>(type: "int", nullable: false),
                    Periodo_Academico = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Estatus = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Modalidad = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    ID_Profesor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.Seccion_ID);
                    table.ForeignKey(
                        name: "FK_Seccion_Asignatura",
                        column: x => x.ID_Asignatura,
                        principalTable: "Asignatura",
                        principalColumn: "Asignatura_ID");
                    table.ForeignKey(
                        name: "FK_Seccion_Profesor",
                        column: x => x.ID_Profesor,
                        principalTable: "Profesor",
                        principalColumn: "ID_Usuario");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "DetalleFactura",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Factura = table.Column<int>(type: "int", nullable: false),
                    Concepto = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    Monto = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(10)", precision: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Factura_Detalles",
                        column: x => x.ID_Factura,
                        principalTable: "Factura",
                        principalColumn: "ID_Factura");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "SeccionHorario",
                columns: table => new
                {
                    ID_SeccionHorario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Seccion = table.Column<int>(type: "int", nullable: false),
                    Dia = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false, collation: "utf8_general_ci")
                        .Annotation("MySql:CharSet", "utf8"),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeOnly>(type: "time", nullable: false),
                    ID_Aula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID_SeccionHorario);
                    table.ForeignKey(
                        name: "FK_SeccionHorario_Aula",
                        column: x => x.ID_Aula,
                        principalTable: "Aula",
                        principalColumn: "ID_Aula");
                    table.ForeignKey(
                        name: "FK_SeccionHorario_Seccion",
                        column: x => x.ID_Seccion,
                        principalTable: "Seccion",
                        principalColumn: "Seccion_ID");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateTable(
                name: "Seleccion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ID_Usuario = table.Column<int>(type: "int", nullable: false),
                    ID_Seccion = table.Column<int>(type: "int", nullable: false),
                    ID_Periodo = table.Column<int>(type: "int", nullable: false),
                    Calificacion = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    EstatusAcademico = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Seleccion_Periodo",
                        column: x => x.ID_Periodo,
                        principalTable: "PeriodoConfig",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Seleccion_Seccion",
                        column: x => x.ID_Seccion,
                        principalTable: "Seccion",
                        principalColumn: "Seccion_ID");
                    table.ForeignKey(
                        name: "FK_Seleccion_Usuario",
                        column: x => x.ID_Usuario,
                        principalTable: "Usuario",
                        principalColumn: "ID_Usuario");
                })
                .Annotation("MySql:CharSet", "utf8")
                .Annotation("Relational:Collation", "utf8_general_ci");

            migrationBuilder.CreateIndex(
                name: "FK_Asignatura_AreaAcademica",
                table: "Asignatura",
                column: "IdAreaAcademica");

            migrationBuilder.CreateIndex(
                name: "FK_AsignaturaProgramaAcademico_Corequisito",
                table: "Asignatura_ProgramaAcademico",
                column: "Corequisito");

            migrationBuilder.CreateIndex(
                name: "FK_Aula_Edificio",
                table: "Aula",
                column: "ID_Edificio");

            migrationBuilder.CreateIndex(
                name: "FK_Carrera_AreaAcademica",
                table: "Carrera",
                column: "IdAreaAcademica");

            migrationBuilder.CreateIndex(
                name: "FK_CPP_Usuario",
                table: "CuentaPorPagar",
                column: "ID_Usuario");

            migrationBuilder.CreateIndex(
                name: "FK_Factura_Detalles",
                table: "DetalleFactura",
                column: "ID_Factura");

            migrationBuilder.CreateIndex(
                name: "FK_Factura_CPP",
                table: "Factura",
                column: "ID_CuentaPorPagar");

            migrationBuilder.CreateIndex(
                name: "IX_Preseleccion_ID_Asignatura",
                table: "Preseleccion",
                column: "ID_Asignatura");

            migrationBuilder.CreateIndex(
                name: "IX_Preseleccion_ID_Periodo",
                table: "Preseleccion",
                column: "ID_Periodo");

            migrationBuilder.CreateIndex(
                name: "IX_Preseleccion_ID_Usuario",
                table: "Preseleccion",
                column: "ID_Usuario");

            migrationBuilder.CreateIndex(
                name: "FK_Carrera_ProgramaAcademico",
                table: "ProgramaAcademico",
                column: "ID_Carrera");

            migrationBuilder.CreateIndex(
                name: "FK_Seccion_Asignatura",
                table: "Seccion",
                column: "ID_Asignatura");

            migrationBuilder.CreateIndex(
                name: "FK_Seccion_Profesor",
                table: "Seccion",
                column: "ID_Profesor");

            migrationBuilder.CreateIndex(
                name: "FK_SeccionHorario_Aula",
                table: "SeccionHorario",
                column: "ID_Aula");

            migrationBuilder.CreateIndex(
                name: "FK_SeccionHorario_Seccion",
                table: "SeccionHorario",
                column: "ID_Seccion");

            migrationBuilder.CreateIndex(
                name: "IX_Seleccion_ID_Periodo",
                table: "Seleccion",
                column: "ID_Periodo");

            migrationBuilder.CreateIndex(
                name: "IX_Seleccion_ID_Seccion",
                table: "Seleccion",
                column: "ID_Seccion");

            migrationBuilder.CreateIndex(
                name: "IX_Seleccion_ID_Usuario",
                table: "Seleccion",
                column: "ID_Usuario");

            migrationBuilder.CreateIndex(
                name: "FK_Rol_Usuario",
                table: "Usuario",
                column: "ID_Rol");

            migrationBuilder.CreateIndex(
                name: "CK_UsuarioAreaAcademica1",
                table: "Usuario_AreaAcademica",
                column: "ID_Usuario");

            migrationBuilder.CreateIndex(
                name: "CK_Usuario_ProgramaAcademico2",
                table: "Usuario_ProgramaAcademico",
                column: "ID_ProgramaAcademico");

            migrationBuilder.CreateIndex(
                name: "FK_RefreshToken_Usuario",
                table: "Usuario_RefreshToken",
                column: "ID_Usuario");

            migrationBuilder.CreateIndex(
                name: "CK_UsuarioRol2",
                table: "Usuario_Rol",
                column: "ID_Rol");

            migrationBuilder.CreateIndex(
                name: "CK_UsuarioTarifario1",
                table: "UsuarioTarifario",
                column: "ID_Tarifario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asignatura_ProgramaAcademico");

            migrationBuilder.DropTable(
                name: "DetalleFactura");

            migrationBuilder.DropTable(
                name: "Preseleccion");

            migrationBuilder.DropTable(
                name: "SeccionHorario");

            migrationBuilder.DropTable(
                name: "Seleccion");

            migrationBuilder.DropTable(
                name: "Usuario_AreaAcademica");

            migrationBuilder.DropTable(
                name: "Usuario_ProgramaAcademico");

            migrationBuilder.DropTable(
                name: "Usuario_RefreshToken");

            migrationBuilder.DropTable(
                name: "Usuario_Rol");

            migrationBuilder.DropTable(
                name: "UsuarioTarifario");

            migrationBuilder.DropTable(
                name: "Factura");

            migrationBuilder.DropTable(
                name: "Aula");

            migrationBuilder.DropTable(
                name: "PeriodoConfig");

            migrationBuilder.DropTable(
                name: "Seccion");

            migrationBuilder.DropTable(
                name: "ProgramaAcademico");

            migrationBuilder.DropTable(
                name: "Tarifario");

            migrationBuilder.DropTable(
                name: "CuentaPorPagar");

            migrationBuilder.DropTable(
                name: "Edificio");

            migrationBuilder.DropTable(
                name: "Asignatura");

            migrationBuilder.DropTable(
                name: "Profesor");

            migrationBuilder.DropTable(
                name: "Carrera");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "AreaAcademica");

            migrationBuilder.DropTable(
                name: "Rol");
        }
    }
}
