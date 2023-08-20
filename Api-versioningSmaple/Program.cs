using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Api_versioningSmaple.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.ResolveConflictingActions(x => x.Last()));

builder.Services.AddApiVersioning(setup => {
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.AssumeDefaultVersionWhenUnspecified = false;
    setup.ReportApiVersions = true;
    setup.ApiVersionReader = new HeaderApiVersionReader("X-Version");

    var conv = setup.Conventions.Controller<WeatherForecastController>();
    conv.HasApiVersion(new ApiVersion(1, 0));
    conv.HasApiVersion(new ApiVersion(1, 1));
    conv.HasDeprecatedApiVersion(new ApiVersion(1, 0));
});
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
