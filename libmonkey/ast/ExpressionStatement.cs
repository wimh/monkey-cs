namespace libmonkey.ast
{
    public class ExpressionStatement : IStatement
    {
        public ExpressionStatement(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression { get; }

        public override string ToString()
        {
            return string.Format("({0})", Expression);
        }
    }
}