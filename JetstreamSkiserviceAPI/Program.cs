using JetstreamSkiserviceAPI.Models;
using JetstreamSkiserviceAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace JetstreamSkiserviceAPI
{
    /// <summary>
    /// Main Program from the backend
    /// </summary>
    /// <remarks>
    /// If you use a other IP-Adress than the CORS registered, change it.
    /// On Errors please look at the root-folder on the webapi.log
    /// </remarks>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Serilogger
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            // Add DbContext class
            builder.Services.AddDbContext<RegistrationsContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("JetstreamSkiserviceDB")));

            // Add Scopes from the implemented interfaces
            builder.Services.AddScoped<IRegistrationService, RegistrationService>();
            builder.Services.AddScoped<IStatusService, StatusService>();
            builder.Services.AddScoped<IPriorityService, PriorityService>();
            builder.Services.AddScoped<ITokenService, TokenService>();

            // Configure JWT Token
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                        ValidAudience = builder.Configuration["JWT:Key"],
                        ValidIssuer = builder.Configuration["JWT:Key"],
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5502", "http://127.0.0.1:5502", "https://localhost:7119", "https://127.0.0.1:7119") // Change this IP's
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

                builder.Services.AddEndpointsApiExplorer();

                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jetstream Web API", Version = "v1" });
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme."
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                    }
                },
                new string[] {}
                }
            });
        });

            builder.Services.AddAuthorization();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCors(); // Activate CORS

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
