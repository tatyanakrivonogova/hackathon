using Microsoft.Extensions.Hosting;

class ExperimentWorker(IHostApplicationLifetime host, Experiment experiment) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await experiment.Run();
        host.StopApplication();
    }
}