using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("==== Game Client =====");
Console.WriteLine("Connected to server...");

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5088/game")
    .Build();

connection.On<string>("Welcome", (message) =>
{
    Console.WriteLine($"Server Message: {message}");
});

connection.On<string>("QueueJoined", (message) =>
{
    Console.WriteLine($"Server Message: {message}");
});

connection.On<string>("MatchFound", (message) => 
{
    Console.WriteLine($"Server Message: {message}");
});

connection.Closed += async (error) =>
{
    Console.WriteLine("Connection Closed");
    await Task.CompletedTask;
};

try
{
    await connection.StartAsync();
    Console.WriteLine("✓ Connected!");
    Console.WriteLine("Type 'queue' to join matchmaking queue, 'quit' to exit");

    while (true)
    {
        var input = Console.ReadLine() ?? "";

        if(input?.ToLower() == "quit")
            break;

        if(input.ToLower() == "queue")
        {
            await connection.InvokeAsync("JoinQueue");
        }
    }

    await connection.StopAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

Console.WriteLine("Disconnected.");