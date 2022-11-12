using System.Collections;

namespace shigLeBot.Methods
{
    internal class testArgsMethod : IMethod
    {
        public IEnumerator Run(Message message, MethodInput methodInput)
        {
            yield return null;

            string inputString = "";
            bool inputTrue = false;
            bool inputFalse = true;
            float inputFloat = 0.0f;

            if (methodInput.inputString.TryGetValue("param1", out string s1)) inputString = s1;
            if (methodInput.inputBoolean.TryGetValue("param3", out bool b1)) inputTrue = b1;
            if (methodInput.inputBoolean.TryGetValue("param4", out bool b2)) inputFalse = b2;
            if (methodInput.inputFloat.TryGetValue("param5", out float f1)) inputFloat = f1;

            message.context.Message.Channel.SendMessageAsync(inputString + "\n" + inputTrue + "\n" + inputFalse + "\n" + inputFloat);
        }
    }
}
