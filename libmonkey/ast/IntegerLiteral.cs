using libmonkey.token;

namespace libmonkey.ast
{
    public class IntegerLiteral : IIntegerLiteral
    {
        private readonly Token _token;

        public IntegerLiteral(Token token)
        {
            _token = token;
        }

        public int Value => int.Parse(_token.Literal);

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}