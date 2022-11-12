using System.Collections;

namespace shigLeBot
{
    internal interface IMethod
    {
        public IEnumerator Run(Message message, MethodInput methodInput);
    }
}
