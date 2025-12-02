using CrecerIncidencia.Application;
using CrecerIncidencia.Application.Services;
using CrecerIncidencia.Application.Validations;
using CrecerIncidencia.Domain.Interfaces;
using CrecerIncidencia.Infrastructure.Database;
using CrecerIncidencia.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Configuration
var configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// DI: DapperContext
var conn = configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new DapperContext(conn));

// DI: repos & services
builder.Services.AddScoped<IIncidenciaRepository, IncidenciaRepository>();
builder.Services.AddScoped<IIncidenciaService, IncidenciaService>();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<IncidenciaRequestValidator>();
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
