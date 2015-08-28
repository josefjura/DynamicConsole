namespace DynamicConsole.Commands.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class CommandParameterAttribute : Attribute
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

        public virtual string Id { get; }

        public int Index { get; set; }

        public virtual TypeCode Type { get; }

        public string Value { get; set; }

        #endregion
    }
}