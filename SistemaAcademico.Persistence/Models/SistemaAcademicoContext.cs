using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace SistemaAcademico.Persistence.Models;

public partial class SistemaAcademicoContext : DbContext
{
    public SistemaAcademicoContext()
    {
    }

    public SistemaAcademicoContext(DbContextOptions<SistemaAcademicoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AreaAcademica> AreaAcademicas { get; set; }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<AsignaturaProgramaAcademico> AsignaturaProgramaAcademicos { get; set; }

    public virtual DbSet<Aula> Aulas { get; set; }

    public virtual DbSet<Carrera> Carreras { get; set; }

    public virtual DbSet<CuentaPorPagar> CuentaPorPagars { get; set; }

    public virtual DbSet<DetalleFactura> DetalleFacturas { get; set; }

    public virtual DbSet<Edificio> Edificios { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<ProgramaAcademico> ProgramaAcademicos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Seccion> Seccions { get; set; }

    public virtual DbSet<SeccionHorario> SeccionHorarios { get; set; }

    public virtual DbSet<Seleccion> Seleccions { get; set; }

    public virtual DbSet<Tarifario> Tarifarios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioAreaAcademica> UsuarioAreaAcademicas { get; set; }

    public virtual DbSet<UsuarioProgramaAcademico> UsuarioProgramaAcademicos { get; set; }

    public virtual DbSet<UsuarioRefreshToken> UsuarioRefreshTokens { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }

    public virtual DbSet<UsuarioTarifario> UsuarioTarifarios { get; set; }

    /* ESTO SE MOVIO AL API GATEWAY
     * 
     * protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=bolzz0zm2tnkvjiuf56w-mysql.services.clever-cloud.com;port=3306;database=bolzz0zm2tnkvjiuf56w;user=uxhmzwsqo4x2jabr;password=fzPC670ZBPqowlD0gFCs;sslmode=Required", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.22-mysql"));
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<AreaAcademica>(entity =>
        {
            entity.HasKey(e => e.AreaAcademicaId).HasName("PRIMARY");

            entity.ToTable("AreaAcademica");

            entity.Property(e => e.AreaAcademicaId)
                .HasMaxLength(3)
                .HasColumnName("AreaAcademica_ID");
            entity.Property(e => e.AreaAcademicaNombre)
                .HasMaxLength(50)
                .HasColumnName("AreaAcademica_Nombre");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.AsignaturaId).HasName("PRIMARY");

            entity.ToTable("Asignatura");

            entity.HasIndex(e => e.IdAreaAcademica, "FK_Asignatura_AreaAcademica");

            entity.Property(e => e.AsignaturaId).HasColumnName("Asignatura_ID");
            entity.Property(e => e.IdAreaAcademica).HasMaxLength(3);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Tipo).HasMaxLength(20);

            entity.HasOne(d => d.IdAreaAcademicaNavigation).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.IdAreaAcademica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Asignatura_AreaAcademica");
        });

        modelBuilder.Entity<AsignaturaProgramaAcademico>(entity =>
        {
            entity.HasKey(e => new { e.IdAsignatura, e.IdProgramaAcademico })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Asignatura_ProgramaAcademico");

            entity.HasIndex(e => e.Corequisito, "FK_AsignaturaProgramaAcademico_Corequisito");

            entity.Property(e => e.IdAsignatura).HasColumnName("Id_Asignatura");
            entity.Property(e => e.IdProgramaAcademico).HasColumnName("Id_ProgramaAcademico");
            entity.Property(e => e.PreRequisitos).HasColumnType("json");

            entity.HasOne(d => d.CorequisitoNavigation).WithMany(p => p.AsignaturaProgramaAcademicoCorequisitoNavigations)
                .HasForeignKey(d => d.Corequisito)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AsignaturaProgramaAcademico_Corequisito");

            entity.HasOne(d => d.IdAsignaturaNavigation).WithMany(p => p.AsignaturaProgramaAcademicoIdAsignaturaNavigations)
                .HasForeignKey(d => d.IdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_AsignaturaProgramaAcademico1");
        });

        modelBuilder.Entity<Aula>(entity =>
        {
            entity.HasKey(e => e.IdAula).HasName("PRIMARY");

            entity.ToTable("Aula");

            entity.HasIndex(e => e.IdEdificio, "FK_Aula_Edificio");

            entity.Property(e => e.IdAula).HasColumnName("ID_Aula");
            entity.Property(e => e.IdEdificio).HasColumnName("ID_Edificio");
            entity.Property(e => e.Nombre).HasMaxLength(20);

            entity.HasOne(d => d.IdEdificioNavigation).WithMany(p => p.Aulas)
                .HasForeignKey(d => d.IdEdificio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Aula_Edificio");
        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.CarreraId).HasName("PRIMARY");

            entity.ToTable("Carrera");

            entity.HasIndex(e => e.IdAreaAcademica, "FK_Carrera_AreaAcademica");

            entity.Property(e => e.CarreraId).HasColumnName("Carrera_ID");
            entity.Property(e => e.IdAreaAcademica).HasMaxLength(3);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Nomenclatura).HasMaxLength(20);

            entity.HasOne(d => d.IdAreaAcademicaNavigation).WithMany(p => p.Carreras)
                .HasForeignKey(d => d.IdAreaAcademica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carrera_AreaAcademica");
        });

        modelBuilder.Entity<CuentaPorPagar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("CuentaPorPagar");

            entity.HasIndex(e => e.IdUsuario, "FK_CPP_Usuario");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CantidadRestante).HasPrecision(10);
            entity.Property(e => e.CantidadTotal).HasPrecision(10);
            entity.Property(e => e.Descuento).HasPrecision(10);
            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.CuentaPorPagars)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CPP_Usuario");
        });

        modelBuilder.Entity<DetalleFactura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("DetalleFactura");

            entity.HasIndex(e => e.IdFactura, "FK_Factura_Detalles");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Concepto).HasMaxLength(150);
            entity.Property(e => e.Descuento).HasPrecision(10);
            entity.Property(e => e.IdFactura).HasColumnName("ID_Factura");
            entity.Property(e => e.Monto).HasPrecision(10);
            entity.Property(e => e.MontoTotal).HasPrecision(10);
            entity.Property(e => e.Subtotal).HasPrecision(10);

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleFacturas)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_Detalles");
        });

        modelBuilder.Entity<Edificio>(entity =>
        {
            entity.HasKey(e => e.IdEdificio).HasName("PRIMARY");

            entity.ToTable("Edificio");

            entity.Property(e => e.IdEdificio).HasColumnName("ID_Edificio");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Nomenclatura).HasMaxLength(10);
        });

        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PRIMARY");

            entity.ToTable("Factura");

            entity.HasIndex(e => e.IdCuentaPorPagar, "FK_Factura_CPP");

            entity.Property(e => e.IdFactura).HasColumnName("ID_Factura");
            entity.Property(e => e.Descuento).HasPrecision(10);
            entity.Property(e => e.Estatus).HasColumnType("enum('Pagada','Anulada')");
            entity.Property(e => e.IdCuentaPorPagar).HasColumnName("ID_CuentaPorPagar");
            entity.Property(e => e.MontoTotal).HasPrecision(10);
            entity.Property(e => e.NumeroComprobante).HasMaxLength(30);
            entity.Property(e => e.Subtotal).HasPrecision(10);

            entity.HasOne(d => d.IdCuentaPorPagarNavigation).WithMany(p => p.Facturas)
                .HasForeignKey(d => d.IdCuentaPorPagar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Factura_CPP");
        });

        modelBuilder.Entity<ProgramaAcademico>(entity =>
        {
            entity.HasKey(e => e.ProgramaAcademicoId).HasName("PRIMARY");

            entity.ToTable("ProgramaAcademico");

            entity.HasIndex(e => e.IdCarrera, "FK_Carrera_ProgramaAcademico");

            entity.Property(e => e.ProgramaAcademicoId)
                .ValueGeneratedNever()
                .HasColumnName("ProgramaAcademico_ID");
            entity.Property(e => e.Estatus).HasMaxLength(20);
            entity.Property(e => e.IdCarrera).HasColumnName("ID_Carrera");
            entity.Property(e => e.Periodo).HasMaxLength(10);
            entity.Property(e => e.TotalCreditos).HasColumnName("Total_Creditos");

            entity.HasOne(d => d.IdCarreraNavigation).WithMany(p => p.ProgramaAcademicos)
                .HasForeignKey(d => d.IdCarrera)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carrera_ProgramaAcademico");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PRIMARY");

            entity.ToTable("Rol");

            entity.Property(e => e.RolId).HasColumnName("Rol_ID");
            entity.Property(e => e.Descripcion).HasMaxLength(50);
        });

        modelBuilder.Entity<Seccion>(entity =>
        {
            entity.HasKey(e => e.SeccionId).HasName("PRIMARY");

            entity.ToTable("Seccion");

            entity.HasIndex(e => e.IdAsignatura, "FK_Seccion_Asignatura");

            entity.HasIndex(e => e.IdProfesor, "FK_Seccion_Profesor");

            entity.Property(e => e.SeccionId)
                .ValueGeneratedNever()
                .HasColumnName("Seccion_ID");
            entity.Property(e => e.IdAsignatura).HasColumnName("ID_Asignatura");
            entity.Property(e => e.IdProfesor).HasColumnName("ID_Profesor");
            entity.Property(e => e.NumeroSeccion)
                .HasMaxLength(10)
                .HasColumnName("Numero_Seccion");

            entity.HasOne(d => d.IdAsignaturaNavigation).WithMany(p => p.Seccions)
                .HasForeignKey(d => d.IdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Seccion_Asignatura");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.Seccions)
                .HasForeignKey(d => d.IdProfesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Seccion_Profesor");
        });

        modelBuilder.Entity<SeccionHorario>(entity =>
        {
            entity.HasKey(e => e.IdSeccionHorario).HasName("PRIMARY");

            entity.ToTable("SeccionHorario");

            entity.HasIndex(e => e.IdAula, "FK_SeccionHorario_Aula");

            entity.Property(e => e.IdSeccionHorario).HasColumnName("ID_SeccionHorario");
            entity.Property(e => e.Dia).HasMaxLength(15);
            entity.Property(e => e.HoraFin).HasColumnType("time");
            entity.Property(e => e.HoraInicio).HasColumnType("time");
            entity.Property(e => e.IdAula).HasColumnName("ID_Aula");

            entity.HasOne(d => d.IdAulaNavigation).WithMany(p => p.SeccionHorarios)
                .HasForeignKey(d => d.IdAula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SeccionHorario_Aula");
        });

        modelBuilder.Entity<Seleccion>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdSeccion })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Seleccion");

            entity.HasIndex(e => e.IdSeccion, "CK_Seleccion_Seccion");

            entity.HasIndex(e => e.Asignatura, "FK_Seleccion_Asignatura");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.IdSeccion).HasColumnName("ID_Seccion");
            entity.Property(e => e.Asignatura).HasMaxLength(150);
            entity.Property(e => e.Comentario).HasMaxLength(255);
            entity.Property(e => e.Estado).HasColumnType("enum('Cursando','Aprobando','Retirado')");
            entity.Property(e => e.PeriodoAcademico).HasMaxLength(10);

            entity.HasOne(d => d.IdSeccionNavigation).WithMany(p => p.Seleccions)
                .HasForeignKey(d => d.IdSeccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_Seleccion_Seccion");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Seleccions)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_Seleccion_Usuario");
        });

        modelBuilder.Entity<Tarifario>(entity =>
        {
            entity.HasKey(e => e.IdTarifario).HasName("PRIMARY");

            entity.ToTable("Tarifario");

            entity.Property(e => e.IdTarifario).HasColumnName("ID_Tarifario");
            entity.Property(e => e.Estatus).HasColumnType("enum('Activo','Inactivo')");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PeriodoAcademico).HasMaxLength(10);
            entity.Property(e => e.Precio).HasPrecision(10);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.IdRol, "FK_Rol_Usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.Apellido).HasMaxLength(100);
            entity.Property(e => e.ClaveHash)
                .HasMaxLength(255)
                .HasColumnName("Clave_Hash");
            entity.Property(e => e.CorreoInstitucional)
                .HasMaxLength(255)
                .HasColumnName("Correo_Institucional");
            entity.Property(e => e.CorreoPersonal)
                .HasMaxLength(255)
                .HasColumnName("Correo_Personal");
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.FechaIngreso).HasColumnName("Fecha_Ingreso");
            entity.Property(e => e.IdRol).HasColumnName("ID_Rol");
            entity.Property(e => e.Nacionalidad).HasMaxLength(50);
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(11);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rol_Usuario");
        });

        modelBuilder.Entity<UsuarioAreaAcademica>(entity =>
        {
            entity.HasKey(e => new { e.IdAreaAcademica, e.IdUsuario })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Usuario_AreaAcademica");

            entity.HasIndex(e => e.IdUsuario, "CK_UsuarioAreaAcademica1");

            entity.Property(e => e.IdAreaAcademica)
                .HasMaxLength(3)
                .HasColumnName("ID_AreaAcademica");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.Estatus).HasColumnType("enum('Activo','Inactivo')");

            entity.HasOne(d => d.IdAreaAcademicaNavigation).WithMany(p => p.UsuarioAreaAcademicas)
                .HasForeignKey(d => d.IdAreaAcademica)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_UsuarioAreaAcademica2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioAreaAcademicas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_UsuarioAreaAcademica1");
        });

        modelBuilder.Entity<UsuarioProgramaAcademico>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdProgramaAcademico })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Usuario_ProgramaAcademico");

            entity.HasIndex(e => e.IdProgramaAcademico, "CK_Usuario_ProgramaAcademico2");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.IdProgramaAcademico).HasColumnName("ID_ProgramaAcademico");
            entity.Property(e => e.Estatus).HasMaxLength(20);
            entity.Property(e => e.FechaInscripcion).HasColumnName("Fecha_Inscripcion");

            entity.HasOne(d => d.IdProgramaAcademicoNavigation).WithMany(p => p.UsuarioProgramaAcademicos)
                .HasForeignKey(d => d.IdProgramaAcademico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_Usuario_ProgramaAcademico2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioProgramaAcademicos)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_Usuario_ProgramaAcademico1");
        });

        modelBuilder.Entity<UsuarioRefreshToken>(entity =>
        {
            entity.HasKey(e => e.IdRefreshToken).HasName("PRIMARY");

            entity.ToTable("Usuario_RefreshToken");

            entity.HasIndex(e => e.IdUsuario, "FK_RefreshToken_Usuario");

            entity.Property(e => e.IdRefreshToken).HasColumnName("ID_RefreshToken");
            entity.Property(e => e.FechaCreacion).HasColumnName("Fecha_Creacion");
            entity.Property(e => e.FechaExpiracion).HasColumnName("Fecha_Expiracion");
            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.Token).HasMaxLength(500);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioRefreshTokens)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_Usuario");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdRol })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("Usuario_Rol");

            entity.HasIndex(e => e.IdRol, "CK_UsuarioRol2");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.IdRol).HasColumnName("ID_Rol");
            entity.Property(e => e.Estatus).HasColumnType("enum('Activo','Inactivo')");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_UsuarioRol2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_UsuarioRol1");
        });

        modelBuilder.Entity<UsuarioTarifario>(entity =>
        {
            entity.HasKey(e => new { e.IdUsuario, e.IdTarifario })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("UsuarioTarifario");

            entity.HasIndex(e => e.IdTarifario, "CK_UsuarioTarifario1");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_Usuario");
            entity.Property(e => e.IdTarifario).HasColumnName("ID_Tarifario");
            entity.Property(e => e.Estatus).HasMaxLength(20);
            entity.Property(e => e.FechaInscripcion).HasColumnName("Fecha_Inscripcion");

            entity.HasOne(d => d.IdTarifarioNavigation).WithMany(p => p.UsuarioTarifarios)
                .HasForeignKey(d => d.IdTarifario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_UsuarioTarifario1");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioTarifarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CK_UsuarioTarifario2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
