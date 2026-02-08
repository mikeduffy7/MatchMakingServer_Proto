using System;
using Microsoft.AspNetCore.SignalR;

using Models;
using GameServer.Hubs;

namespace GameServer.Services;

public class MatchMakingService(IHubContext<GameHub> _hubContext)
{
    readonly Queue<QueuedPlayer> _queue = new();

    public void AddToQueue(string id) 
    {
        _queue.Enqueue(new QueuedPlayer { 
                ConnectionId = id, 
                JoinedAt = DateTime.UtcNow 
        });

        Console.WriteLine($"Players in queue: {_queue}");

        FindMatch();
    }

    public async Task FindMatch() 
    {
        while(_queue.Count >= 2) 
        {
            var player1 = _queue.Dequeue();
            var player2 = _queue.Dequeue();

            Console.WriteLine($"Match created: {player1.ConnectionId} vs {player2.ConnectionId}");
            
            await _hubContext.Clients.Client(player1.ConnectionId)
                .SendAsync("MatchFound", $"Found a Match Versus Player2: {player2.ConnectionId}");

            await _hubContext.Clients.Client(player2.ConnectionId)
                .SendAsync("MatchFound", $"Found a Match Versus Player1: {player1.ConnectionId}");
        }
    }
}
