namespace DynamicConsole.Commands.Base
{
    using System;
    using System.Collections.Generic;

    using global::DynamicConsole.Commands.Errors;
    using global::DynamicConsole.Commands.Input;
    using global::DynamicConsole.Commands.Signatures;
    using global::DynamicConsole.IO.Base;

    public interface IEnvironmentCommand : IDisposable
    {
        #region Properties

        DynamicConsole Console { get; set; }

        string Keyword { get; set; }

        List<CommandSignature> Signatures { get; set; }

        #endregion

        bool TryRun(CommandInput ci, IOutput output, out IList<CommandError> errors);
    }
}