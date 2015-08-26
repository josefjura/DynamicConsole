namespace DynamicConsole.Commands.Signatures
{
    using System;

    using Input;

    public class TypeSignaturePart : CommandSignaturePart
    {
        #region Fields

        private readonly TypeCode _checkType;

        #endregion

        #region Constructors

        public TypeSignaturePart(string name, TypeCode checkType)
            : base(name)
        {
            this._checkType = checkType;
        }

        #endregion

        public override bool CanParse(Parameter parameter)
        {
            try
            {
                var result = Convert.ChangeType(parameter.Value, this._checkType);
                return true;
            }
            catch (InvalidCastException)
            {
                return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"<{this.SignatureName}:{this._checkType}>";
        }
    }
}