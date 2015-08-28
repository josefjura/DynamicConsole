namespace DynamicConsole.Commands.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public class CommandParameterAttribute : Attribute
    {
        private string _id;

        private TypeCode _type;

        #region Constructors

        // This is a positional argument
        public CommandParameterAttribute(string id, TypeCode typeCode)
        {
            this._id = id;
            this._type = typeCode;
        }

        #endregion

        #region Properties

        public virtual TypeCode Type => this._type;

        public virtual string Id => this._id;

        public int Index { get; set; }


        public string Value { get; set; }

        #endregion
    }
}