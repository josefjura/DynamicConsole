namespace DynamicConsole.Commands
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using global::DynamicConsole.Commands.Base;
    using global::DynamicConsole.Commands.Modules.Base;

    public class SimpleModule : IModule
    {
        #region Fields

        private List<IConsoleCommand> _commands;

        #endregion

        #region Constructors

        public SimpleModule(string name)
        {
            this.Name = name;
            this._commands = new List<IConsoleCommand>();
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        #endregion

        public ReadOnlyCollection<IConsoleCommand> Commands
        {
            get
            {
                return this._commands.AsReadOnly();
            }
        }

        public void AddCommands(List<IConsoleCommand> commands)
        {
            this._commands = commands;
            foreach (var consoleCommand in commands)
            {
                consoleCommand.Module = this;
            }
        }
    }
}