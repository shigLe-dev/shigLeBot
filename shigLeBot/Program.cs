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
        private static DiscordSocketClient client;
        public static Dictionary<ulong, Server> servers = new Dictionary<ulong, Server>();

        #region ボイラーテンプレート
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            });
            client.Log += LogAsync;
            client.Ready += onReady;
            client.MessageReceived += onMessage;
        }

        public async Task MainAsync()
        {
            await client.LoginAsync(TokenType.Bot, ConfigurationManager.AppSettings["botToken"]);
            await client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task onReady()
        {
            Console.WriteLine($"{client.CurrentUser} is Running!!");
            return Task.CompletedTask;
        }

        private async Task onMessage(SocketMessage message)
        {
            if (message.Author.Id == client.CurrentUser.Id)
            {
                return;
            }
            if (message.Content == "hoge")
            {
                await message.Channel.SendMessageAsync(message.Author.Mention + "foo");
                CommandContext context = new CommandContext(client, message as SocketUserMessage);
                Console.WriteLine(context.Guild.Id);
            }
        }
        #endregion
    }
}
