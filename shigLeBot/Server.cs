using System.Collections;
using Discord.Commands;

namespace shigLeBot
{
    internal class Server
    {
        private ulong id;
        private List<Message> messages = new List<Message>();
        private List<Command> commands = new List<Command>();
        private List<Job> jobs = new List<Job>();

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
                // たまっているメッセージを変換できる場合はjobに変換する
                foreach (Message message in messages)
                {
                    foreach (var command in commands)
                    {
                        if (command.command == message.context.Message.Content)
                        {
                            jobs.Add(command.NewJob());
                        }

                        yield return null;
                    }

                    yield return null;
                }
                messages.Clear();

                // 

                yield return null;
            }
        }
    }
}
