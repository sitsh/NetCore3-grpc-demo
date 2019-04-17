using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using GRpc.NetCoreBackgroundHostServer.BackgroundServices;
using GRpc.NetCoreBackgroundHostServer.Extensions;

namespace GRpc.NetCoreBackgroundHostServer
{
    public class Startup
    {

        public static IServiceCollection Services { set; get; }
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(env.ContentRootPath)
            //    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            //Configuration = builder.Build();
            //Services = services;
            Configuration = Program.Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //加入 缓存模块
            //services.AddMemoryCache();
            
            //加入 HealthChecks 健康监控服务
            services.AddHealthChecksService(Configuration);


            //后台服务测试 定时打印
            //services.AddTimerHosted();

            //Mq 后台任务
            //services.AddComsumeRabbitMQHosted();

            //grpc server
            services.AddGRpcServerHosted();

            //注入配置文件
            services.Configure<IConfiguration>(Configuration);
            

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWelcomePage("/");
            }
            else
            {
                app.UseHsts();
            }
            
            //使用 HealthChecks 健康监控服务
            app.UseHealthChecksService();

            

            app.UseMvc();

        }

       
    }

    
}
