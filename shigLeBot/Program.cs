using Discord;
using Discord.Commands;
using Discord.Webhook;
using Discord.WebSocket;
using System;
using System.Collections;
using System.Configuration;

namespace shigLeBot
{
    class Program
    {
        private static DiscordSocketClient client;
        public static List<IEnumerator> serverloops = new List<IEnumerator>();
        public static Dictionary<ulong, Server> servers = new Dictionary<ulong, Server>();

        #region ボイラーテンプレート
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            SetServers();
            client = new DiscordSocketClient(new DiscordSocketConfig()
            {
                GatewayIntents = GatewayIntents.All
            });
            client.Log += LogAsync;
            client.Ready += onReady;
            client.MessageReceived += onMessage;

            Task.Run(MainLoop);
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
        #endregion

        private void SetServers()
        {
            new Server(811964339375308890);
            new Server(1031157559404548107);
        }

        private void MainLoop()
        {
            while (true)
            {
                for (int i = serverloops.Count - 1; i >= 0; i--)
                {
                    // サーバーループを一つ進める
                    serverloops[i].MoveNext();
                }
            }
        }

        // メッセージを受けた場合
        private async Task onMessage(SocketMessage message)
        {
            try
            {
                // メッセージがnullの場合は無視する
                if (message == null) return;
                // メッセージが時分自身の場合は無視する
                if (message.Author.Id == client.CurrentUser.Id) return;

                // コンテキスト生成
                CommandContext context = new CommandContext(client, message as SocketUserMessage);

                if (servers.TryGetValue(context.Guild.Id, out Server server))
                {
                    server.AddMessage(context);
                    await message.Channel.SendMessageAsync(context.Message.Content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
