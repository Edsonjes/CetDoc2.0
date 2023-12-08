using Dominio.Interfaces;
using Infra.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddScoped<IAuthentication, AuthenticationRepository>();

			// Build the configuration
			var configuration = builder.Configuration;

			// Retrieve the key from the configuration
			var key = Encoding.ASCII.GetBytes(configuration.GetSection("CriptoRash:Key").Value);

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

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Login}/{id?}");

				endpoints.MapControllerRoute(
				name: "Index",
				pattern: "Home/Index",
				defaults: new { controller = "Home", action = "Index" }
																									);

				endpoints.MapControllerRoute(
					name: "ListarPessoas",
					pattern: "/Pessoas",
					defaults: new { controller = "Pessoas", action = "PessoasIndex" }
					);

				endpoints.MapControllerRoute(
					name: "CadastrarPessoa",
					pattern: "/Pessoas/Cadastrar",
					defaults: new { controller = "Pessoas", action = "CadastrarPessoa" }
					);
			});

			app.Run();
		}
	}
}