using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;

public interface IPreseleccionRepository
{
    Task<UsuarioProgramaAcademico?> GetUsuarioProgramaAsync(int usuarioId);
    Task<IEnumerable<AsignaturaProgramaAcademico>> GetAsignaturasByProgramaAsync(int programaId);
    Task<IEnumerable<HistorialAcademico>> GetHistorialByUsuarioAsync(int usuarioId);
    Task<IEnumerable<Seccion>> GetSeccionesByPeriodoAsync(string periodoCodigo);
    Task<IEnumerable<Seccion>> GetSeccionesByIdsAsync(List<int> seccionIds);
    Task<IEnumerable<Preseleccion>> GetByUsuarioAndPeriodoAsync(int usuarioId, int periodoId);
    Task<IEnumerable<Preseleccion>> GetByUsuarioAndPeriodoAllAsync(int usuarioId, int periodoId);
    Task<Preseleccion?> GetByIdAsync(int id);
    Task AddAsync(Preseleccion preseleccion);
    Task UpdateAsync(Preseleccion preseleccion);
    Task DeleteAsync(Preseleccion preseleccion);
    Task UpdateSeccionAsync(Seccion seccion);
}

public interface ISeleccionRepository
{
    Task AddAsync(Seleccion seleccion);
    Task<IEnumerable<Seleccion>> GetByUsuarioAndPeriodoAsync(int usuarioId, int periodoId);
}
