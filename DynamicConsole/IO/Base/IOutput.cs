namespace DynamicConsole.IO.Base
{
    using System.Runtime.InteropServices;

    using global::DynamicConsole.Commands.Base;

    public interface IOutput
    {
        void Write(string text);

        void WriteLine(string text);

        void WriteHelp(IEnvironmentCommand comm);

        void WriteTable(TabularOutput table);

        void Clear();
    }
}