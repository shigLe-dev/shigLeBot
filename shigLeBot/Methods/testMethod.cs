using System.Collections;

namespace shigLeBot.Methods
{
    internal class testMethod : IMethod
    {
        //Todo: Testメソッドの実装
        public IEnumerator Run(Message message, MethodInput methodInput)
        {
            message.context.Message.Channel.SendMessageAsync(message.context.Message.Author.Username + " : " + message.context.Message.Content);

            yield return null;
        }
    }
}
