using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Seleccion;
using SistemaAcademico.Persistence.Data;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;

public interface IPreseleccionService
{
    Task<OfertaResponseDto> GetOfertaAsync(
        int usuarioId, 
        string? searchTerm = null, 
        TipoAsignatura? tipo = null, 
        bool soloDisponibles = false, 
        ModalidadSeccion? modalidad = null,
        int? periodo = null,
        int page = 1,
        int itemsPerPage = 5);
    Task<AccionPreseleccionResponseDto> GuardarPreseleccionAsync(int usuarioId, List<int> seccionIds);
    Task<ResumenPreseleccionResponseDto> GetResumenAsync(int usuarioId);
    Task<AccionPreseleccionResponseDto> CancelarPreseleccionAsync(int id, int usuarioId);
}

public interface ISeleccionService
{
    Task<AccionPreseleccionResponseDto> SeleccionarAsync(int usuarioId, int seccionId);
    Task<ResumenSeleccionResponseDto> GetResumenAsync(int usuarioId);
    Task<bool> ConfirmarPreseleccionAsync(int usuarioId);
}
