using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Configuration;

namespace shigLeBot
{
    class DiscordBotServer
    {
        private readonly DiscordSocketClient client;
        private Func<CommandContext, Task> onMessage;

        public DiscordBotServer(Func<CommandContext, Task> onMessage)
        {
            this.onMessage = onMessage;

            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            });
            client.Log += LogAsync;
            client.Ready += onReady;
            client.MessageReceived += message =>
            {
                // メッセージがnullの場合は無視する
                if (message == null) return Task.CompletedTask;
                // メッセージが時分自身の場合は無視する
                if (message.Author.Id == client.CurrentUser.Id) return Task.CompletedTask;

                // コンテキスト生成
                CommandContext context = new CommandContext(client, message as SocketUserMessage);

                return this.onMessage(context);
            };

            MainAsync().GetAwaiter().GetResult();
        }

        public async Task MainAsync()
        {
            await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("bottoken"));
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
    }
}
