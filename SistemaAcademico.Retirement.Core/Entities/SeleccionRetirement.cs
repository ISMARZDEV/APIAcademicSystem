using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Retirement.Core.Entities
{
    internal class SeleccionRetirement
    {
        public int IdUsuario { get; set; }

        public int IdSeccion { get; set; }

        public string PeriodoAcademico { get; set; } = null!;

        public string Asignatura { get; set; } = null!;
        public string Comentario { get; set; } = null!;
    }
}
