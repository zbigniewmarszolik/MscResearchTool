using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MScResearchTool.Server.Binder.AutofacModules;
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
            services.AddMvc();

            var builder = new ContainerBuilder();

            builder.RegisterModule<InfrastructureModule>();

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
                app.UseExceptionHandler("/Home/Error");
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home/Error/";
                    await next();
                }
            });

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
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
