# nov-ene-pf-seleccion-backend

# Sistema de Gestión Académico - Backend

Este repositorio contiene el backend para el Sistema de Gestión Académico. Está construido utilizando **.NET 8** y sigue una arquitectura de **Monolito Modular** para garantizar la escalabilidad, el mantenimiento y una clara separación de responsabilidades.

## 🛠️ Stack Tecnológico

  * **Framework:** .NET 8 (ASP.NET Core Web API)
  * **Lenguaje:** C\# 
  * **ORM:** Entity Framework Core 8
  * **Base de Datos:** MySQL (usando Pomelo.EntityFrameworkCore.MySql)
  * **Autenticación:** JWT (JSON Web Tokens)

-----

## 🏛️ Arquitectura: Monolito Modular

Este proyecto NO es una arquitectura de microservicios tradicional, ni un monolito desordenado. Es un **Monolito Modular**.

### Conceptos Clave

1.  **Una Unidad de Despliegue:** Al final, se compila y ejecuta una sola aplicación API (`SistemaAcademico.ApiGateway`). No hay complejidad de red entre servicios.
2.  **Separación Lógica Estricta:** El código está organizado rigurosamente en **Módulos** funcionales (ej. `Payment`, `Authentication`, `Catalog`).
3.  **Aislamiento de Módulos:** Un módulo **NUNCA** accede directamente a las clases internas (especialmente repositorios o el DbContext) de otro módulo. La comunicación entre módulos ocurre exclusivamente a través de **Interfaces Públicas** definidas en la capa `.Core` del módulo destino.

Esta estructura combina la simplicidad operativa de un monolito con la organización y el desacoplamiento de los microservicios.

-----

## 📂 Estructura de la Solución

La solución de Visual Studio está organizada de la siguiente manera. Respetar esta estructura es vital para la arquitectura.

```
📦 src
 ┣ 📂 SistemaAcademico.ApiGateway  (El Host Principal)
 ┃ ┗ Punto de entrada HTTP. Contiene Controllers, Middlewares y Configuración.
 ┃
 ┣ 📂 SharedKernel                 (El Núcleo Compartido)
 ┃ ┣ 📂 *.Core           -> Entidades base, interfaces genéricas y excepciones globales.
 ┃ ┗ 📂 *.Infrastructure -> CONTIENE EL ApplicationDbContext PRINCIPAL.
 ┃
 ┗ 📂 Modules                      (Los Dominios de Negocio)
   ┣ 📂 [NombreModulo]             (ej. Payment, Authentication)
   ┃ ┣ 📂 *.Core
   ┃ ┃ ┣ 📂 Entities     -> Clases de dominio que mapean a tablas.
   ┃ ┃ ┣ 📂 Interfaces   -> Contratos públicos (IService) y privados (IRepository).
   ┃ ┃ ┣ 📂 Services     -> Implementación de la lógica de negocio.
   ┃ ┃ ┗ 📂 DTOs         -> Objetos de transferencia de datos.
   ┃ ┃
   ┃ ┗ 📂 *.Infrastructure
   ┃   ┣ 📂 Persistence/Configurations -> Mapeos de EF Core (IEntityTypeConfiguration).
   ┃   ┗ 📂 Persistence/Repositories   -> Implementación de repositorios usando el DbContext general.
```

-----

## 📏 Reglas Fundamentales de Desarrollo

Para mantener la integridad de la arquitectura, todo el equipo debe seguir estas reglas:

### Regla \#1: Flujo de Dependencias (Inversión de Control)

La dependencia siempre apunta hacia el centro (la lógica de negocio).

  * ✅ `Infrastructure` DEPENDE DE `Core`.
  * ❌ `Core` NUNCA depende de `Infrastructure`.

### Regla \#2: Persistencia de Datos Centralizada

Aunque cada módulo define sus propias entidades y configuraciones, **NO existen DbContexts por módulo**.

  * Existe un único **`ApplicationDbContext`** (en `SharedKernel.Infrastructure`).
  * Este contexto escanea y aplica automáticamente las configuraciones (`IEntityTypeConfiguration`) definidas en la capa de infraestructura de cada módulo.

