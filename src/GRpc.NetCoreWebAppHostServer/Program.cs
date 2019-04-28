using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
namespace GRpc.NetCoreWebAppHostServer
{
    public class Program
    {

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
           .AddEnvironmentVariables()
           .Build();

        private static X509Certificate2 cert;
        public static void Main(string[] args)
        {
            string rootpath = AppDomain.CurrentDomain.BaseDirectory;
            Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//debug
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                    .MinimumLevel.Override("System", LogEventLevel.Information)//debug
                    .MinimumLevel.Override("Default", LogEventLevel.Information)//debug
                    .Enrich.FromLogContext()
                   
                    .WriteTo.ColoredConsole()//debug
                    //.WriteTo.RollingFile(AppDomain.CurrentDomain.BaseDirectory + @"\logs\GRpc.NetCoreWebAppHostServer.log",
                    //    fileSizeLimitBytes: 1_000_000,
                    //    shared: true,
                    //    flushToDiskInterval: TimeSpan.FromSeconds(10))
                    .CreateLogger();

            //手动加载 pfx 证书
            //cert = GetSigningCertificate(rootpath, "pwd", "localhostdevcert.pfx");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseUrls("https://localhost:50051")
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.Limits.MinRequestBodyDataRate = null;
                        options.Listen(IPAddress.Any, 50051, listenOptions =>
                        {
                            
                            listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2;

                            //我们使用ASP.NET核心开发证书
                            listenOptions.UseHttps();

                            //或者我们可以使用.pfx格式的自己的证书 X509Certificate2
                            //listenOptions.UseHttps(cert);
                        });
                    });
                    webBuilder.UseSerilog();
                    webBuilder.UseStartup<Startup>();
                    
                });

        internal static X509Certificate2 GetSigningCertificate(string rootPath, string pwd, string certName)
        {
            string tmp = Path.Combine(rootPath, "cert");
            
            var fileName = Path.Combine(tmp, certName);
            Log.Debug("Signing path:" + tmp);
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Signing Certificate is missing!");
            }
            var cert = new X509Certificate2(fileName, pwd);
            return cert;
        }
    }
}
