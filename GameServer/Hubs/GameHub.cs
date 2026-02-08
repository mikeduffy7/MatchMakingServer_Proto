using Microsoft.AspNetCore.SignalR;

namespace GameServer.Hubs;

public class GameHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}