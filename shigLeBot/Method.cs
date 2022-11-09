namespace shigLeBot
{
    internal class Method
    {
        public readonly string id;
        public readonly string opcode;
        public readonly string next;
        public readonly MethodInput input;

        public Method(string id, string opcode, string next, MethodInput input)
        {
            this.id = id;
            this.opcode = opcode;
            this.next = next;
            this.input = input;
        }
    }
}
