namespace DynamicConsole.Commands.Exceptions
{
    using System;
    using System.Collections.Generic;

    using global::DynamicConsole.Commands.Attributes;

    public class MissingParametersException : Exception
    {
        private readonly List<CommandParameterAttribute> _unprocessedMandatory;

        public MissingParametersException(List<CommandParameterAttribute> unprocessedMandatory) : base()
        {
            this._unprocessedMandatory = unprocessedMandatory;
        }
    }
}