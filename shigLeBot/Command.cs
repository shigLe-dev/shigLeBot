using System;
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

        public Job NewJob()
        {
            Job job = new Job();
            return job;
        }
    }
}
