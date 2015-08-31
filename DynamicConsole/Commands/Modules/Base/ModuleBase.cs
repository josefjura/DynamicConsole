namespace DynamicConsole.Commands.Modules.Base
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using global::DynamicConsole.Commands.Base;

    public abstract class ModuleBase : IModule
    {
        public ReadOnlyCollection<IConsoleCommand> Commands { get; }
    }
}