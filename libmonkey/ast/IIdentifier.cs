namespace libmonkey.ast
{
    public interface IIdentifier: IExpression
    {
        string Value { get; }
    }
}