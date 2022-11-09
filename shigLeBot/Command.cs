using System.Collections;

namespace shigLeBot
{
    internal class Command
    {
        public readonly string key = "";
        private readonly Func<Message, object, IEnumerator> j;
        private readonly object obj;
        
        public Command(string key, Func<Message, object, IEnumerator> j, object obj)
        {
            this.key = key;
            this.j = j;
            this.obj = obj;
        }

        public IEnumerator NewJob(Message message)
        {
            if (j == null) return nullJob(message);

            return j(message, obj);
        }

        private IEnumerator nullJob(Message message)
        {
            Console.WriteLine(message.context.Message.Content);

            yield return null;
        }
    }
}
