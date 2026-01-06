using System.Collections.Generic;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;

public class ResumenPreseleccionResponseDto
{
    public ResumenCargaDto ResumenCarga { get; set; } = new();
    public IEnumerable<PreseleccionResumenDto> Resumen { get; set; } = new List<PreseleccionResumenDto>();
}
