using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Application.Authorization;
using Restaurants.Infrastructure.Application.Authorization.Requirements;
using Restaurants.Infrastructure.Application.Constants;
using Restaurants.Infrastructure.Database;
using Serilog;

namespace Restaurants.Presentation.Api;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:5168/")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });
        builder.Services.AddControllers();
        builder.Configuration.AddJsonFile("serilog.json", true, true)
            .AddJsonFile($"serilog.{builder.Environment.EnvironmentName}.json", true, true);

        builder.Services.AddScoped<ExceptionHandlerMiddleware>();
        builder.Services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();
        builder.Services.AddIdentityApiEndpoints<User>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.SignIn.RequireConfirmedEmail = false;
            })
            .AddRoles<Role>()
            .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>()
            .AddEntityFrameworkStores<IdentityDbContext>();
        builder.Services.AddAuthorizationBuilder()
            // Cas où il faut un prenom est requis et le prenom doit correspondre à Kake ou Jean
            .AddPolicy(PolicyNames.HasFirstName, b => b.RequireClaim("FirstName", "Kake", "Jean"))
            // Cas où il faut un prenom est requis
            // .AddPolicy("has-firstname", b => b.RequireClaim("FirstName"))
            // Cas où l'utilisateur courant a obligatoire au 20 ans sur les ressources où cette policie est utilisé
            .AddPolicy(PolicyNames.HasAtLeast20YearOld, b => b.AddRequirements(new MinimumAgeRequirement(20)));

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Restaurants API",
                Version = "v1",
                Description = "An API to manage restaurants",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "KAKE Abdoulaye",
                    Email = "abdoulaye.kake.pro@gmail.com",
                    Url = new Uri("https://twitter.com/jwalkner")
                },
                License = new OpenApiLicense
                {
                    Name = "Restaurants API LICX",
                    Url = new Uri("https://example.com/license")
                }
            });

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            swagger.IncludeXmlComments(xmlPath);

            swagger.AddSecurityDefinition("BearerAuth", new OpenApiSecurityScheme
            {
                Scheme = "Bearer",
                Type = SecuritySchemeType.Http
            });

            swagger.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "BearerAuth" }
                        },
                        []
                    }
                });
        });

        builder.Host.UseSerilog((context, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(context.Configuration));
    }
}