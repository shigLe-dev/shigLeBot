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
        public string command { get; private set; } = "";
        
        public Command(string command)
        {
            this.command = command;
        }

        public IEnumerator NewJob(Message message)
        {
            return job(message);
        }

        private IEnumerator job(Message message)
        {
            yield return null;
        }
    }
}