### Regla \#3: Comunicación Entre Módulos

Si el módulo A necesita datos o acciones del módulo B:

  * ✅ INYECTA la interfaz de servicio pública del otro módulo (ej. `IPaymentService`).
  * ❌ NUNCA inyectes el repositorio del otro módulo (`IPaymentRepository`) ni uses sus entidades para consultas directas.

-----

## 🚀 Flujo de Trabajo: Nueva Funcionalidad

Pasos recomendados para implementar una nueva característica (ej. en el módulo `Payment`):

1.  **Definir el Dominio (en `.Core`):** Crea la Entidad, los DTOs necesarios y define la interfaz del servicio (`IPaymentService`).
2.  **Configurar Persistencia (en `.Infrastructure`):** Crea la clase de configuración de EF Core para mapear la entidad a MySQL.
3.  **Implementar Lógica (en `.Core`):** Crea la clase del servicio (`PaymentService`) que implementa la interfaz y contiene la lógica de negocio.
4.  **Exponer API (en `ApiGateway`):** Crea el Controlador, inyecta el servicio del módulo y crea el endpoint HTTP.

-----

## 🗄️ Manejo de Base de Datos y Migraciones

Las migraciones se gestionan de forma centralizada ya que solo hay un `DbContext`.

**IMPORTANTE:** Los comandos siempre deben ejecutarse desde la carpeta raíz `src/`, apuntando al proyecto de inicio (`ApiGateway`) pero indicando que el contexto está en `SharedKernel.Infrastructure`.

Abre tu terminal en la carpeta `src/` y utiliza los siguientes comandos:

### Crear una nueva migración

Utiliza esto después de modificar cualquier entidad en cualquier módulo.

```bash
dotnet ef migrations add [NombreDescriptivoMigracion] -s SistemaAcademico.ApiGateway -p SharedKernel/SistemaAcademicoBackend.SharedKernel.Infrastructure
```

### Actualizar la base de datos

Aplica las migraciones pendientes a tu base de datos MySQL configurada.

```bash
dotnet ef database update -s SistemaAcademico.ApiGateway -p SharedKernel/SistemaAcademicoBackend.SharedKernel.Infrastructure
```

-----

## ⚙️ Configuración Local (Getting Started)

1.  **Prerrequisitos:**
      * .NET 8 SDK instalado.
      * Servidor MySQL en ejecución (local o Docker).
2.  **Configuración:**
      * Ve a `src/SistemaAcademico.ApiGateway`.
      * Crea un archivo `appsettings.Development.json` (si no existe).
      * Configura tu cadena de conexión a MySQL y las settings de JWT:

<!-- end list -->

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaAcademicoDB;User=root;Password=tu_password;"
  },
  "JwtSettings": {
    "SecretKey": "TU_CLAVE_SECRETA_MUY_LARGA_PARA_DESARROLLO_LOCAL",
    "Issuer": "SistemaAcademicoApi",
    "Audience": "SistemaAcademicoClient",
    "DurationInMinutes": 60
  }
}
```

3.  **Base de Datos:** Ejecuta el comando de actualización de base de datos (ver sección anterior) para crear el esquema.
4.  **Ejecutar:** Abre la solución en Visual Studio y ejecuta el proyecto `SistemaAcademico.ApiGateway`.


# 📘 Guía de Estándares de Desarrollo

Esta guía establece los lineamientos de codificación para el proyecto **Sistema de Gestión Académico**. El objetivo es mantener una base de código limpia, consistente y mantenible en nuestro Monolito Modular con .NET 8.

## 1\. Principios Fundamentales

  * **KISS (Keep It Simple, Stupid):** La simplicidad es la meta. Evita la sobre-ingeniería.
  * **Clean Code:** Escribe código para humanos. Usa nombres que revelen la intención.
  * **DRY (Don't Repeat Yourself):** Extrae la lógica duplicada a métodos o servicios comunes.
  * **Regla del Boy Scout:** Deja el archivo siempre un poco más limpio de lo que lo encontraste.

-----

## 2\. Convenciones de Nombres (Naming Conventions)

| Elemento | Convención | Ejemplo |
| :--- | :--- | :--- |
| **Clase / Record** | `PascalCase` | `EstudianteInscrito`, `PaymentService` |
| **Interfaz** | `IPascalCase` | `IPaymentService`, `IUserRepository` |
| **Método** | `PascalCase` | `RegistrarPagoAsync`, `CalcularTotal` |
| **Propiedad Pública** | `PascalCase` | `FechaNacimiento`, `MontoTotal` |
| **Variable Local** | `camelCase` | `totalFactura`, `usuarioEncontrado` |
| **Parámetro** | `camelCase` | `idEstudiante`, `montoPagado` |
| **Campo Privado** | `_camelCase` | `_dbContext`, `_logger` |
| **Constante** | `SCREAMING_SNAKE_CASE` | `MAX_INTENTOS_LOGIN`, `IMPUESTO_ITBIS` |

-----

## 3\. Estructura y Formato

### 3.1. Llaves `{}`

Usamos el estilo **Allman** (llave en nueva línea). Las llaves son obligatorias siempre, incluso en bloques de una línea.

```csharp
// ✅ Correcto
if (esValido)
{
    return true;
}

