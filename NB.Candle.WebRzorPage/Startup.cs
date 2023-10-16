using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.Services.Claims;
using NB.Candle.WebRzorPage.Services.Email;
using NB.Candle.WebRzorPage.Services.Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue; // or your desired value
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("AppDbContext")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(Startup));

            services.AddMediatR(typeof(Startup));

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Vendor",
                    policy => policy.RequireRole("Vendor"));
            });
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.AccessDeniedPath = "/Error";
            //    options.Cookie.Name = "AC";
            //    options.Cookie.HttpOnly = true;
            //    options.ExpireTimeSpan = TimeSpan.FromDays(30);
            //    options.LoginPath = "/Login";
            //    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            //    options.SlidingExpiration = true;
            //});

            //services.AddAuthorization(options =>
            //{
            //    // Create policies
            //    options.AddPolicy("Vendor", p => p.RequireRole("Vendor"));
            //});
            //services.AddRazorPages().AddRazorPagesOptions(options =>
            //{
            //    options.Conventions.AuthorizeAreaFolder("Vendor", "/", "Vendor");
            //});


            services.AddScoped<IUserClaimsPrincipalFactory<User>, ClaimFactory>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ISmsSender, SmsSender>();
            services.AddSingleton<
            IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            }
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
