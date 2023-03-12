using System.Reflection;
using Avonale.Payment.API.Extensions.Configurations;
using Avonale.Payment.Infra.CrossCutting.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddMessageBusConfiguration(builder.Configuration);
builder.Services.ResolveDependencies();

var app = builder.Build();

app.UseApiConfiguration();

app.Run();