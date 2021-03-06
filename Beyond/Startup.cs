using System.Reflection;
using AutoMapper;
using Beyond.Data;
using Beyond.Data.Models;
using Beyond.Services;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Beyond
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });
            services.AddDefaultIdentity<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/identity/Account/Login";
            });
            services.AddMemoryCache();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddControllersWithViews();
            services.AddScoped<ITakeRanks, TakeRanks>();
            services.AddScoped<ITakeEntityById, TakeEntityById>();
            services.AddScoped<ITakeModels, TakeModels>();
            services.AddScoped<ICreateEntity, CreateEntity>();
            services.AddScoped<IGenerate, Generate>();
            services.AddScoped<IDeleteEntity, DeleteEntity>();
            services.AddScoped<IAssignToRole, AssignToRole>();
            services.AddScoped<ICreateDto, CreateDto>();
            services.AddScoped<IUpdateEntity, UpdateEntity>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            context.Database.Migrate();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

            });
            //Generate "Admin" and "User" roles
            AppDbInitializer.GenerateRoles(app);
            //Creates admin acc
            AppDbInitializer.CreateAdmin(app);
            //Data Seed "optional"
            AppDbInitializer.Seed(app);
        }
    }
}
