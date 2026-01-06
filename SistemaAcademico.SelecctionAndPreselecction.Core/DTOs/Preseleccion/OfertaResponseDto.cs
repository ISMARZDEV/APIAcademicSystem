using System.Collections.Generic;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;

public class OfertaResponseDto
{
    public ResumenCargaDto ResumenCarga { get; set; } = new();
    public IEnumerable<OfertaAsignaturaDto> Oferta { get; set; } = new List<OfertaAsignaturaDto>();
    public int Page { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
}
