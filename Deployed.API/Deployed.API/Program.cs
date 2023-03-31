using Deployed.Data.Services.Database;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Deployed.Application.Commands;
using Deployed.API.Infrastracture.Mapping;
using Deployed.Application.Infrastracture.Interfaces;
using Deployed.Application.Infrastracture.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddMediatR(typeof(ListCustomerQuery).Assembly);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerDependentService, CustomerDependentService>();
builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddMaps(Assembly.GetExecutingAssembly());
    mc.AddMaps(Assembly.GetAssembly(typeof(CustomerMappingProfile)));
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .SetIsOriginAllowed((host) => true)
    .AllowAnyHeader());

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
