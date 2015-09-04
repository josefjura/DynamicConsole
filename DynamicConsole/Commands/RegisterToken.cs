namespace DynamicConsole.Commands
{
    using System;
    using System.Collections.Generic;

    using global::DynamicConsole.Commands.Base;
    using global::DynamicConsole.Commands.Modules.Base;

    public class RegisterToken
    {
        #region Fields

        private readonly List<IConsoleCommand> _commands;

        private readonly IModuleRegistrar _registrar;

        #endregion

        #region Constructors

        public RegisterToken(IModuleRegistrar registrar)
        {
            this._registrar = registrar;

            _commands = new List<IConsoleCommand>();
        }

        #endregion

        public RegisterToken RegisterCommand<T>() where T : class, IConsoleCommand
        {
            var c = _registrar.ResolveCommand<T>();
            this._commands.Add(c);
            return this;
        }

        public RegisterToken RegisterCommand<T>(Action<IModuleRegistrar> serviceInitialization)
            where T : class, IConsoleCommand
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
}