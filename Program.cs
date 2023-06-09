using BirdPlatFormEcommerce.Entities;
using BirdPlatFormEcommerce.Product;
using Microsoft.EntityFrameworkCore;

namespace BirdPlatFormEcommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<SwpContext>(op =>
            {
                op.UseSqlServer(builder.Configuration.GetConnectionString("BirdForm"));
            });

            builder.Services.AddCors(option => option.AddPolicy("BirdPlatform", build =>
            {
                build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IHomeViewProductService, HomeViewProductService>();
         
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