using libmonkey.token;

namespace libmonkey.ast
{
    public class Identifier : IIdentifier
    {
        private Token _token;

        public Identifier(Token token)
        {
            _token = token;
        }

        public string Value => _token.Literal;

        public override string ToString()
        {
            return Value;
        }
    }
}