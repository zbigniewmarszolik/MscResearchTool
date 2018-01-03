using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MScResearchTool.Server.Binder.AutofacModules;
using MScResearchTool.Server.Web.AutofacModules;
using MScResearchTool.Server.Web.Configurations;
using System;

namespace MScResearchTool.Server.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IContainer _container { get; set; }
        private IMapper _mapper { get; set; }

        public Startup(IConfiguration configuration)
        {
            InitializerAutoMapper();

            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Identity/Login/";
                    options.LogoutPath = "/Identity/Login/";
                });

            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            var builder = new ContainerBuilder();

            builder.RegisterModule<InfrastructureModule>();
            builder.RegisterModule<BusinessModule>();
            builder.RegisterModule<WebModule>();

            builder.Populate(services);
            builder.RegisterInstance(_mapper).As<IMapper>();

            _container = builder.Build();

            return new AutofacServiceProvider(_container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Site/Error");
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Site/Error/";
                    await next();
                }
            });

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Identity}/{action=Index}/{id?}");
            });
        }

        private void InitializerAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperConfiguration>();
            });

            _mapper = config.CreateMapper();
        }
    }
}
