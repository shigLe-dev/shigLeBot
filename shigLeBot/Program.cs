using Discord.Commands;
using Discord.WebSocket;
using System.Collections;
using System.Text;

namespace shigLeBot
{
    class Program
    {
        public static List<IEnumerator> serverloops = new List<IEnumerator>();
        public static Dictionary<ulong, Server> servers = new Dictionary<ulong, Server>();
        public static BuiltInMethods builtInMethods;

        private static void Main(string[] args)
        {
            new Program();
        }

        Program()
        {
            SetServers();
            builtInMethods = new BuiltInMethods();
            Task.Run(() =>
            {
                MainLoop();
            });
            new DiscordBotServer(onMessage);
        }

        private void SetServers()
        {
            new Server(811964339375308890);
            new Server(1031157559404548107);
        }

        private void MainLoop()
        {
            while (true)
            {
                for (int i = 0; i < serverloops.Count; i++)
                {
                    // サーバーループを一つ進める
                    serverloops[i].MoveNext();
                }
            }
        }

        // メッセージを受けた場合
        private async Task onMessage(CommandContext context)
        {
            try
            {
                if (servers.TryGetValue(context.Guild.Id, out Server server))
                {
                    server.AddMessage(context);
                    StringBuilder builder = new StringBuilder();

                    builder.Append("MessageID : ");
                    builder.AppendLine(context.Message.Id.ToString());
                    builder.Append("    UserID : ");
                    builder.AppendLine(context.User.Id.ToString());
                    builder.Append("    GuildID : ");
                    builder.AppendLine(context.Guild.Id.ToString());
                    builder.Append("    ChannelID : ");
                    builder.AppendLine(context.Channel.Id.ToString());

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(builder.ToString());
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
            }
        }
    }
}
