namespace DynamicConsole.IO
{
    using System;

    using global::DynamicConsole.Commands.Base;
    using global::DynamicConsole.IO.Base;
    using global::DynamicConsole.IO.Formatting;

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

        public void WriteHelp(IConsoleCommand comm)
        {
            Console.WriteLine($"Help for command: '{comm.Keyword?.ToUpper() ?? "Unknown"}'");
            foreach (var sig in comm.Signatures)
            {
                Console.Write("\t> ");
                Console.Write(comm.Keyword);
                Console.WriteLine(sig.GetHelp());
                Console.Write("\t  ");
                Console.WriteLine(sig.Description);
            }
        }

        public void WriteTable(TabularOutput table)
        {
            foreach (var row in table.Data)
            {
                for (int cellIndex = 0; cellIndex < row.Count; cellIndex++)
                {
                    WriteCell(row[cellIndex], table.GetColumnWidth(cellIndex));
                }
                WriteLine("");
            }
        }

        private void WriteCell(string text, int cellWidth)
        {
            var cell = $"{text.PadRight(cellWidth)}";
            Write($"{cell} ");
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}