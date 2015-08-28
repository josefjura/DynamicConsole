namespace DynamicConsole
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;

    using global::DynamicConsole.Commands.Base;
    using global::DynamicConsole.Commands.Exceptions;
    using global::DynamicConsole.Commands.Input;
    using global::DynamicConsole.Commands.Modules;
    using global::DynamicConsole.Commands.Modules.Base;
    using global::DynamicConsole.IO.Base;

    using Microsoft.Practices.Unity;

    public class DynamicConsole : IDisposable
    {
        public delegate void ConsoleActionCallback(IOutput output, CommandInput ci);

        public delegate void ConsoleActionCallback<in T>(IOutput output, CommandInput ci, T parameter);

        #region Fields

        private readonly List<IModule> _modules;

        protected IModuleRegistrar _registrar;

        #endregion

        #region Constructors

        public DynamicConsole(
            string prompt,
            IOutput output,
            ConsoleActionCallback<IEnvironmentCommand> foundCommand,
            ConsoleActionCallback unknownCommand)
        {
            this.Output = output;
            this.Prompt = prompt;
            this.FoundCommand = foundCommand;
            this.UnknownCommand = unknownCommand;

            var uc = new UnityContainer();
            uc.RegisterInstance(this, new PerThreadLifetimeManager());

            _registrar = new UnityRegistrar(uc);
            _modules = new List<IModule>();
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<IEnvironmentCommand> Commands
        {
            get
            {
                return _modules.SelectMany(x => x.Commands).ToList().AsReadOnly();
            }
        }

        public ConsoleActionCallback<IEnvironmentCommand> FoundCommand { get; set; }

        public bool IsExiting { get; set; }

        public ReadOnlyCollection<IModule> Modules
        {
            get
            {
                return _modules.AsReadOnly();
            }
        }

        public IOutput Output { get; }

        public string Prompt { get; set; }

        public ConsoleActionCallback UnknownCommand { get; set; }

        #endregion

        public void Dispose()
        {
            foreach (var c in this.Commands)
            {
                c.Dispose();
            }
        }

        public void AddModule(IModule module)
        {
            this._modules.Add(module);
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
                    this.FoundCommand(this.Output, input, exec);
                }
                else
                {
                    throw new ApplicationException($"More commands with same name: {input.Keyword}");
                }
            }
            else
            {
                this.UnknownCommand(this.Output, input);
            }
        }

        public virtual void CloseConsole()
        {
            this.Output.WriteLine("Console ending");
            this.Dispose();
        }

        public virtual void StartConsole()
        {
            this.Output.WriteLine("Console starting");
            this.Output.WriteLine("");
        }

        public void EnsureSignatures()
        {
            foreach (var command in Commands)
            {
                foreach (var signature in command.Signatures)
                {
                    var random = signature.GenerateRandomInput(command.Keyword);

                    Debug.WriteLine(random);
                    var processors = Commands.SelectMany(x => x.Signatures).Where(x => x.CanRun(random)).ToList();

                    if (processors.Count > 1)
                    {
                        throw new CommandUniquenessException("EnsureSignatures", processors);
                    }
                }
            }
        }
    }
}