using Microsoft.AspNetCore.SignalR.Client;

const string URL = "https://localhost:7140/chat";

await using var connection = new HubConnectionBuilder()
    .WithUrl(URL)
    .Build();

await connection.StartAsync();


await foreach (var message in connection.StreamAsync<string>("Streaming"))
{
    Console.WriteLine(message);
}
