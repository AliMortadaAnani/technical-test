using Dashboard.API.Application.DTOs.ResponseDTOsValidators;
using Dashboard.API.Application.Exceptions;
using Dashboard.API.Application.ServiceContracts;
using Dashboard.API.Application.Services;
using Dashboard.API.Domain.RepositoryContracts;
using Dashboard.API.Domain.ResultErrorDomain;
using Dashboard.API.Infrastructure.DatabaseContext;
using Dashboard.API.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

namespace Dashboard.API.StartupExtension
{
    public static class ConfigureServicesExtension
    {
        /// <summary>
        /// Master extension method that calls all segregated configuration groups.
        /// Call this once in Program.cs via: builder.ConfigureAllServices();
        /// </summary>
        public static WebApplicationBuilder ConfigureAllServices(this WebApplicationBuilder builder)
        {
            builder.ConfigureDatabase()
                   .ConfigureControllers()
                   .ConfigureExceptions()
                   .ConfigureSwagger()
                   .ConfigureCors()
                   .ConfigureApplicationServices()
                   .ConfigureValidation()
                   .ConfigureRateLimiting()
                   ;

            return builder;
        }

        public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
        {
            // Get the connection string from configuration (user secrets or appsettings.json)
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException
                    ("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted, RelationalEventId.CommandError }, LogLevel.Information);
            });

            return builder;
        }

        public static WebApplicationBuilder ConfigureControllers(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                // Automatically convert ALL enums to their string names in JSON responses
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddProblemDetails();

            return builder;
        }

        public static WebApplicationBuilder ConfigureExceptions(this WebApplicationBuilder builder)
        {
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            return builder;
        }

        public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(c =>
            {
                // Standard Swagger metadata
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dashboard API", Version = "v1" });

                //Here we should enable setting in csproj

                // 1. Get the name of the generated XML file (usually YourProjectName.xml)
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                // 2. Combine with the base directory to get the full path
                var fullPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

                // 3. Tell Swagger to use it
                c.IncludeXmlComments(fullPath);
            });

            return builder;
        }

        public static WebApplicationBuilder ConfigureCors(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", policyBuilder =>
                {
                    policyBuilder.WithOrigins("http://localhost:5173") // My React Frontend URL
                                 .AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowCredentials();
                });
            });

            return builder;
        }

        public static WebApplicationBuilder ConfigureApplicationServices(this WebApplicationBuilder builder)
        {
            //Scoped services are created once per client request (connection).

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();

            builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();

            return builder;
        }

        public static WebApplicationBuilder ConfigureValidation(this WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<JobApplicationResponseDTOValidator>();
            builder.Services.AddFluentValidationRulesToSwagger();

            return builder;
        }

        public static WebApplicationBuilder ConfigureRateLimiting(this WebApplicationBuilder builder)
        {
            builder.Services.AddRateLimiter(options =>
            {
                // 1. Set the default status code to 429 Too Many Requests
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                // 2. Customize the response to match your ProblemDetails architecture!
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.HttpContext.Response.ContentType = "application/problem+json";

                    var problem = new ProblemDetails
                    {
                        Status = StatusCodes.Status429TooManyRequests,
                        Title = nameof(ProblemDetails429ErrorTypes.RateLimit_Exceeded),
                        Detail = "You have exceeded your rate limit. Please try again later.",
                        Type = "429TooManyRequests",
                        Instance = context.HttpContext.Request.Path
                    };

                    await context.HttpContext.Response.WriteAsJsonAsync(problem, token);
                };

                // Policy : General limit for the API (e.g., 100 requests per minute)
                options.AddPolicy("GeneralApiLimiter", httpContext =>
                {
                    string ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

                    return RateLimitPartition.GetSlidingWindowLimiter(
                            partitionKey: ip,
                            factory: _ => new SlidingWindowRateLimiterOptions
                            {
                                PermitLimit = 20,                // Max 20 requests allowed...
                                Window = TimeSpan.FromMinutes(1), // ...in a total 1-minute window
                                SegmentsPerWindow = 4,            // Split that minute into four 15-second blocks
                                QueueLimit = 0,                   // Reject instantly (no waiting queue)
                                AutoReplenishment = true          // Automatically release expired segments
                            });
                });
            });

            return builder;
        }
    }
}