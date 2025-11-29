using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Authentication.Core.DTOs
{
    public class CreateUserDto
    {
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public string Apellido { get; set; } = string.Empty;

        public string Nacionalidad { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string CorreoPersonal { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string CorreoInstitucional { get; set; } = string.Empty;

        [Required]
        // [AUTH-10] Reglas de fortaleza: Mínimo 8 chars, 1 mayúscula, 1 número.
        // Regex explicación:
        // (?=.*[A-Z]) = Al menos una mayúscula
        // (?=.*\d)    = Al menos un número
        // .{8,}       = Mínimo 8 caracteres
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).{8,}$",
            ErrorMessage = "La contraseña temporal debe ser segura (min 8 chars, 1 mayúscula, 1 número).")]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int IdRol { get; set; }
    }
}
