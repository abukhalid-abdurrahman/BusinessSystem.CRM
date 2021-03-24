using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using BusinessSystem.Logger;
using System.IO;
using BusinessSystem.CRM.Extensions;
using BusinessSystem.CRM.Logics.Services.Category;
using BusinessSystem.CRM.Logics.Services.Good;
using BusinessSystem.CRM.Logics.Services.PartnerApplication;
using BusinessSystem.CRM.Logics.Services.User;
using BusinessSystem.Database.Models.BusinessObjects;
using BusinessSystem.Database.Models.DataTransferObjects.Request;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace BusinessSystem.CRM
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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGoodService, GoodService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IPartnersApplicationsService, PartnerApplicationService>();

            services.AddAuthorization();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Login/Index");
                        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Login/Index");
                        options.Cookie.Name = "BusinessSystemApplicationCookie";
                    });
            services.AddHttpContextAccessor();
            services.AddMvc().AddFluentValidation(fv =>
            {
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });
            services.AddTransient<IValidator<PartnerRequestModel>, Validators.UserValidator>();
            services.AddTransient<IValidator<CategoryRequestModel>, Validators.CategoryValidator>();
            services.AddTransient<IValidator<GoodRequestModel>, Validators.GoodValidator>();
            services.AddTransient<IValidator<RemovePartnerRequestModel>, Validators.RemoveUserValidator>();
            services.AddTransient<IValidator<MessagingEntity>, Validators.MessageValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            #region Logger
            loggerFactory.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "logger.txt"));
            var logger = loggerFactory.CreateLogger<Startup>();
            #endregion

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/Home/HandleError/{0}");
            app.UseMobileRedierct();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{Id?}");
            });
        }
    }
}
