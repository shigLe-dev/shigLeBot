using System.Collections.ObjectModel;

namespace shigLeBot
{
    internal class MethodInput
    {
        public readonly ReadOnlyDictionary<string, string> inputString;
        public readonly ReadOnlyDictionary<string, bool> inputBoolean;
        public readonly ReadOnlyDictionary<string, float> inputFloat;

        public MethodInput(Dictionary<string, string> inputString, Dictionary<string, bool> inputBoolean, Dictionary<string, float> inputFloat)
        {
            this.inputString = new ReadOnlyDictionary<string, string>(inputString);
            this.inputBoolean = new ReadOnlyDictionary<string, bool>(inputBoolean);
            this.inputFloat = new ReadOnlyDictionary<string, float>(inputFloat);
        }
    }
}
