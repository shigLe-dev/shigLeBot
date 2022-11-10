using System.Collections;

namespace shigLeBot
{
    internal interface IMethod
    {
        public IEnumerator Run(MethodInput methodInput);
    }
}
