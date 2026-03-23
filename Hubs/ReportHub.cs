using Microsoft.AspNetCore.SignalR;

namespace backend_agua.Hubs;

public class ReportHub : Hub
{
    // Este hub se encargará de transmitir el estado de los reportes en tiempo real
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}
