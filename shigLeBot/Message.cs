using Discord.Commands;

namespace shigLeBot
{
    internal class Message
    {
        public Message(CommandContext context)
        {
            this.context = context;
        }

        public CommandContext context;
    }
}
