using shigLeBot.Methods;
using System.Collections;

namespace shigLeBot
{
    internal class BuiltInMethod
    {
        public Dictionary<string, IMethod> methods = new Dictionary<string, IMethod>();

        public BuiltInMethod()
        {
            SetBuiltInMethods();
        }

        private void SetBuiltInMethods()
        {
            methods.Add("if_else", new if_elseMethod());
            methods.Add("test", new testMethod());
        }

        public IEnumerator Run(string methodName, Message message, MethodInput methodInput)
        {
            if (!methods.TryGetValue(methodName, out IMethod method)) yield break;

            yield return null;

            var e = method.Run(message, methodInput);

            yield return null;

            while (e.MoveNext())
            {
                yield return null;
            }
        }
    }
}
