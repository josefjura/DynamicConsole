namespace DynamicConsole.Commands.Base
{
    using System;
    using System.Collections.Generic;

    using Errors;
    using Input;
    using Signatures;
    using IO.Base;

    public interface IEnvironmentCommand : IDisposable
    {
        #region Properties

        DynamicConsole Console { get; set; }

        string Keyword { get; set; }

        List<CommandSignature> Signatures { get; set; }

        #endregion

        bool TryRun(CommandInput ci, IOutput output, out IList<CommandError> errors);

        void AccessCache(Dictionary<string, object> cache);
    }
}