namespace DynamicConsole.Commands.Signatures
{
    using System.Collections.Generic;
    using System.Text;

    using global::DynamicConsole.Commands.Errors;
    using global::DynamicConsole.Commands.Input;

    public class SimpleSignature : CommandSignature
    {
        public delegate bool CommandSignatureHitCallback(CommandInput ci, IList<CommandError> errors);

        #region Fields

        private readonly CommandSignatureHitCallback _callback;

        #endregion

        #region Constructors

        public SimpleSignature(string keyword, string description, CommandSignatureHitCallback callback)
            : base(keyword, description)
        {
            this._callback = callback;
            this.Parts = new List<CommandSignaturePart>();
        }

        #endregion

        #region Properties

        public int ParameterCount => this.Parts.Count;

        public List<CommandSignaturePart> Parts { get; set; }

        #endregion

        public override bool Run(CommandInput ci, IList<CommandError> errors)
        {
            return this._callback?.Invoke(ci, errors) ?? false;
        }

        public override bool CanRun(CommandInput ci)
        {
            if (ci.Keyword != Keyword)
            {
                return false;
            }

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

        public override CommandInput GenerateRandomInput(string keyword)
        {
            var toReturn = new CommandInput();

            toReturn.Keyword = keyword;

            var i = 0;
            foreach (var part in Parts)
            {
                toReturn.Parameters.Add(new Parameter { Value = part.GenerateInput(), Index = i++ });
            }

            return toReturn;
        }
    }
}