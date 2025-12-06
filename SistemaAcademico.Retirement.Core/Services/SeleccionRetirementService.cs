using SistemaAcademico.Retirement.Core.DTOs;
using SistemaAcademico.Retirement.Core.Interfaces;
using SistemaAcademico.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademico.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademico.Retirement.Core.Services
{
    internal class SeleccionRetirementService(SistemaAcademicoContext context) : ISeleccionRetirementService
    {

        public async Task<string> Retirar(SeleccionRetirementDTO slrDTO)
        {
            var rets = await context.Seleccions.CountAsync(rets => rets.Asignatura == slrDTO.Asignatura && rets.IdUsuario == slrDTO.IdUsuario);
            if(rets >= 3) return "Ya se ha retirado 3 veces en la misma asignatura";
            var sel = await context.Seleccions.FirstOrDefaultAsync(sel => sel.IdSeccion == slrDTO.IdSeccion && sel.IdUsuario == slrDTO.IdUsuario);
            if (sel == null) return "Usted no está inscrito a esta seccion de la asignatura";
            sel.Estado = "retirado";
            
            context.SaveChanges();
            
            return "Ha retirado la asignatura este trimestre";
        }
    }
}
