namespace DynamicConsole.Commands.Exceptions
{
    using System;

    using global::DynamicConsole.Commands.Input;

    public class UnidentifiedParameterException : Exception
    {
        public Parameter Parameter { get; set; }

        public UnidentifiedParameterException(Parameter par) : base("Parameter couldn\'t be identified")
        {
            this.Parameter = par;
        }
    }
}