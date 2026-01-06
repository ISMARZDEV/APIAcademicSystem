using System.Collections.Generic;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Preseleccion;
using SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Seleccion;

namespace SistemaAcademico.SelecctionAndPreselecction.Core.DTOs.Seleccion;

public class ResumenSeleccionResponseDto
{
    public ResumenCargaDto ResumenCarga { get; set; } = new();
    public IEnumerable<SeleccionResumenDto> Resumen { get; set; } = new List<SeleccionResumenDto>();
}
