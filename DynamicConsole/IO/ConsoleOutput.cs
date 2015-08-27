namespace DynamicConsole.IO
{
    using System;

    using global::DynamicConsole.Commands.Base;
    using global::DynamicConsole.IO.Base;

    public class ConsoleOutput : IOutput
    {
        public void Write(string text)
        {
            Console.Write(text);
        }

        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteHelp(IEnvironmentCommand comm)
        {
            Console.WriteLine($"Help for command: '{comm.Keyword?.ToUpper() ?? "Unknown"}'");
            foreach (var sig in comm.Signatures)
            {
                Console.Write("\t-");
                Console.Write(comm.Keyword);
                Console.WriteLine(sig.GetHelp());
                Console.Write("\t  ");
                Console.WriteLine(sig.Description);
            }
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}