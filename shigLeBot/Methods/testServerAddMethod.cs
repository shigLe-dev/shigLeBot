using System.Collections;

namespace shigLeBot.Methods
{
    internal class testServerAddMethod : IMethod
    {
        public IEnumerator Run(Message message, MethodInput methodInput)
        {
            Task.Run(async () =>
            {
                var a = await new Parser(new StreamReader("C:\\Users\\KurisuJuha\\Documents\\GitHub\\shigLeBot\\shigLeBot\\Example2.json").ReadToEnd()).Parse();
                Console.WriteLine(Program.servers.Count);
                if (Program.servers.TryGetValue(message.context.Guild.Id, out Server server))
                {
                    foreach (var item in a)
                    {
                        server.AddCommand(item);
                    }
                }
            });

            yield return null;
        }
    }
}
