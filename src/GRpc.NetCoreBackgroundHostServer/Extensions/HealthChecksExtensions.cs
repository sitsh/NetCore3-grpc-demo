using System;
using System.Collections.Generic;
using System.Text;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GRpc.NetCoreBackgroundHostServer.Common;

namespace GRpc.NetCoreBackgroundHostServer.Extensions
{
    public static class HealthChecksExtensions
    {
        /// <summary>
        /// 配置 api Swagger  Startup 辅助类
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHealthChecksService(this IServiceCollection services, IConfiguration configuration)
        {
            /*
              *HealthChecks
             * 更多 HC 监控 https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks
              */
            services.AddHealthChecks()
                .AddCheck<RandomHealthCheck>("random");   //随机测试HC 服务。


            return services;
        }
        /// <summary>
        /// 配置 api Swagger  Startup 辅助类
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseHealthChecksService(this IApplicationBuilder app)
        {

            //HealthChecks
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true
            });

            app.UseHealthChecks("/healthz", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }

    }
}
