using Infra.Repository;
using Dominio.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


namespace CetDocApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			IConfiguration _Configuration = builder.Configuration;

			// Adiciona serviços ao contêiner.
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Id = "Bearer",
								Type = ReferenceType.SecurityScheme
							}
						},
						new string[]{}
					}
				});
			});
			builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
			builder.Services.AddScoped<IAuthentication, AuthenticationRepository>();
			builder.Services.AddAutoMapper(typeof(Mapper));
			builder.Services.AddCors();
			builder.Services.AddMvc();

			var key = Encoding.ASCII.GetBytes(_Configuration.GetSection("CriptoRash:Key").Value);

			builder.Services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				};
			});

			var app = builder.Build();

			// Configure o pipeline de solicitação HTTP.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			

			app.UseAuthentication();
			app.UseAuthorization();
			app.UseCors(app => app.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

			app.MapControllers();

			app.Run();
		}
	}
}
