using System;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Repositories;

public class CareerRepository : ICareerRepository
{
    public SistemaAcademicoContext _db;
    public CareerRepository(SistemaAcademicoContext db)
    {
        _db = db;
    }
    public ICollection<Carrera> GetCarreers()
    {
        return _db.Carreras
            .Include(c => c.IdAreaAcademicaNavigation)
            .OrderBy(c => c.CarreraId)
            .ToList();
    }
    public Carrera? GetCarreerById(int careerId)
    {
        if (careerId <= 0)
        {
            return null;
        }
        return _db.Carreras
            .Include(c => c.IdAreaAcademicaNavigation)
            .FirstOrDefault(p => p.CarreraId == careerId);
    }
    public bool CarreerExists(int careerId)
    {
        if (careerId <= 0)
        {
            return false;
        }

        return _db.Carreras.Any(c => c.CarreraId == careerId);
    }
    public bool CarreerNameExists(string carreerName)
    {
        if (string.IsNullOrWhiteSpace(carreerName))
        {
            return false;
        }
        return _db.Carreras.Any(c => c.Nombre.ToLower().Trim() == carreerName.ToLower().Trim());
    }

}
