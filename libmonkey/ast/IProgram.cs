using System.Collections.Generic;

namespace libmonkey.ast
{
    public interface IProgram
    {
        IEnumerable<IStatement> Statements { get; }
    }
}