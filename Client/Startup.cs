using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Client.Models.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Client
{
    public class Startup
    {
        private IConfiguration configuration;
        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "SCHEME_ADMIN";
            })
            .AddCookie("SCHEME_ADMIN", option =>
            {
                option.LoginPath = "/admin/home/login";
                option.AccessDeniedPath = "/admin/home/accessDenied";
                option.LogoutPath = "/admin/home/logout";
                option.Cookie.Name = "loangngoangadmin";
            }).AddCookie("SCHEME_USER", option =>
            {
                option.LoginPath = "/home/login";
                option.AccessDeniedPath = "/home/accessDenied";
                option.LogoutPath = "/home/logout";
                option.Cookie.Name = "loangngoanguser";
            });
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IBallotRequestRepository, BallotRequestRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                ClaimsPrincipal principal = new ClaimsPrincipal();
                var result = await context.AuthenticateAsync("SCHEME_ADMIN");
                if (result?.Principal != null)
                {
                    principal.AddIdentities(result.Principal.Identities);
                }
                var result2 = await context.AuthenticateAsync("SCHEME_USER");
                if (result2?.Principal != null)
                {
                    principal.AddIdentities(result2.Principal.Identities);
                }
                context.User = principal;
                await next();
            });
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("admin_route", "admin/{controller}/{action}/{id?}",
                    defaults: new { area = "admin" }, constraints: new { area = "admin" });
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
