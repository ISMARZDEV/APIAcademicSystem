using SistemaAcademico.Retirement.Core.DTOs;
using SistemaAcademico.Retirement.Core.Interfaces;
using SistemaAcademico.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademico.Persistence.Models;
using SistemaAcademico.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademico.Retirement.Core.Services
{
    public class SeleccionRetirementService(SistemaAcademicoContext context) : ISeleccionRetirementService
    {

        public async Task<string> Retirar(SeleccionRetirementDTO slrDTO)
        {
            var rets = await context.Seleccions
                .Include(s => s.IdSeccionNavigation)
                .CountAsync(rets => rets.IdSeccionNavigation.IdAsignatura == slrDTO.Asignatura
                    && rets.IdUsuario == slrDTO.IdUsuario
                    && rets.EstatusAcademico == SeleccionEstatus.Retirado);

            if (rets >= 3) return "Ya se ha retirado 3 veces en la misma asignatura";

            var sel = await context.Seleccions.FirstOrDefaultAsync(sel => sel.IdSeccion == slrDTO.IdSeccion
                && sel.IdUsuario == slrDTO.IdUsuario
                && sel.EstatusAcademico != SeleccionEstatus.Retirado);

            if (sel == null) return "Usted no está inscrito a esta sección de la asignatura, esta no existe o ya se ha retirado";

            sel.EstatusAcademico = SeleccionEstatus.Retirado;
            // sel.Comentario = slrDTO.Comentario; // Comentario removed from Seleccion model per new schema

            await context.SaveChangesAsync();
            
            return "Ha retirado la asignatura este trimestre";
        }
    }
}
