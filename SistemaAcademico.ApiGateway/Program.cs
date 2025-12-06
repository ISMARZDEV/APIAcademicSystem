using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Persistence.Models;
using SistemaAcademico.AcademicProgress.Core.Interfaces;
using SistemaAcademico.AcademicProgress.Core.Services;
using SistemaAcademico.AcademicProgress.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


//Sistema Academico DB Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


var serverVersion = ServerVersion.AutoDetect(connectionString);


builder.Services.AddDbContext<SistemaAcademicoContext>(options =>
{
    options.UseMySql(connectionString, serverVersion);
});

// Add services to the container.

// Register the services for the AcademicProgress module
builder.Services.AddScoped<IAcademicProgressService, AcademicProgressService>();
builder.Services.AddScoped<IAcademicProgressRepository, AcademicProgressRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