// ❌ Incorrecto
if (esValido) return true;
```

### 3.2. Condicionales y Cláusulas de Guarda

Evita el anidamiento profundo (`if` dentro de `if` dentro de `if`). Usa **Cláusulas de Guarda** para validar y salir rápido.

**✅ Correcto (Clean Code):**

```csharp
public async Task Inscribir(Estudiante estudiante)
{
    if (estudiante == null) throw new ArgumentNullException(nameof(estudiante));
    if (!estudiante.Activo) return;
    if (estudiante.TieneDeuda) throw new Exception("Tiene deuda.");

    // Lógica principal (Happy Path) al final y sin indentación excesiva
    await _repo.GuardarAsync(estudiante);
}
```

**❌ Incorrecto (Arrow Code):**

```csharp
public async Task Inscribir(Estudiante estudiante)
{
    if (estudiante != null)
    {
        if (estudiante.Activo)
        {
             // Lógica enterrada
             await _repo.GuardarAsync(estudiante);
        }
    }
}
```

-----

## 4\. Documentación de Código

Utilizamos **Comentarios XML (`///`)** obligatorios en interfaces y servicios públicos. Esto debe describir el método, qué entra y qué sale.

### Formato Requerido:

```csharp
/// <summary>
/// Descripción breve y clara de QUÉ hace el método.
/// </summary>
/// <param name="nombreParametro">Descripción de qué es este parámetro.</param>
/// <returns>Descripción de qué devuelve el método al finalizar.</returns>
/// <exception cref="TipoExcepcion">Descripción de errores controlados que puede lanzar.</exception>
```

### Ejemplo Real:

```csharp
public class PaymentService : IPaymentService
{
    /// <summary>
    /// Procesa el registro de un pago, actualiza la deuda y genera la factura.
    /// </summary>
    /// <param name="usuarioId">El GUID único del estudiante.</param>
    /// <param name="monto">El dinero recibido en la transacción.</param>
    /// <returns>
    /// Retorna el ID de la nueva factura generada.
    /// </returns>
    /// <exception cref="ArgumentException">Si el monto es negativo.</exception>
    public async Task<Guid> RegistrarPagoAsync(Guid usuarioId, decimal monto)
    {
        // ... implementación
    }
}
```

-----

## 5\. Buenas Prácticas .NET

1.  **Inyección de Dependencias:** Siempre por constructor. Nunca usar `new Service()`.
2.  **Async/Await:** Todo I/O (Base de datos, API calls) debe ser asíncrono. Evita `.Result` o `.Wait()`.
3.  **Manejo de Excepciones:** No uses `try/catch` vacíos. Deja que las excepciones suban al Middleware global a menos que puedas corregir el error en el momento.
4.  **LINQ:** Prefiere LINQ (`Where`, `Select`) sobre bucles `foreach` manuales para transformaciones de listas.
