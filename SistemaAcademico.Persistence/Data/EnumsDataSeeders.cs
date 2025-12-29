namespace SistemaAcademico.Persistence.Data
{
    public enum EstatusSeccion
    {
        Activa,
        Cerrada,
        Cancelada
    }

    public enum ModalidadSeccion
    {
        Presencial,
        Virtual,
        Hibrida
    }

    public enum TipoAsignatura
    {
        Teoría,
        Laboratorio,
        Electiva
    }

    public enum EstatusPrograma
    {
        Activo,
        Inactivo
    }

    public enum EstatusProfesor
    {
        Activo,
        Licencia,
        Inactivo
    }

    public enum PeriodoEstatus
    {
        Planificacion,
        Activo,
        Cerrado
    }

    public enum SeleccionEstatus
    {
        Inscrito,
        Cursando,
        Aprobado,
        Reprobado,
        Retirado
    }

    public enum DiaSemana
    {
        Lunes,
        Martes,
        Miercoles,
        Jueves,
        Viernes,
        Sabado,
        Domingo
    }
}
