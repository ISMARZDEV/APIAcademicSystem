using System;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Core.Interfaces;

public interface ISubjectsRepository
{
    ICollection<Asignatura> GetSubjects(string? search, int page, int itemsPerPage);
    int GetTotalSubjects();
    Asignatura? GetSubjectById(string subjectId);
    bool SubjectExists(string subjectId);
    bool SubjectNameExists(string subjectName);

    /*
    - GetSubjectsByAcademicArea
    → Recibe un string academicAreaId y devuelve las subjects de esa Area academica en ICollection del tipo Subjects.
    */

    /*
    - GetSubjectsByType
    → Recibe un int type y devuelve las subjects de esos Tipos en ICollection del tipo ProgramaAcademico.
    */

    /*
    - SearchSubjectName
    → Recibe un nombre y devuelve las asignaturas que coincidan en ICollection del tipo Asignatura.
    */
}
