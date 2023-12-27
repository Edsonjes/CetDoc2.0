using Dominio.Interfaces;
using Infra.Repository;
using Microsoft.AspNetCore.Authentication;
using AutoMapper;
using Servicos;

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
			builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
            builder.Services.AddScoped<PessoaServices>();
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy", builder => builder
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());
			});

			// Build the configuration
			var configuration = builder.Configuration;

			// Retrieve the key from the configuration
			////var key = Encoding.ASCII.GetBytes(configuration.GetSection("CriptoRash:Key").Value);
			var serviceURl = configuration.GetSection("ServicesUrls:IdentityServer").Value;

			builder.Services.AddAuthentication(x =>
			{
				x.DefaultScheme = "Cookies";
				x.DefaultChallengeScheme = "oidc";

			}).AddCookie("Cookies", x => x.ExpireTimeSpan = System.TimeSpan.FromMinutes(60))
			.AddOpenIdConnect("oidc", options =>
			{
				options.Authority = serviceURl;
                options.GetClaimsFromUserInfoEndpoint = true;
				options.ClientId = "CetDocsApp";
				options.ClientSecret = "CriptoRash";
				options.ResponseType = "code";
				options.ClaimActions.MapJsonKey("role", "role", "role");
                options.ClaimActions.MapJsonKey("sub", "sub", "sub");
				options.TokenValidationParameters.NameClaimType = "name";
				options.TokenValidationParameters.RoleClaimType = "role";
				options.UsePkce = true;
				options.Scope.Add("CetDocsApp");
				options.SaveTokens = true;

            });

			builder.Services.AddAutoMapper(typeof(Mapper));
			

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
			}
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
			app.UseCors("CorsPolicy");
		

            app.UseEndpoints(endpoints =>
            {
				
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Login}/{id?}");

          
                endpoints.MapControllerRoute(
                    name: "ListarPessoas",
                    pattern: "/Pessoas",
                    defaults: new { controller = "Pessoas", action = "PessoasIndex" });

                endpoints.MapControllerRoute(
                    name: "CadastrarPessoa",
                    pattern: "/Pessoas/Cadastrar",
                    defaults: new { controller = "Pessoas", action = "CadastrarPessoa" });
            });
            
            app.Use(async (context, next) =>
            {
				if (!context.User.Identity.IsAuthenticated)
				{
					context.Response.Redirect("https://localhost:4435/Account/Login");
				}
				else
				{
                    await next();
                }
                //context.Response.Redirect("/Home/Index");
                
            });

            app.Run();
		}
	}
}