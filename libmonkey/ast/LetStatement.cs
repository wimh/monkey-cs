namespace libmonkey.ast
{
    public class LetStatement : IStatement
    {
        public LetStatement(IIdentifier identifier, IExpression expression)
        {
            Identifier = identifier;
            Expression = expression;
        }

        public IIdentifier Identifier { get; }

        public IExpression Expression { get; }

        public override string ToString()
        {
            return string.Format("let {0} = {1};", Identifier, Expression);
        }
    }
}