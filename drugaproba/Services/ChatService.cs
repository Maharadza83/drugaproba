using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;    // <- tu
using drugaproba.Models;

namespace drugaproba.Services;

public class ChatService
{
    private HubConnection? _hub;
    public event Action<Message>? OnMessageReceived;

    public async Task StartConnection(string token)
    {
        _hub = new HubConnectionBuilder()
            .WithUrl("http://localhost:5244/chatHub", opts =>
            {
                // zwracamy Task<string?> aby pasowało do API
                opts.AccessTokenProvider = () => Task.FromResult<string?>(token);
            })
            .WithAutomaticReconnect()
            .Build();

        _hub.On<Message>("ReceiveMessage", msg => OnMessageReceived?.Invoke(msg));
        await _hub.StartAsync();
    }

    public Task SendMessage(string text)
        => _hub?.SendAsync("SendMessageToChannel", new { ChannelId = "general", Message = text })
        ?? Task.CompletedTask;
}
