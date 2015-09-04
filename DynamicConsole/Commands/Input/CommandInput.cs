namespace DynamicConsole.Commands.Input
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text.RegularExpressions;

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
            string pattern = "-(?<name>(?:(?![-\" :]).)+):\"(?<value>(?:(?![-\":]).)+)\"|-(?<name>(?:(?![-\" :]).)+):(?<value>(?:(?![-\":]).)+)|-(?<name>(?:(?![-\" :]).)+)|(?<value>(?:(?![-\" :]).)+)";

            var regex = new Regex(pattern);
            var result = regex.Matches(input);
            int i = 0;
            foreach (Match match in result)
            {
                if (i == 0)
                    ci.Keyword = match.Value;
                else
                {
                    var nameGroup = match.Groups["name"];
                    var name = nameGroup.Success ? nameGroup.Value.Trim() : null;
                    var valueGroup = match.Groups["value"];
                    var value = valueGroup.Success ? valueGroup.Value.Trim() : null;
                    ci.Parameters.Add(new Parameter { Index = name == null ? i - 1 : -1, Name = name, Value = value });
                }
                i++;
            }

            return ci;
        }

        public override string ToString()
        {
            var indexParams = string.Join(" ", this.IndexParameters.Select(s => s.Value));
            var namedParams = string.Join(" ", this.NamedParameters.Select(x => $"-{x.Name} {x.Value}"));

            return $"{this.Keyword} {indexParams} {namedParams}";
        }
    }
}