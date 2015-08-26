namespace DynamicConsole.Commands.Signatures
{
    using System.Collections.Generic;

    using Errors;
    using Input;
    using IO.Base;

    public abstract class CommandSignature
    {
        #region Constructors

        public CommandSignature(string description)
        {
            this.Description = description;
        }

        #endregion

        #region Properties

        public string Description { get; set; }

        #endregion

        public abstract bool Run(CommandInput ci, IOutput output, IList<CommandError> errors);

        public abstract bool CanRun(CommandInput ci);

        public abstract string GetHelp();
    }
}