namespace DynamicConsole.Commands.Modules.Base
{
    using System.Collections.ObjectModel;

    using global::DynamicConsole.Commands.Base;

    public abstract class ModuleBase : IModule
    {
        public ReadOnlyCollection<IConsoleCommand> Commands { get; }
    }
}