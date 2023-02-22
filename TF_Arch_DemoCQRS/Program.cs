using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using TF_Arch_DemoCQRS.Models.Entities;
using TF_Arch_DemoCQRS.Models.Queries;
using Tools.Cqrs.Queries;

namespace TF_Arch_DemoCQRS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            IConfiguration configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<DbConnection>(sp => new SqlConnection(configuration.GetConnectionString("Default")));
            builder.Services.AddScoped<IQueryHandler<GetAllProductQuery,IEnumerable<Produit>>, GetAllProductQueryHandler >();
            builder.Services.AddScoped<GetOneProductQueryHandler>();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}