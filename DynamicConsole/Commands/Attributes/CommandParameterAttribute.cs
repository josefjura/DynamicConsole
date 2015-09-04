namespace DynamicConsole.Commands.Attributes
{
    using System;
    using System.Net.Http.Headers;
    using System.Reflection;

    using global::DynamicConsole.Commands.Input;

    [AttributeUsage(AttributeTargets.Property)]
    public class CommandParameterAttribute : Attribute
    {
        #region Constructors

        // This is a positional argument
        public CommandParameterAttribute(string id, TypeCode typeCode = TypeCode.String)
        {
            this.Id = id;
            this.Type = typeCode;
            this.IsMandatory = false;
        }

        #endregion

        #region Properties

        public virtual string Id { get; }

        public int Index { get; set; } = -1;

        public virtual TypeCode Type { get; }

        public string Value { get; set; }

        public bool IsMandatory { get; set; }


        #endregion

        public virtual void Process<T>(T instance, PropertyInfo key, Parameter par) where T : class, new()
        {
            key.SetValue(instance, Convert.ChangeType(par.Value, this.Type));
        }
    }
}