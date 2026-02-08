using Microsoft.AspNetCore.SignalR;
using GameServer.Services;

namespace GameServer.Hubs;

public class GameHub(MatchMakingService matchMaker) : Hub
{
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine($"Player connected: {Context.ConnectionId}");
        await Clients.Caller.SendAsync("Welcome", "Connected to game server!");
        await base.OnConnectedAsync();
    }

    public async Task JoinQueue() 
    {
        matchMaker.AddToQueue(Context.ConnectionId);
        await Clients.Caller.SendAsync("QueueJoined", "Searching for Opponent");
    }
}