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
                    var methods = kvp.Value;

                    foreach (var method in methods.AsObject())
                    {
                        ParseMethod(method.Key, method.Value);
                    }
                }
            });

            return result.ToArray();
        }

        private Method ParseMethod(string key, JsonNode value)
        {
            string id = key;
            string opcode = value["opcode"].ToString();
            string next = value["next"]?.ToString();

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
