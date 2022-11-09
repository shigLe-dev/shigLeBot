using System.Collections.ObjectModel;

namespace shigLeBot
{
    internal class MethodInput
    {
        public readonly ReadOnlyDictionary<string, string> inputString;
        public readonly ReadOnlyDictionary<string, bool> inputBoolean;
        public readonly ReadOnlyDictionary<string, int> inputInt;
        public readonly ReadOnlyDictionary<string, float> inputFloat;

        public MethodInput(Dictionary<string, string> inputString, Dictionary<string, bool> inputBoolean, Dictionary<string, int> inputInt, Dictionary<string, float> inputFloat)
        {
            this.inputString = new ReadOnlyDictionary<string, string>(inputString);
            this.inputBoolean = new ReadOnlyDictionary<string, bool>(inputBoolean);
            this.inputInt = new ReadOnlyDictionary<string, int>(inputInt);
            this.inputFloat = new ReadOnlyDictionary<string, float>(inputFloat);
        }
    }
}
