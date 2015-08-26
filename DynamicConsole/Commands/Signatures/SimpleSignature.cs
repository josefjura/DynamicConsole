namespace DynamicConsole.Commands.Signatures
{
    using System.Collections.Generic;
    using System.Text;

    using Errors;
    using Input;
    using IO.Base;

    public class SimpleSignature : CommandSignature
    {
        public delegate bool CommandSignatureHitCallback(CommandInput ci, IOutput output, IList<CommandError> errors);

        #region Fields

        private readonly CommandSignatureHitCallback _callback;

        #endregion

        #region Constructors

        public SimpleSignature(string description, CommandSignatureHitCallback callback)
            : base(description)
        {
            this._callback = callback;
            this.Parts = new List<CommandSignaturePart>();
        }

        #endregion

        #region Properties

        public int ParameterCount => this.Parts.Count;

        public List<CommandSignaturePart> Parts { get; set; }

        #endregion

        public override bool Run(CommandInput ci, IOutput output, IList<CommandError> errors)
        {
            return this._callback?.Invoke(ci, output, errors) ?? false;
        }

        public override bool CanRun(CommandInput ci)
        {
            if (ci.IndexParameters.Count != this.ParameterCount)
            {
                return false;
            }

            for (var i = 0; i < ci.IndexParameters.Count; i++)
            {
                if (!this.Parts[i].CanParse(ci.IndexParameters[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override string GetHelp()
        {
            var sb = new StringBuilder();
            foreach (var p in this.Parts)
            {
                sb.Append($" {p}");
            }
            return sb.ToString();
        }
    }
}