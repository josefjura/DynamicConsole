namespace DynamicConsole.IO.Base
{
    using System.Runtime.InteropServices;

    using global::DynamicConsole.Commands.Base;
    using global::DynamicConsole.IO.Formatting;

    public interface IOutput
    {
        void Write(string text);

        void WriteLine(string text);

        void WriteHelp(IConsoleCommand comm);

        void WriteTable(TabularOutput table);

        void Clear();
    }
}