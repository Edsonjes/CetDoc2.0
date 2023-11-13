namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoite =>
            {
                endpoite.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoite.MapControllerRoute(
                    name: "ListarPessoas",
                    pattern: "/Pessoas",
                    defaults: new { controller = "Pessoas", action = "PessoasIndex" }
                    );

                endpoite.MapControllerRoute(
                    name: "CadastrarPessoa",
                    pattern: "/Pessoas/Cadastrar",
                    defaults: new { controller = "Pessoas", action = "CadastrarPessoa" }
                    );

            });
            app.Run();
        }
    }
}