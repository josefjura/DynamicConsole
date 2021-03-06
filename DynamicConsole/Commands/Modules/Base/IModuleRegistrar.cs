namespace DynamicConsole.Commands.Modules.Base
{
    using System;

    using global::DynamicConsole.Commands.Base;

    using Microsoft.Practices.Unity;

    public interface IModuleRegistrar
    {
        void RegisterService<TIntf, TImpl>() where TImpl : TIntf;

        void RegisterService<TIntf, TImpl>(Func<TImpl> constructor) where TImpl : TIntf;

        T ResolveCommand<T>() where T : class, IConsoleCommand;

        void RegisterInstance<T>(T dynamicConsole, LifetimeManager perThreadLifetimeManager);
    }
}