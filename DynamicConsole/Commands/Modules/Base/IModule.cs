namespace DynamicConsole.Commands.Modules.Base
{
    using System;
    using System.Collections.ObjectModel;

    using global::DynamicConsole.Commands.Base;

    public interface IModule
    {
        #region Properties

        ReadOnlyCollection<IConsoleCommand> Commands { get; }

        #endregion

        void AddCommand<T>() where T : class, IConsoleCommand;

        void AddCommand<T>(Action<IModuleRegistrar> serviceInitialization) where T : class, IConsoleCommand;
    }
}