using System.Globalization;
using System.Reflection;
using Avonale.Products.API.Extensions.Configurations;
using Avonale.Products.Infra.CrossCutting.Ioc;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-US");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapperConfiguration();

builder.Services.ResolveDependencies();
builder.Services.AddMessageBusConfiguration(builder.Configuration);

var app = builder.Build();

app.UseApiConfiguration();
app.UseSwaggerConfiguration(app.Environment);

app.Run();