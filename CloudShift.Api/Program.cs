using CloudShift.Api.Extensions;
using CloudShift.Application.Repositories;
using CloudShift.Application.Services;
using CloudShift.Domain;
using CloudShift.Kernel.Application.Http;
using CloudShift.Kernel.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Serilog;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        builder.Services.AddHttpClient("", client =>
        {
            client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });

        builder.Services
            .AddScoped<IOrderItemsService, OrderItemsService>()
            .AddScoped<IGuidEntitiesService, GuidEntitiesService>()
            .AddScoped<ITodosService, TodosService>()
            .AddScoped<IRestClient, RestClient>();

        builder.Services.AddDbContext<CloudShiftDbContext>(options => options.UseInMemoryDatabase("CloudShiftDb"));

        builder.Services.AddAuthorization();

        builder.Services.AddIdentityApiEndpoints<IdentityUser>(config =>
        {
            config.Password.RequiredLength = 3;
            config.Password.RequireNonAlphanumeric = false;
            config.Password.RequiredUniqueChars = 0;
            config.Password.RequireUppercase = false;
            config.Password.RequireLowercase = false;
        })
            .AddEntityFrameworkStores<CloudShiftDbContext>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "CloudShelf API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        builder.Host.UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File(Path.Combine("logs", "log.txt"), rollingInterval: RollingInterval.Day));


        var app = builder.Build();

        app.SeedApplicationData();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.MapIdentityApi<IdentityUser>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
