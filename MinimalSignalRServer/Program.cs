using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MinimalSignalRServer.models;


var _messagens = new List<Mensagem>();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.Add(new ServiceDescriptor(typeof(List<Mensagem>), _messagens));

var app = builder.Build();

app.MapHub<MyHub>("/chat");
app.MapGet("/", () => "Hello World!");

app.Run();


class MyHub : Hub
{

    private List<Mensagem> _messagens = new List<Mensagem>();

    MyHub([FromServices] List<Mensagem> messagens)
    {
        _messagens = messagens;
    }


    public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            yield return DateTime.Now;
            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
        }
    }


    public async IAsyncEnumerable<DateTime> DataAsSendedLastMessage(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {


            yield return _messagens.Count() == 0 ? DateTime.Now : _messagens.Last().DataHoraDeEnvio;
            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
        }
    }


    public async IAsyncEnumerable<String> StreamingMessageToClient()
    {
        await foreach (var time in Streaming(Context.ConnectionAborted))
        {
            yield return _messagens.Count() > 0 ? _messagens.Last().Texto : "Nenhuma mensagem enviada";
            await Clients.All.SendAsync("ReceiveMessage", time);
        }
    }

    public async Task ReceiveMessage(string mensagem)
    {
        var message = new Mensagem(mensagem);
        await Clients.All.SendAsync("ReceiveMessage", message);
        _messagens.Add(message);


    }
}