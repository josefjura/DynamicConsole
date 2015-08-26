namespace DynamicConsole.Commands.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class CommandParameterAttribute : Attribute
    {
        #region Constructors

        // This is a positional argument
        public CommandParameterAttribute(string id, TypeCode typeCode)
        {
            this.Id = id;
            this.Type = typeCode;
        }

        #endregion

        #region Properties

        public string Id { get; set; }

        public int Index { get; set; }

        public TypeCode Type { get; set; }

        public string Value { get; set; }

        #endregion
    }
}