namespace DynamicConsole.IO.Base
{
    using global::DynamicConsole.Commands.Base;

    public interface IOutput
    {
        void Write(string text);

        void WriteLine(string text);

        void WriteHelp(IEnvironmentCommand comm);

        void Clear();
    }
}