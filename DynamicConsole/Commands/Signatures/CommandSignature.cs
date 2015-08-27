namespace DynamicConsole.Commands.Signatures
{
    using System.Collections.Generic;

    using global::DynamicConsole.Commands.Errors;
    using global::DynamicConsole.Commands.Input;
    using global::DynamicConsole.IO.Base;

    public abstract class CommandSignature
    {
        #region Constructors

        public CommandSignature(string keyword, string description)
        {
            this.Keyword = keyword;
            this.Description = description;
        }

        #endregion

        #region Properties

        public string Description { get; set; }

        public string Keyword { get; set; }

        #endregion

        public abstract bool Run(CommandInput ci, IOutput output, IList<CommandError> errors);

        public abstract bool CanRun(CommandInput ci);

        public abstract string GetHelp();

        public abstract CommandInput GenerateRandomInput(string keyword);
    }
}