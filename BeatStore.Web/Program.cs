
using System.IO;

namespace BeatStore.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationOptions opts;
            opts = new()
            {
                Args = args,
                WebRootPath = "/app/bin/Debug/net6.0/wwwroot",
                ContentRootPath = "/app/bin/Debug/net6.0",
            };
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");
            app.Run();
        }
    }
}