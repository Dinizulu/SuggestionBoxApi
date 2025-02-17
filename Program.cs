using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SuggestionBoxApi.Data;
using SuggestionBoxApi.Interfaces;
using SuggestionBoxApi.Models.JWT;
using SuggestionBoxApi.Repositories;
using SuggestionBoxApi.Services;

namespace SuggestionBoxApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Getting connection string
            builder.Services.AddDbContext<SuggboxContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("suggbox")));

            //Additing Jwt authentication pipeline
            var jwtsettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            builder.Services.AddSingleton(jwtsettings);
            builder.Services.AddScoped<AuthService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtsettings.Issuer,
                    ValidAudience = jwtsettings.Audience,
                    IssuerSigningKey = jwtsettings.GetSymmetricSecurityKey()
                };
            }
            );

            //Registering all the repositories
            builder.Services.AddScoped(typeof(ISuggestionBoxRepository<>), typeof(SuggestionBoxRepository<>));
            builder.Services.AddScoped(typeof(SuggestionBoxRepository<>));

            //adding automapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
