
using Mechanic;
using Mechanic.Api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//Converts enum data(for eg.: 0,1,2,3,4) to string(Karossz�ria for e.g)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddSwaggerGen();


builder.Services.AddSerilog(
    options => options
    .MinimumLevel.Information()
    .WriteTo.Console());



builder.Services.AddSingleton<IClientService, ClientService>();
builder.Services.AddSingleton<IJobService, JobService>();


builder.Services.AddDbContext<MechanicDbContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
        options.UseLazyLoadingProxies();
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();

