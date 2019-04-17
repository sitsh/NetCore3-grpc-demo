using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GRpc.NetCoreBackgroundHostServer.BackgroundServices;

namespace GRpc.NetCoreBackgroundHostServer.Extensions
{
    public static class BackgroundServicesExtensions
    {
        //public static IHostBuilder UseHostedService<T>(this IHostBuilder hostBuilder)
        //    where T : class, IHostedService, IDisposable
        //{
        //    return hostBuilder.ConfigureServices(services =>
        //        services.AddHostedService<T>());
        //}

        //public static IHostBuilder UseTimerHosted(this IHostBuilder hostBuilder)
        //{
        //    return hostBuilder.ConfigureServices(services =>
        //        services.AddHostedService<TimerHostedService>());
        //}

        public static IServiceCollection AddTimerHosted(this IServiceCollection services)
        {
            /*
              *HealthChecks
              */
            services.AddHostedService<TimerHostedService>(); //测试 TimerHostedService 后台服务

            //Mq 后台服务添加
            //services.AddHostedService<ComsumeRabbitMQHostedService>();

            return services;
        }

        public static IServiceCollection AddComsumeRabbitMQHosted(this IServiceCollection services)
        {
            //Mq 后台服务添加
            services.AddHostedService<ComsumeRabbitMQHostedService>(); //测试 TimerHostedService 后台服务

            
            

            return services;
        }

        public static IServiceCollection AddGRpcServerHosted(this IServiceCollection services)
        {
            //grpc server 端点
            services.AddHostedService<GRpcHostedService>(); 
            

            return services;
        }

    }
}
