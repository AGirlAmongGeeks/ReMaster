using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Targets;
using Microsoft.AspNetCore.Http;
using NLog.Web;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ReMaster.BusinessLogic.Company;
using ReMaster.EntityFramework.Repositories;
using ReMaster.EntityFramework;
using Microsoft.EntityFrameworkCore;


namespace ReMaster
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
			env.ConfigureNLog("nlog.config");
		}

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddDbContext<ReMasterDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			services.AddAutoMapper(typeof(Startup));

			services.AddMvc();

			services.AddSingleton(typeof(IRepositoryBase<>), typeof(RepositoryBase<>)); 
			services.AddSingleton(typeof(ICompanyRepository<>), typeof(CompanyRepository<>));
			services.AddSingleton<ICompanyAppService, CompanyAppService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
				routes.MapRoute("areaRoute", "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
				routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

			//add NLog to ASP.NET Core
			loggerFactory.AddNLog();

			//add NLog.Web
			app.AddNLogWeb();
		}
    }
}
