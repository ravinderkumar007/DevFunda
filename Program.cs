using Devfunda.Areas.Admin.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System;

namespace Devfunda
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<DevfundaDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Admin/Account/Login";
        options.LogoutPath = "/Admin/Account/Logout";
        options.AccessDeniedPath = "/Admin/Account/Login";
    });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

         
                app.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
            );


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=index}/{id?}");

            app.MapControllerRoute(
      name: "topic-detail",
      pattern: "Tutorials/{categorySlug}/{topicSlug}",
      defaults: new { controller = "Tutorials", action = "Topic" });

            app.MapControllerRoute(
                name: "category-topics",
                pattern: "Tutorials/{categorySlug}",
                defaults: new { controller = "Tutorials", action = "Category" });

            app.MapControllerRoute(
                name: "all-categories",
                pattern: "Tutorials",
                defaults: new { controller = "Tutorials", action = "Index" });

            app.Run();
        }
    }
}
