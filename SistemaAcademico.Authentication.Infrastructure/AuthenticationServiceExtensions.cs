using Microsoft.Extensions.DependencyInjection;
using SistemaAcademico.Authentication.Core.Interfaces;
using SistemaAcademico.Authentication.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Authentication.Infrastructure
{
    public static class AuthenticationServiceExtensions
    {
        public static IServiceCollection AddAuthenticationModule(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
