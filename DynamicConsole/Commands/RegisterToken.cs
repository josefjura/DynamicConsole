using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicConsole.Commands
{
    using System.Collections.ObjectModel;

    using global::DynamicConsole.Commands.Base;
    using global::DynamicConsole.Commands.Modules;
    using global::DynamicConsole.Commands.Modules.Base;

    public class RegisterToken
    {
        private readonly List<IConsoleCommand> _commands;

        private readonly IModuleRegistrar _registrar;

        public RegisterToken(IModuleRegistrar registrar)
        {
            this._registrar = registrar;

            _commands = new List<IConsoleCommand>();
        }

        public RegisterToken RegisterCommand<T>() where T : class, IConsoleCommand
        {
            var c = _registrar.ResolveCommand<T>();
            this._commands.Add(c);
            return this;
        }

        public RegisterToken RegisterCommand<T>(Action<IModuleRegistrar> serviceInitialization) where T : class, IConsoleCommand
        {
            serviceInitialization(_registrar);
            this.RegisterCommand<T>();
            return this;
        }

        public IModule AsModule(string name)
        {
            var module = new SimpleModule(name);
            module.AddCommands(_commands);
            return module;
        }

        public IEnumerable<IConsoleCommand> GetCurrentCommands()
        {
            return _commands;
        }
    }

    public class SimpleModule : IModule
    {
        private List<IConsoleCommand> _commands;

        public string Name { get; set; }

        public SimpleModule(string name)
        {
            this.Name = name;
            _commands = new List<IConsoleCommand>();
        }

        public void AddCommands(List<IConsoleCommand> commands)
        {
            this._commands = commands;
            foreach (var consoleCommand in commands)
            {
                consoleCommand.Module = this;
            }
        }

        public ReadOnlyCollection<IConsoleCommand> Commands
        {
            get
            {
                return _commands.AsReadOnly();
            }
        }
    }
}
