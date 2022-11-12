using System.Collections;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace shigLeBot
{
    internal class Parser
    {
        public readonly string code;

        public Parser(string code)
        {
            this.code = code;
        }


        public async Task<Command[]> Parse()
        {
            List<Command> result = new List<Command>();

            await Task.Run(() =>
            {
                var node = JsonNode.Parse(code);

                JsonNode commands = node["commands"];


                foreach (var kvp in commands.AsObject())
                {
                    var key = kvp.Key;
                    var value = kvp.Value;

                    List<Method> methods = new List<Method>();

                    foreach (var method in value.AsObject())
                    {
                        Method m = ParseMethod(method.Key, method.Value);
                        Console.WriteLine(m.id);
                        methods.Add(m);
                    }

                    result.Add(new Command(key, jsonCommandJ, methods));
                }
            });

            return result.ToArray();
        }

        //TODO: JSONからパースした処理を実際に実行するコードを書かないといけない
        private IEnumerator jsonCommandJ(Message m, object obj)
        {
            Method[] methods = ((List<Method>)obj).ToArray();
            // methodsが一つもない場合すぐに終わる
            if (methods.Length == 0) yield break;
            Method entryPoint = methods[0];

            yield return null;

            // idをキーにしたmethodの辞書を作る
            Dictionary<string, Method> id_methods = new Dictionary<string, Method>();
            foreach (var method in methods)
            {
                id_methods[method.id] = method;
            }

            yield return null;

            Method currentMethod = entryPoint;

            // entrypointから順に実行する
            while (true)
            {
                // 実行
                var e = Program.builtInMethod.Run(currentMethod.opcode, m, currentMethod.input);
                while (e.MoveNext())
                {
                    yield return null;
                }

                yield return null;


                // nextがある場合それを実行する
                if (currentMethod.next == "")
                {
                    Console.WriteLine("nextが存在しません。正常終了です。：" + currentMethod.id);
                    break;
                }

                currentMethod = id_methods[currentMethod.next];

                yield return null;
            }
        }

        private Method ParseMethod(string key, JsonNode value)
        {
            string id = key;
            string opcode = value["opcode"]?.GetValue<string>() ?? "";
            string next = value["next"]?.ToString() ?? "";

            Dictionary<string, string> inputString = new Dictionary<string, string>();
            Dictionary<string, bool> inputBoolean = new Dictionary<string, bool>();
            Dictionary<string, int> inputInt = new Dictionary<string, int>();
            Dictionary<string, float> inputFloat = new Dictionary<string, float>();

            foreach (var input in value["inputs"].AsObject())
            {
                var element = input.Value.GetValue<JsonElement>();
                switch (element.ValueKind)
                {
                    case JsonValueKind.Undefined:
                        break;
                    case JsonValueKind.Object:
                        break;
                    case JsonValueKind.Array:
                        break;
                    case JsonValueKind.String:
                        inputString[input.Key] = input.Value.GetValue<string>();
                        break;
                    case JsonValueKind.Number:
                        if (float.TryParse(input.Value.GetValue<float>().ToString(), out float vf))
                        {
                            inputFloat[input.Key] = vf;
                        }
                        else if (int.TryParse(input.Value.GetValue<float>().ToString(), out int vi))
                        {
                            inputInt[input.Key] = vi;
                        }
                        break;
                    case JsonValueKind.True:
                        inputBoolean[input.Key] = true;
                        break;
                    case JsonValueKind.False:
                        inputBoolean[input.Key] = false;
                        break;
                    case JsonValueKind.Null:
                        break;
                    default:
                        break;
                }
            }

            MethodInput methodInput = new MethodInput(inputString, inputBoolean, inputInt, inputFloat);

            return new Method(id, opcode, next, methodInput);
        }
    }
}
