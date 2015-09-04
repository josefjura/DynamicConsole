namespace DynamicConsole.Commands.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class CommandTokenAttribute : CommandParameterAttribute
    {
        #region Constructors

        public CommandTokenAttribute(int index, string value)
            : base("token")
        {
            this.Value = value;
            this.Index = index;
            this.IsMandatory = true;
        }

        #endregion
    }
}