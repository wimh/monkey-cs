namespace libmonkey.ast
{
    public class Identifier : IIdentifier
    {
        public Identifier(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }
    }
}