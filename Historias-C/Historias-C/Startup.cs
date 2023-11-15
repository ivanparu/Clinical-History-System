using Historias_C.Data;
using Historias_C.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Historias_C
{
    public static class Startup
    {
        public static WebApplication InicializarApp(string[] args)
        {
            //Crear una nueva instancia de nuestro servidor web
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder); // Lo configuramos con sus respectivos servicios

            var app = builder.Build(); //Sobre esta app, configuraremos los middlewares
            Configure(app); // COnfiguramos los middleware

            return app; //Retornamos la app ya inicializada
        }
        private static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddDbContext<HistoriasClinicasCContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("HistoriasDBCS")));

            builder.Services.AddIdentity<Persona, Rol>().AddEntityFrameworkStores<HistoriasClinicasCContext>();

            builder.Services.Configure<IdentityOptions>(opciones =>
            {
                opciones.Password.RequireNonAlphanumeric = false;
                opciones.Password.RequireLowercase = false;
                opciones.Password.RequireUppercase = false;
                opciones.Password.RequireDigit = false;
                opciones.Password.RequiredLength = 5;
            }
            );

           builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, opciones =>
            {
                opciones.LoginPath = "/Account/IniciarSesion";
                opciones.AccessDeniedPath = "/Account/AccesoDenegado";
                opciones.Cookie.Name = "IdentityHistoriasClinicasApp";
            });
            builder.Services.AddControllersWithViews();

        }
        private static void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
