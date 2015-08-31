namespace DynamicConsole.IO
{
    using System;
    using System.Linq;

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
            TabularOutput to = new TabularOutput(77);
            foreach (var sig in comm.Signatures)
            {
                to.AddRow(comm.Keyword, sig.GetHelp(), ":", sig.Description);
            }

            WriteTable(to);
        }

        public void WriteTable(TabularOutput table)
        {
            foreach (var row in table.Data)
            {
                for (int cellIndex = 0; cellIndex < row.Count; cellIndex++)
                {
                    var colWidth = table.GetColumnWidth(cellIndex);
                    var colStart = table.GetColumnStart(cellIndex);
                    var realColWidth = colStart + colWidth > Console.BufferWidth ? Console.BufferWidth - colStart : colWidth;
                    WriteCell(row[cellIndex], colStart, realColWidth);
                }
                WriteLine("");
            }
        }

        private void WriteCell(string text, int startWidth, int cellWidth)
        {
            int written = 0;
            while (written < text.Length)
            {
                var word = text.Skip(written).Take(cellWidth).ToList();
                written += word.Count();
                var realText = new string(word.ToArray());
                var cell = $"{realText.PadRight(cellWidth)}";
                WriteFromPosition(startWidth, $"{cell} ");
            }
        }

        private void WriteFromPosition(int position, string text)
        {
            Console.CursorLeft = position;
            Write(text);
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}