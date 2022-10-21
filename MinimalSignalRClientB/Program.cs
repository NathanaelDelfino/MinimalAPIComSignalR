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



// await foreach (var dataEHoraDaUltimaMenssagem in connection.StreamAsync<string>("DataAsSendedLastMessage"))
// {
//     // bool DataEMaiorQueUltimaMensagem = DateTime.Compare(DateTime.Parse(dataEHoraDaUltimaMenssagem), ultimamensagem) > 0;

//     // if (DataEMaiorQueUltimaMensagem)
//     // {

//     // }
// ultimamensagem = Convert.ToDateTime(dataEHoraDaUltimaMenssagem);

// }


await foreach (var message in connection.StreamAsync<string>("StreamingMessageToClient"))
{
    Console.WriteLine(message);
}



