using CarRental.Application.Abstractions;
using CarRental.Application.PipelineBehaviours;
using CarRental.Domain.Repositories;
using CarRental.Infrastructure.Persistence;
using CarRental.Infrastructure.Persistence.Handlers;
using CarRental.Infrastructure.Persistence.Repositories;
using CarRental.Infrastructure.Services.EmailService;
using Microsoft.OpenApi.Models;
using FluentValidation;
using MediatR;

const string ApiTile = "CarRentals";
const string SwaggerApiVersion = "v1";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerOptions =>
{
    swaggerOptions.SwaggerDoc(SwaggerApiVersion,
        new OpenApiInfo
        {
            Title = ApiTile,
            Version = SwaggerApiVersion
        });
    swaggerOptions.DescribeAllParametersInCamelCase();
    swaggerOptions.UseAllOfForInheritance();
});

builder.Services.AddControllers().AddApplicationPart(typeof(CarRental.Presentation.AssemblyReference).Assembly);

builder.Services.AddMediatR(cfg=>
    cfg.RegisterServicesFromAssemblies(CarRental.Application.AssemblyReference.Assembly));

builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviour<,>));
builder.Services.AddValidatorsFromAssembly(CarRental.Application.AssemblyReference.Assembly, includeInternalTypes: true);

builder.Services.AddSingleton<IBookingRepository, BookingRepository>();
builder.Services.AddSingleton<IReportRepository, ReportRepository>();
builder.Services.AddSingleton<INotificationSender, EmailService>();
builder.Services.AddSingleton<DbContext>();

Dapper.SqlMapper.AddTypeHandler(new SQLiteGuidTypeHandler());
Dapper.SqlMapper.RemoveTypeMap(typeof(Guid));
Dapper.SqlMapper.RemoveTypeMap(typeof(Guid?));

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DbContext>();
await context.InitDataBase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelsExpandDepth(-1);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();