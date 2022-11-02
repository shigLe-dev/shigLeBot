using System.Collections;
using Discord.Commands;

namespace shigLeBot
{
    internal class Server
    {
        private ulong id;
        private List<Message> messages = new List<Message>();
        private List<Command> commands = new List<Command>();

        public Server(ulong id)
        {
            this.id = id;

            Program.servers.Add(id, this);
        }

        public void AddMessage(CommandContext context)
        {
            messages.Add(new Message(context));
        }

        public IEnumerator ServerLoop()
        {
            while (true)
            {
                Console.WriteLine(id);
                yield return null;
            }
        }
    }
}
