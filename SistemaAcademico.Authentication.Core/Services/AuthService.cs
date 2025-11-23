using SistemaAcademico.Authentication.Core.DTOs;
using SistemaAcademico.Authentication.Core.Entities;
using SistemaAcademico.Authentication.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Authentication.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly SistemaAcademicoContext _context; 

        public AuthService(SistemaAcademicoContext context)
        {
            _context = context;
        }

        public async Task<int> CrearUsuarioAsync(CreateUserDto dto)
        {
            // 1. Validar duplicados (Correo Institucional debe ser único)
            bool existe = await _context.Usuarios
                .AnyAsync(u => u.CorreoInstitucional == dto.CorreoInstitucional);

            if (existe)
                throw new Exception($"El usuario {dto.CorreoInstitucional} ya existe.");

            // 2. Hashear la contraseña (Seguridad)
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // 3. Crear la Entidad (Mapeo)
            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Nacionalidad = dto.Nacionalidad,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                CorreoPersonal = dto.CorreoPersonal,
                CorreoInstitucional = dto.CorreoInstitucional,

                // Adaptación: Tu modelo usa DateOnly
                FechaIngreso = DateOnly.FromDateTime(DateTime.Now),

                // Seguridad
                ClaveHash = passwordHash,

                // Rol (Definido por el Admin al crear)
                IdRol = dto.IdRol
            };

            // 4. Guardar en Base de Datos
            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

            return nuevoUsuario.IdUsuario;
        }
    }
}
