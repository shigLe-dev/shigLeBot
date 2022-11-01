using Discord;
using Discord.Commands;
using Discord.Webhook;
using Discord.WebSocket;
using System;
using System.Configuration;

namespace shigLeBot
{
    class Program
    {
        private readonly DiscordSocketClient _client;
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            _client = new DiscordSocketClient();
            _client.Log += LogAsync;
            _client.Ready += onReady;
            _client.MessageReceived += onMessage;
        }

        public async Task MainAsync()
        {
            await _client.LoginAsync(TokenType.Bot, ConfigurationManager.AppSettings["botToken"]);
            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task onReady()
        {
            Console.WriteLine($"{_client.CurrentUser} is Running!!");
            return Task.CompletedTask;
        }

        private async Task onMessage(SocketMessage message)
        {
            if (message.Author.Id == _client.CurrentUser.Id)
            {
                return;
            }
            if (message.Content == "hoge")
            {
                await message.Channel.SendMessageAsync("foo @" + message.Author.Username);
            }
        }
    }
}