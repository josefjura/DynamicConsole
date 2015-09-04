namespace DynamicConsole.Commands.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using global::DynamicConsole.Commands.Attributes;

    public class AmbiguousParameterException : Exception
    {
        public AmbiguousParameterException(List<KeyValuePair<PropertyInfo, CommandParameterAttribute>> result) : base()
        {

        }
    }
}