using System.IO;
using libmonkey.lexer;
using libmonkey.token;

namespace libmonkey.repl
{
    public class Repl
    {
        private const string Prompt = ">> ";
        
        public void Start(StreamReader input, StreamWriter output)
        {
            while (true)
            {
                output.Write(Prompt);
                output.Flush();
                var line = input.ReadLine();

                var lexer = new Lexer(line);
                
                for(var token=lexer.NextToken();token.Type!=Token.Tokens.Eof;token=lexer.NextToken())
                {
                    output.WriteLine("{0}", token);
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}