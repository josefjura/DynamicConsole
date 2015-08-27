namespace DynamicConsole.Commands.Input
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::DynamicConsole.Commands.Errors;

    public class CommandInput
    {
        #region Constructors

        public CommandInput()
        {
            this.Parameters = new List<Parameter>();
            this.Errors = new List<CommandError>();
        }

        #endregion

        #region Properties

        public IList<CommandError> Errors { get; set; }

        public IList<Parameter> IndexParameters
        {
            get
            {
                return this.Parameters.Where(x => !x.IsNamed).ToList();
            }
        }

        public string Keyword { get; set; } = string.Empty;

        public IList<Parameter> NamedParameters
        {
            get
            {
                return this.Parameters.Where(x => x.IsNamed).ToList();
            }
        }

        public IList<Parameter> Parameters { get; set; }

        #endregion

        public static CommandInput Parse(string input)
        {
            var ci = new CommandInput();
            var tokens = input.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Any())
            {
                ci.Keyword = tokens.First();
                ParseParameters(ci, tokens.Skip(1));
            }

            return ci;
        }

        private static void ParseParameters(CommandInput ci, IEnumerable<string> parameters, int index = 0)
        {
            var paramsList = parameters as IList<string> ?? parameters.ToList();
            if (!paramsList.Any())
            {
                return;
            }

            var curr = paramsList.First();
            var processed = 0;
            if (curr.StartsWith("-"))
            {
                if (paramsList.Count > 1)
                {
                    ProcessNamedParameter(ci, index, paramsList.First(), paramsList[1]);
                    processed = 2;
                }
                else
                {
                    ProcessNamedParameter(ci, index, paramsList.First());
                    processed = 1;
                }
            }
            else
            {
                ProcessIndexedParameter(ci, index, paramsList.First());
                processed = 1;
            }

            ParseParameters(ci, paramsList.Skip(processed), index + processed);
        }

        private static void ProcessNamedParameter(CommandInput ci, int index, string name, string value)
        {
            ci.Parameters.Add(new Parameter { Index = index, Name = name.Replace("-", ""), Value = value });
        }

        private static void ProcessIndexedParameter(CommandInput ci, int index, string value)
        {
            ci.Parameters.Add(new Parameter { Index = index, Value = value });
        }

        private static void ProcessNamedParameter(CommandInput ci, int index, string name)
        {
            ci.Errors.Add(
                new CommandError("Parameter value missing", $"Missing value for parameter {name.Replace("-", "")}"));
        }

        public override string ToString()
        {
            var indexParams = string.Join(" ", this.IndexParameters.Select(s => s.Value));
            var namedParams = string.Join(" ", this.NamedParameters.Select(x => $"-{x.Name} {x.Value}"));

            return $"{this.Keyword} {indexParams} {namedParams}";
        }
    }
}