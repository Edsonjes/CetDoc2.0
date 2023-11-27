using Microsoft.AspNetCore.Builder;
using Infra.Repository;
using Dominio.Model;
using Dominio.Interfaces;
using Infra.Repository;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CetDocApi
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
            IServiceCollection ServicePessoaColection = builder.Services.AddTransient<IPessoaRepository, PessoaRepository>();
            builder.Services.AddAutoMapper(typeof(Mapper));
            builder.Services.AddCors();
            var key = Encoding.ASCII.GetBytes(_Configuration.GetSection("CriptoRash:Key").Value);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}