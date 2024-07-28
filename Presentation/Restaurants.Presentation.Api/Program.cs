using Restaurant.Application.Ioc;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Database;
using Restaurants.Infrastructure.Ioc;
using Restaurants.Presentation.Api;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();
    builder.AddPresentation();


    var app = builder.Build();
    var scope = app.Services.CreateAsyncScope();
    var seeder = scope.ServiceProvider.GetService<IRestaurantsSeeder>();
    if (seeder != null)
    {
        await seeder.SeedIdentityAsync();
        await seeder.SeedRestaurantAsync();
    }

    app.UseSerilogRequestLogging();

    app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();
    app.UseCors("AllowSpecificOrigin");

    app.MapControllers();
    app.MapGroup("api/Identities").MapIdentityApi<User>();

    await app.RunAsync();
}
catch (Exception exception)
{
    Log.Fatal(exception, "Application fail starting");
}
finally
{
    Log.CloseAndFlush();
}

/// <summary>
///     Class partiel du program pour l'initier dans les tests
/// </summary>
public abstract partial class Program;