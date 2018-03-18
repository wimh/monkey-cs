namespace libmonkey.ast
{
    public class IntegerLiteral : IIntegerLiteral
    {
        public IntegerLiteral(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}