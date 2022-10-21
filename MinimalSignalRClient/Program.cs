using Microsoft.AspNetCore.SignalR.Client;
using MinimalSignalRClient.models;

const string URL = "https://localhost:7140/chat";

await using var connection = new HubConnectionBuilder()
    .WithUrl(URL)
    .Build();

await connection.StartAsync();

// await foreach (var message in connection.StreamAsync<string>("Streaming"))
// {
//     Console.WriteLine(message);
// }

await connection.SendAsync("ReceiveMessage", "Hello World");


// await foreach (var message in connection.StreamAsync<string>("Streaming"))
// {
//     Console.WriteLine(message);
// }
