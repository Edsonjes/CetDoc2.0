using Infra.Repository;
using Dominio.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using Servicos;
using Dominio.Profiles;
using Microsoft.Data.SqlClient;

namespace CetDocApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IMapper Mapper = MapperConfig.RegisterMap().CreateMapper();

            var builder = WebApplication.CreateBuilder(args);
            IConfiguration _Configuration = builder.Configuration;
            
            //addOsMapper 
            builder.Services.AddSingleton(Mapper);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Adiciona serviços ao contêiner.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
            builder.Services.AddScoped<IAuthentication, AuthenticationRepository>();
            builder.Services.AddScoped<ICurriculoProssional, CurriculoProfissionalRepository>();
            builder.Services.AddScoped<PessoaServices>();
            builder.Services.AddScoped<CurriculoProfissionalService>();
            
            builder.Services.AddAutoMapper(typeof(Mapper));
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder => builder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());
			});
			builder.Services.AddMvc();

            var key = Encoding.ASCII.GetBytes(_Configuration.GetSection("CriptoRash:Key").Value);
            var serviceURl = _Configuration.GetSection("ServicesUrls:IdentityServer").Value;

            builder.Services.AddAuthentication("Bearer").AddJwtBearer("Bearer",options =>
            {
                options.Authority = serviceURl;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,

                };
            });
            builder.Services.AddAuthorization(options =>
            {

                options.AddPolicy("ApiScope", policy =>
                {

                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "CetDocsApp");
                });


            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CetDocApi", Version = "v1" });
                c.EnableAnnotations();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Enter 'Bearer' [space] and your token!",
                    Name = "Autorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                             Scheme = "oauth2",
                            Name = "Bearer",
                           In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure o pipeline de solicitação HTTP.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors("CorsPolicy");

            app.Run();
        }
    }
}
