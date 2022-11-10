using shigLeBot.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shigLeBot
{
    internal class BuiltInMethods
    {
        public Dictionary<string, IMethod> methods = new Dictionary<string, IMethod>();

        public BuiltInMethods()
        {
            SetBuiltInMethods();
        }

        private void SetBuiltInMethods()
        {
            methods.Add("if_else", new if_else());
        }
    }
}
