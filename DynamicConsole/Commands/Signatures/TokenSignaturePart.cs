namespace DynamicConsole.Commands.Signatures
{
    using Input;

    public class TokenSignaturePart : CommandSignaturePart
    {
        #region Fields

        private readonly string _token;

        #endregion

        #region Constructors

        public TokenSignaturePart(string token)
            : base("TOKEN")
        {
            this._token = token;
        }

        #endregion

        public override bool CanParse(Parameter parameter)
        {
            return parameter.Value == this._token;
        }

        public override string ToString()
        {
            return $"\"{this._token}\">";
        }
    }
}