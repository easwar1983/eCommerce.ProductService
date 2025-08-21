#region "Namespace"

#region "Namespace .Net"
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
#endregion

#region "Namespace Project"

#region "Dependency Injection"
using ProductService.Bal;
using ProductService.Dal;
#endregion

#region "Middleware"
using ProductService.Api.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Bal.Mappers;
using ProductService.Api.ApiEndPoints;
//using ProductService.Core.Mapper;
#endregion

#endregion

#endregion

#region "Setup Stage"

var builder = WebApplication.CreateBuilder(args);

#region "Services"

#region "Register"
//Add Controllers
////builder.Services.AddControllers()
////    .AddJsonOptions(
////        options =>
////        {
////            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
////        }
////    );
builder.Services.AddControllers();
// Converts enums to strings in JSON responses
builder.Services.ConfigureHttpJsonOptions
(
    options => 
    {
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()); 
    }
);
//Add AutoMapper
//V13.0.1
//builder.Services.AddAutoMapper(typeof(EntityDtoMapping).Assembly);
//V15.0.1
////builder.Services.AddAutoMapper(cfg =>
////{
////    cfg.AddMaps(typeof(ProductMapping).Assembly);
////});

//Fluent Validations
builder.Services.AddFluentValidationAutoValidation();

//Add API explorer services
builder.Services.AddEndpointsApiExplorer();
//Add swagger generation services to create swagger specification
builder.Services.AddSwaggerGen();

//Add Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins
            (
                "http://localhost:5000", // allowed origin ASP.NET MVC
                "http://localhost:4200", // allowed origin Angular
                "http://localhost:3000" // allowed origin React.js/Node.js/Next.js
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

#endregion

#region"Dependency Injection"
//Add DI
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddBusinessAccessLayer();
#endregion

#endregion

#region "App Built"
var app = builder.Build();

#region "Configure Middleware"
//Middle Ware
app.UseExceptionHandlingMiddleware();

//Routing
app.UseRouting();

//Swagger
app.UseSwagger(); //Adds endpoint that can serve the swagger .json
app.UseSwaggerUI(); //Adds swagger UI(interactive page to explore and test API endpoints)

//Corss Domain
app.UseCors();

//Auth
app.UseAuthentication();
app.UseAuthorization();

#endregion

#region "Route HTTP Request"
app.MapControllers();
#endregion

#region "Route Minimal API Request"
app.MapProductApiEndpoints();
#endregion

#region "Redirect root URL"
///SwaggerUI
app.MapGet("/", () => Results.Redirect("/swagger"));
#endregion

#region "Start Web Server"
app.Run();
#endregion

#endregion

#endregion

