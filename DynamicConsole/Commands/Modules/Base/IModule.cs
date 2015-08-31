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
    }
}