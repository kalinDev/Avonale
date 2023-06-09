﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Avonale.Products.API.Extensions.Configurations;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.DocumentFilter<IgnoreValidationResultFilter>();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Avonale Product API",
                Version = "v1",
                Contact = new OpenApiContact{ Name = "Joao Paulo", Email = "vasconcelosjoao438@gmail.com"}
            });
        });
    }
    
    private class IgnoreValidationResultFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var schema in swaggerDoc.Components.Schemas)
            {
                schema.Value?.Properties?.Remove("validationResult");
            }
        }
    }
    
    public static void UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Avonale Product API");
        });
        
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}