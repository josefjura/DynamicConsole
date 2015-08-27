namespace DynamicConsole.Commands.Base
{
    using System.Collections.Generic;

    using Errors;
    using Input;
    using Signatures;
    using IO.Base;

    public abstract class BaseCommand : IEnvironmentCommand
    {
        #region Constructors

        public BaseCommand(string keyword)
        {
            this.Keyword = keyword;
            this.Signatures = new List<CommandSignature>();
        }

        #endregion

        public virtual void Dispose()
        {
        }

        public DynamicConsole Console { get; set; }

        public string Keyword { get; set; }

        public virtual bool TryRun(CommandInput ci, IOutput output, out IList<CommandError> errors)
        {
            errors = new List<CommandError>();

            foreach (var sig in this.Signatures)
            {
                if (sig.CanRun(ci))
                {
                    return sig.Run(ci, output, errors);
                }
            }

            errors.Add(new CommandError("Unknown parameters", "Current parameters can't be parsed"));
            return false;
        }

        public virtual void AccessCache(Dictionary<string, object> cache)
        {
        }

        public List<CommandSignature> Signatures { get; set; }
    }
}