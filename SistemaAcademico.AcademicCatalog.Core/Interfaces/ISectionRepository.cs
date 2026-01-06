using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Core.Interfaces;

public interface ISectionRepository
{
    ICollection<Seccion> GetSections(string? subjectId = null);
}
