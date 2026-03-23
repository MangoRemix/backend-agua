using backend_agua.Hubs;
using backend_agua.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace backend_agua.BackgroundServices;

public class ReportTimerWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IHubContext<ReportHub> _hubContext;
    private readonly ILogger<ReportTimerWorker> _logger;

    public ReportTimerWorker(
        IServiceProvider serviceProvider,
        IHubContext<ReportHub> hubContext,
        ILogger<ReportTimerWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _hubContext = hubContext;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ReportTimerWorker is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var reporteService = scope.ServiceProvider.GetRequiredService<IReporteService>();
                    var status = await reporteService.GetReporteStatusAsync();

                    // Notificar a todos los clientes el nuevo estado
                    await _hubContext.Clients.All.SendAsync("ReceiveReportStatus", status, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error broadcasting report status");
            }

            // Esperar 5 segundos antes de la siguiente actualización
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        _logger.LogInformation("ReportTimerWorker is stopping.");
    }
}
