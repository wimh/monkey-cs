using System.IO;
using libmonkey.lexer;

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

                foreach (var token in lexer.Tokens)
                {
                    output.WriteLine("{0}", token);
                }
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}