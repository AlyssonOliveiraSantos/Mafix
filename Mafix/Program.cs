using Mafix.Data;
using Mafix.Helper;
using Mafix.Repositorio;
using Mafix.Repositorio.Interfaces;
using Mafix.Services;
using Mafix.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mafix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.UseUrls("https://inf05:5000");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BancoContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<ISessao, Sessao>();
            builder.Services.AddScoped<IMaquinaRepositorio, MaquinaRepositorio>();
            builder.Services.AddScoped<IOperadorRepositorio, OperadorRepositorio>();
            builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            builder.Services.AddScoped<IProducaoRepositorio, ProducaoRepositorio>();
            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            builder.Services.AddScoped<IParadaMaquinaRepositorio, ParadaMaquinaRepositorio>();
            builder.Services.AddScoped<IRelatorioService, RelatorioService>();
            builder.Services.AddScoped<IProducaoService, ProducaoService>();


            builder.Services.AddSession(o =>
            {
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
            });


            var app = builder.Build();

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

            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
