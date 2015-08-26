namespace DynamicConsole.Commands.Signatures
{
    using Input;

    public abstract class CommandSignaturePart
    {
        #region Constructors

        public CommandSignaturePart(string signatureName)
        {
            this.SignatureName = signatureName;
        }

        #endregion

        #region Properties

        public string SignatureName { get; set; }

        #endregion

        public abstract bool CanParse(Parameter parameter);
    }
}