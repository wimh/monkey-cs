namespace libmonkey.ast
{
    public class ReturnStatement : IStatement
    {
        public ReturnStatement(IExpression expression)
        {
            Expression = expression;
        }

        public IExpression Expression { get; }

        public override string ToString()
        {
            return string.Format("return {0};", Expression);
        }
    }
}