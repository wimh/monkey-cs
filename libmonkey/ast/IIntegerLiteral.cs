namespace libmonkey.ast
{
    public interface IIntegerLiteral: IExpression
    {
        int Value { get; }
    }
}