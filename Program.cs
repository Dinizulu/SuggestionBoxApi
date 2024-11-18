using Microsoft.EntityFrameworkCore;
using SuggestionBoxApi.Data;
using SuggestionBoxApi.Interfaces;
using SuggestionBoxApi.Repositories;

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
