
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PseuSM.Extensions;
using System.Text;

namespace PseuSM
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            Adapters.DependencyInjector.Inject(builder.Services, builder.Configuration, builder.Environment.IsDevelopment());
            Core.DependencyInjector.Inject(builder.Services, builder.Configuration, builder.Environment.IsDevelopment());
            Tools.DependencyInjector.Inject(builder.Services, builder.Configuration);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    bool isDevelopment = builder.Environment.IsDevelopment();
                    IConfiguration configuration = builder.Configuration;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = false,

                        ValidAudience = isDevelopment ? configuration["JwtOptions:Audience"]! : configuration["JWT_AUDIENCE"]!,
                        ValidIssuer = isDevelopment ? configuration["JwtOptions:Issuer"]! : configuration["JWT_ISSUER"]!,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(isDevelopment ? configuration["JwtOptions:SecretKey"]! : configuration["JWT_SEWCRET_KEY"]!))
                    };
                });

            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseErrorHandler();

            app.UseMigration();

            app.UseCors(builder =>
            {
                builder.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}