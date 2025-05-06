
using Mechanic;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

builder.Services.AddSerilog(
    options => options
    .MinimumLevel.Information()
    .WriteTo.Console());



builder.Services.AddSingleton<IClientService, ClientService>();

builder.Services.AddSingleton<IJobService, JobService>();

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

//TODO: Job(jobId, id, licensePlate, ManufacturingYear, Category, Description, Severity,Status -> VALIDATION!!
//TODO: IJobService + Implementation with List
//TODO: JobController: CRUD endpoints