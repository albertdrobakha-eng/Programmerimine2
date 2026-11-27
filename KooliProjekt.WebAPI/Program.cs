using System;
using System.IO;
using System.Text;
using KooliProjekt.Application.Authentication.Jwt;
using KooliProjekt.Application.Behaviors;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure;
using KooliProjekt.Application.Infrastructure.Files;
using KooliProjekt.WebAPI.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
namespace KooliProjekt.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddControllers();
            builder.Services.AddHttpContextAccessor();

            var configuredKey = builder.Configuration["Jwt:Key"];
            if (string.IsNullOrWhiteSpace(configuredKey) && builder.Environment.IsDevelopment())
                configuredKey = Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(48));
            if (string.IsNullOrWhiteSpace(configuredKey)) throw new InvalidOperationException("Configure Jwt:Key using a secure environment setting.");
            builder.Configuration["Jwt:Key"] = configuredKey;
            var jwtSettings = new JwtSettings(builder.Configuration);
            builder.Services.AddSingleton(jwtSettings);
            builder.Services.AddSingleton<JwtTokenBuilder>();
            builder.Services.AddScoped<IUserProvider, WebUserProvider>();
            builder.Services.AddAuthentication("Bearer").AddScheme<AuthenticationSchemeOptions, JwtAuthenticationHandler>("Bearer", _ => { });
            builder.Services.AddAuthorization();

            var filesRoot = builder.Configuration["FilesRoot"];
            if (string.IsNullOrWhiteSpace(filesRoot)) filesRoot = Path.Combine(builder.Environment.ContentRootPath, "Files");
            else if (!Path.IsPathRooted(filesRoot)) filesRoot = Path.Combine(builder.Environment.ContentRootPath, filesRoot);
            builder.Services.AddSingleton<IFileClient>(new LocalFileClient(filesRoot));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var applicationAssembly = typeof(ErrorHandlingBehavior<,>).Assembly;
            builder.Services.AddValidatorsFromAssembly(applicationAssembly);
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(applicationAssembly);
                config.AddOpenBehavior(typeof(ErrorHandlingBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(TransactionalBehavior<,>));
            });
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
#if DEBUG
                SeedData.Generate(db);
#endif
            }
            if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
