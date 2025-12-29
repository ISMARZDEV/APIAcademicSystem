using System;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Core.Interfaces;

public interface ICareerRepository
{
    ICollection<Carrera> GetCarreers();
    Carrera? GetCarreerById(int careerId);
    bool CarreerExists(int careerId);
    bool CarreerNameExists(string careerId);

    /*
    - GetCarrearsByAcademicArea
    → Recibe un int careerId y devuelve los AcadmicsPrograms de esa carrera en ICollection del tipo ProgramaAcademico.
    */

    /*
    - SearchCarreer
    → Recibe un nombre y devuelve las carreras que coincidan en ICollection del tipo Carrera.
    */
}
