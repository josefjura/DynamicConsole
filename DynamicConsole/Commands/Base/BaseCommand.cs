namespace DynamicConsole.Commands.Base
{
    using System.Collections.Generic;

    using global::DynamicConsole.Commands.Errors;
    using global::DynamicConsole.Commands.Input;
    using global::DynamicConsole.Commands.Modules.Base;
    using global::DynamicConsole.Commands.Signatures;
    using global::DynamicConsole.IO.Base;

    public abstract class BaseCommand : IConsoleCommand
    {
        #region Fields

        protected readonly DynamicConsole _console;

        #endregion

        #region Constructors

        public BaseCommand(string keyword, DynamicConsole console)
        {
            this._console = console;
            this.Keyword = keyword;
            this.Signatures = new List<CommandSignature>();
        }

        #endregion

        public virtual void Dispose()
        {
        }

        public string Keyword { get; set; }

        public IModule Module { get; set; }

        public virtual bool TryRun(CommandInput ci, IOutput output, out IList<CommandError> errors)
        {
            errors = new List<CommandError>();

            foreach (var sig in this.Signatures)
            {
                if (sig.CanRun(ci))
                {
                    return sig.Run(ci, errors);
                }
            }

            errors.Add(new CommandError("Unknown parameters", "Current parameters can't be parsed"));
            return false;
        }

        public List<CommandSignature> Signatures { get; set; }
    }
}