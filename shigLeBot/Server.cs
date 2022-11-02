using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shigLeBot
{
    internal class Server
    {
        public Server(ulong id)
        {
            this.id = id;

            Program.servers.Add(id, this);
        }

        private ulong id;
    }
}
