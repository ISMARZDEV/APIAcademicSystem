using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Persistence.Data;
using SistemaAcademico.Persistence.Models;
using SistemaAcademico.AcademicCatalog.Core.Interfaces;
using SistemaAcademico.AcademicCatalog.Infrastructure.Persistence.Repositories;
using SistemaAcademico.AcademicProgress.Core.Interfaces;
using SistemaAcademico.AcademicProgress.Core.Services;
using SistemaAcademico.AcademicProgress.Infrastructure.Persistence.Repositories;
using SistemaAcademico.Authentication.Infrastructure;
using SistemaAcademico.SelecctionAndPreselecction.Core.Interfaces;
using SistemaAcademico.SelecctionAndPreselecction.Core.Services;
using SistemaAcademico.SelecctionAndPreselecction.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


//Sistema Academico DB Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


var serverVersion = ServerVersion.AutoDetect(connectionString);


// builder.Services.AddDbContext<SistemaAcademicoContext>(options =>
// {
//     options.UseMySql(connectionString, serverVersion);
// });

// Data Seeding
builder.Services.AddDbContext<SistemaAcademicoContext>(options =>
  options.UseMySql(connectionString, serverVersion)
  .UseSeeding((context, _) =>
  {
    var appContext = (SistemaAcademicoContext)context;
    DataSeeder.SeedData(appContext);
  })
);

// Repositories
builder.Services.AddScoped<IAcademicAreaRepository, AcademicAreaRepository>();
builder.Services.AddScoped<ISubjectsRepository, SubjectRepository>();
builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<IPeriodoConfigRepository, PeriodoConfigRepository>();
builder.Services.AddScoped<IPreseleccionRepository, PreseleccionRepository>();
builder.Services.AddScoped<ISeleccionRepository, SeleccionRepository>();

// AutoMappers
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

// Register the services for the AcademicProgress module
builder.Services.AddScoped<IAcademicProgressService, AcademicProgressService>();
builder.Services.AddScoped<IAcademicProgressRepository, AcademicProgressRepository>();
builder.Services.AddScoped<IPeriodoConfigService, PeriodoConfigService>();
builder.Services.AddScoped<IPreseleccionService, PreseleccionService>();
builder.Services.AddScoped<ISeleccionService, SeleccionService>();

builder.Services.AddScoped<ICareerRepository, CareerRepository>();
builder.Services.AddScoped<IAcademicProgramRepository, AcademicProgramRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthenticationModule();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
