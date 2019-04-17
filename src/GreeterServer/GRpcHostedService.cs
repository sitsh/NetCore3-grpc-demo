using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Threading.Tasks;
using GreeterServer.RpcImpl;
using Grpc.Core;
using Helloworld;
using Microsoft.Extensions.Configuration;

namespace GreeterServer
{
    public class GRpcHostedService : BackgroundService
    {
        private  int _port;
        private Server _server;
        private string _host;
        private IConfiguration _conf;
        public GRpcHostedService(ILoggerFactory loggerFactory, IConfiguration conf)
        {
            _conf = conf;
            _port = Convert.ToInt32(_conf["Grpc:port"]);
            _host = _conf["Grpc:host"];
        }

       

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("GRpcHosted is starting.");
            stoppingToken.ThrowIfCancellationRequested();


            _server = new Server
            {
                Services =
                {
                    Greeter.BindService(new GreeterImpl()),
                    PostFile.BindService(new PostFileImpl())
                },
                Ports = { new ServerPort(_host, _port, ServerCredentials.Insecure) }
            };
            _server.Start();

            Console.WriteLine("GRpcHosted is doing background work.");

            return Task.CompletedTask;
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {

            Console.WriteLine("GRpcHosted is stopping.");
            _server?.ShutdownAsync();

            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
              _server?.ShutdownAsync();
            base.Dispose();
        }
    }
}
