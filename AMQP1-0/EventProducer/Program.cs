using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventProducer
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.ConfigureMessaging(
                        "amqp://guest:guest@localhost:32772?nms.clientId=GalacticEmpire.EventProducer1",
                        "GalacticEmpireBC",
                        "EventProducer1");

                    services.AddHostedService<EventProducerWorker>();
                });
    }
}