using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TEAMModelOS.SDK.Context.Configuration;
using TEAMModelOS.SDK.Context.Filter;
using TEAMModelOS.SDK.Extension.HttpClient;
using TEAMModelOS.SDK.Extension.JwtAuth;
using TEAMModelOS.SDK.Extension.JwtAuth.Filters;
using TEAMModelOS.SDK.Module.AzureBlob.Configuration;
using TEAMModelOS.Service.Core.Interfaces;

namespace TEAMModelOS.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);//增加环境配置文件，新建项目默认有
            this.Configuration = builder.Build();
            BaseConfigModel.SetBaseConfig(Configuration, env.ContentRootPath, env.WebRootPath);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //上传文件最大处理
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = long.MaxValue; // In case of multipart
                x.MultipartHeadersLengthLimit = int.MaxValue;
            });
            //使用Blob配置
            services.AddAzureBlobStorage().AddConnection(Configuration.GetSection("Azure:Blob").Get<AzureBlobOptions>());
            //全局扫描基于IBusinessService接口的实现类
            services.Scan(scan => scan.FromApplicationDependencies()
               .AddClasses(classes => classes.AssignableTo<IBusinessService>())
                   .AsImplementedInterfaces()
                   .WithTransientLifetime());
            //引入Jwt配置
            services.JwtAuth(Configuration.GetSection("JwtSetting"));
            //HttpContextAccessor，并用来访问HttpContext。
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Http   https://blog.yowko.com/httpclientfactory-dotnet-core-dotnet-framework/
            //services.AddHttpClient();
			services.AddHttp();
			//services.AddTransient<ITeamModelUserService, ITeamModelUserService>();
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
				app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
				{
					HotModuleReplacement = true
				});
			}
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseMiddleware<JwtAuthorizationFilter>();
            app.UseMiddleware<HttpGlobalExceptionInvoke>();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
				routes.MapSpaFallbackRoute(
					name: "spa-fallback",
					defaults: new { controller = "Home", action = "Index" });
			});
        }
    }
}
