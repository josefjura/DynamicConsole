namespace DynamicConsole.Commands.Modules.Base
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using global::DynamicConsole.Commands.Base;

    public abstract class ModuleBase : IModule
    {
        #region Fields

        private readonly List<IConsoleCommand> _commands;

        private readonly IModuleRegistrar _registrar;

        #endregion

        #region Constructors

        public ModuleBase(IModuleRegistrar registrar)
        {
            this._registrar = registrar;
            _commands = new List<IConsoleCommand>();
        }

        #endregion

        public ReadOnlyCollection<IConsoleCommand> Commands
        {
            get
            {
                return _commands.AsReadOnly();
            }
        }

        public void AddCommand<T>() where T : class, IConsoleCommand
        {
            var c = _registrar.ResolveCommand<T>();
            this._commands.Add(c);
        }

        public void AddCommand<T>(Action<IModuleRegistrar> serviceInitialization) where T : class, IConsoleCommand
        {
            serviceInitialization(_registrar);
            this.AddCommand<T>();
        }
    }
}