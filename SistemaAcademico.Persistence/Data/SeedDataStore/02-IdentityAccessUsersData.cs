using System;
using System.Collections.Generic;
using SistemaAcademico.Persistence.Models;

namespace SistemaAcademico.Persistence.Data;

public static class IdentityAccessUsersData
{

    public static List<Rol> GetRols() => new()
    {
        new Rol { RolId = 1, Descripcion = "Profesor" },
        new Rol { RolId = 2, Descripcion = "Estudiante" },
        new Rol { RolId = 3, Descripcion = "Administrador" }
    };

    public static List<Usuario> GetUsuarios() => new()
    {
        new Usuario
        {
            IdUsuario = 1,
            Nombre = "Juan",
            Apellido = "Perez",
            Nacionalidad = "Dominicano",
            Direccion = "Calle 123",
            Telefono = "8091234567",
            CorreoPersonal = "juan.perez@gmail.com",
            CorreoInstitucional = "j.perez@institucion.edu.do",
            FechaIngreso = DateOnly.FromDateTime(DateTime.Now),
            ClaveHash = "hash",
            IdRol = 1
        },
        new Usuario
        {
            IdUsuario = 2,
            Nombre = "Maria",
            Apellido = "Gomez",
            Nacionalidad = "Dominicana",
            Direccion = "Av. Winston Churchill",
            Telefono = "8095551234",
            CorreoPersonal = "maria.gomez@gmail.com",
            CorreoInstitucional = "m.gomez@institucion.edu.do",
            FechaIngreso = DateOnly.FromDateTime(new DateTime(2024, 1, 10)),
            ClaveHash = "hash",
            IdRol = 2 // Estudiante
        }
    };

}
