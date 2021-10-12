using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using API.Helpers;
using API.SignalR;

namespace API.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddSingleton<PresenceTracker>();
      services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
      services.AddDbContext<DataContext>(options =>
        {
          options.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
      services.AddScoped<ITokenService, TokenService>();
      services.AddScoped<LogUserActivity>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();
      services.AddScoped<IPhotoService, PhotoService>();

      services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
      return services;
    }
    public static IServiceCollection AddSwaggerGenServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddSwaggerGen(c =>
         {
           // configure SwaggerDoc and others
           c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
           // add JWT Authentication
           var securityScheme = new OpenApiSecurityScheme
           {
             Name = "JWT Authentication",
             Description = "Enter JWT Bearer token **_only_**",
             In = ParameterLocation.Header,
             Type = SecuritySchemeType.Http,
             Scheme = "bearer", // must be lower case
             BearerFormat = "JWT",
             Reference = new OpenApiReference
             {
               Id = JwtBearerDefaults.AuthenticationScheme,
               Type = ReferenceType.SecurityScheme
             }
           };
           c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
           c.AddSecurityRequirement(new OpenApiSecurityRequirement
         {
            {securityScheme, new string[] { }}
         });
         });
      return services;
    }
  }
}