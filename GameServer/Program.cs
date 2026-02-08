using GameServer.Hubs;
using GameServer.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddSingleton<MatchMakingService>();

var app = builder.Build();
app.MapHub<GameHub>("/game");

Console.WriteLine("Game server starting...");
app.Run();