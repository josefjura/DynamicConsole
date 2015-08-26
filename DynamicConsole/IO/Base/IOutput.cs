namespace DynamicConsole.IO.Base
{
    using Commands.Base;

    public interface IOutput
    {
        void Write(string text);

        void WriteLine(string text);

        void WriteHelp(IEnvironmentCommand comm);

        void Clear();
    }
}