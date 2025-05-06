
using Mechanic;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Converts enum data(for eg.: 0,1,2,3,4) to string(Karosszéria for e.g)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapControllers();

app.Run();


//-----------------------------------------------------------------------------------------------------------------------
//                                          ALWAYS REMEMBER!!!
//                                      -Attribute validation(Whitespace and empty strings

//-----------------------------------------------------------------------------------------------------------------------
