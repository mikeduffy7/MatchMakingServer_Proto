var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();
app.MapHub<GameServer.Hubs.GameHub>("/game");  // <-- Full namespace

Console.WriteLine("Game server starting...");
app.Run();