namespace DynamicConsole.Commands.Modules.Base
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using global::DynamicConsole.Commands.Base;

    public abstract class ModuleBase : IModule
    {
        #region Fields

        private readonly List<IEnvironmentCommand> _commands;

        private readonly DynamicConsole _console;

        private readonly IModuleRegistrar _registrar;

        #endregion

        #region Constructors

        public ModuleBase(DynamicConsole console, IModuleRegistrar registrar)
        {
            this._console = console;
            this._registrar = registrar;
            _commands = new List<IEnvironmentCommand>();
        }

        #endregion

        public ReadOnlyCollection<IEnvironmentCommand> Commands
        {
            get
            {
                return _commands.AsReadOnly();
            }
        }

        public void AddCommand<T>() where T : class, IEnvironmentCommand
        {
            var c = _registrar.ResolveCommand<T>();
            c.Console = _console;
            this._commands.Add(c);
        }

        public void AddCommand<T>(Action<IModuleRegistrar> serviceInitialization) where T : class, IEnvironmentCommand
        {
            serviceInitialization(_registrar);
            this.AddCommand<T>();
        }
    }
}