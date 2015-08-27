namespace DynamicConsole
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using global::DynamicConsole.Commands.Base;
    using global::DynamicConsole.Commands.Input;
    using global::DynamicConsole.IO.Base;

    public class DynamicConsole : IDisposable
    {
        public delegate void ConsoleActionCallback(IOutput output, CommandInput ci);

        public delegate void ConsoleActionCallback<in T>(IOutput output, CommandInput ci, T parameter);

        #region Fields

        private readonly IOutput _output;

        #endregion

        #region Constructors

        public DynamicConsole(
            string prompt,
            IOutput output,
            ConsoleActionCallback<IEnvironmentCommand> foundCommand,
            ConsoleActionCallback unknownCommand)
        {
            this._output = output;
            this._commands = new List<IEnvironmentCommand>();
            this.Prompt = prompt;
            this.FoundCommand = foundCommand;
            this.UnknownCommand = unknownCommand;
        }

        #endregion

        #region Properties

        public Dictionary<string, object> Cache { get; set; }

        public List<IEnvironmentCommand> _commands;

        public ReadOnlyCollection<IEnvironmentCommand> Commands
        {
            get
            {
                return _commands.AsReadOnly();
            }
        }


        public ConsoleActionCallback<IEnvironmentCommand> FoundCommand { get; set; }

        public bool IsExiting { get; set; }

        public string Prompt { get; set; }

        public ConsoleActionCallback UnknownCommand { get; set; }

        #endregion

        public void Dispose()
        {
            foreach (var c in this.Commands)
            {
                c.Dispose();
            }
            this.Cache = null;
        }

        private void InitCache()
        {
            this.Cache = new Dictionary<string, object>();
            foreach (var c in this.Commands)
            {
                c.AccessCache(this.Cache);
            }
        }

        public void AddCommand(IEnvironmentCommand comm)
        {
            this._commands.Add(comm);
            comm.Console = this;
        }

        public void ProcessInput(CommandInput input, bool findSimilar)
        {
            if (this.Commands.Any(x => string.IsNullOrWhiteSpace(x.Keyword)))
            {
                throw new ApplicationException("Some commands have no keyword set.");
            }

            var commands = this.Commands.Where(x => x.Keyword == input.Keyword).ToList();

            if (commands.Any())
            {
                if (commands.Count == 1)
                {
                    var exec = commands.Single();
                    this.FoundCommand(this._output, input, exec);
                }
                else
                {
                    throw new ApplicationException($"More commands with same name: {input.Keyword}");
                }
            }
            else
            {
                this.UnknownCommand(this._output, input);
            }
        }

        public void CloseConsole()
        {
            this._output.WriteLine("Console ending");
            this.Dispose();
        }

        public void StartConsole()
        {
            this._output.WriteLine("Console starting");
            this._output.Write("Precaching . . ");
            this.InitCache();
            this._output.WriteLine("Done");
            this._output.WriteLine("");
        }
    }
}