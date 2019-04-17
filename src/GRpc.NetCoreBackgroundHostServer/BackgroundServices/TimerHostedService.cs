using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GRpc.NetCoreBackgroundHostServer.BackgroundServices
{
    public class TimerHostedService : BackgroundService
    {
        //other ...

        private readonly IConfiguration _conf;
        public TimerHostedService(IConfiguration conf)
        {
            this._conf = conf;
        }

        private Timer _timer;

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(double.Parse(_conf["TimerHostedService:TimerHosted"])));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Serilog.Log.Information("Timer is working");
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Serilog.Log.Information("Timer is stopping");
            _timer?.Change(Timeout.Infinite, 0);
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _timer?.Dispose();
            base.Dispose();
        }
    }
}
