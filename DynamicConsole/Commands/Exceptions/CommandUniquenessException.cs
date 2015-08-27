namespace DynamicConsole.Commands.Exceptions
{
    using System;
    using System.Collections.Generic;

    using global::DynamicConsole.Commands.Signatures;

    public class CommandUniquenessException : Exception
    {
        #region Constructors

        public CommandUniquenessException(List<CommandSignature> processors)
        {
            this.Processors = processors;
        }

        public CommandUniquenessException(string message, List<CommandSignature> processors)
            : base(message)
        {
            this.Processors = processors;
        }

        #endregion

        #region Properties

        public List<CommandSignature> Processors { get; private set; }

        #endregion
    }
}