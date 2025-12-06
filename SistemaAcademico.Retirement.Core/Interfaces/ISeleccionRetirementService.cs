using SistemaAcademico.Retirement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Retirement.Core.Interfaces
{
    public interface ISeleccionRetirementService
    {
        Task<string> Retirar(SeleccionRetirementDTO seleccionRetirementDTO);
    }
}
