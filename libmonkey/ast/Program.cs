using System.Collections.Generic;

namespace libmonkey.ast
{
    public class Program : IProgram
    {
        private readonly List<IStatement> _statements;

        public Program()
        {
            _statements = new List<IStatement>();
        }

        public IEnumerable<IStatement> Statements => _statements;

        public void AddStatement(IStatement statement)
        {
            _statements.Add(statement);
        }
    }
}