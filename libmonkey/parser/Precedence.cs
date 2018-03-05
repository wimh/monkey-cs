namespace libmonkey.parser
{
    public enum Precedence
    {
        Lowest,
        Equals,         // ==
        LessGreater,    // > or <
        Sum,            // +
        Product,        // *
        Prefix,         // -x or !x
        Call            // fn(x)
    }
}