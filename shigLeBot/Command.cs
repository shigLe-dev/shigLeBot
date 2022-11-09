using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shigLeBot
{
    internal class Command
    {
        public string key { get; private set; } = "";
        private Func<Message, IEnumerator> j;
        
        public Command(string key, Func<Message, IEnumerator> j)
        {
            this.key = key;
            this.j = j;
        }

        public IEnumerator NewJob(Message message)
        {
            if (j == null) return job(message);

            return j(message);
        }

        private IEnumerator job(Message message)
        {
            Console.WriteLine(message.context.Message.Content);

            yield return null;
        }
    }
}
