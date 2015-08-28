namespace DynamicConsole.Commands.Modules
{
    using System;
    using System.Linq;

    using global::DynamicConsole.Commands.Base;
    using global::DynamicConsole.Commands.Modules.Base;

    using Microsoft.Practices.Unity;

    public class UnityRegistrar : IModuleRegistrar
    {
        #region Fields

        private readonly UnityContainer _container;

        #endregion

        #region Constructors

        public UnityRegistrar()
        {
            this._container = new UnityContainer();
        }

        #endregion

        public void RegisterService<TIntf, TImpl>() where TImpl : TIntf
        {
            this._container.RegisterType<TIntf, TImpl>(new PerThreadLifetimeManager());
        }

        public void RegisterService<TIntf, TImpl>(Func<TImpl> constructor) where TImpl : TIntf
        {
            if (!this.IsRegistered<TIntf>())
            {
                this._container.RegisterInstance<TIntf>(constructor(), new PerThreadLifetimeManager());
            }
        }

        public T ResolveCommand<T>() where T : class, IEnvironmentCommand
        {
            return this._container.Resolve<T>();
        }

        public void RegisterService<TIntf, TImpl>(TImpl value) where TImpl : TIntf
        {
            if (!this.IsRegistered<TIntf>())
            {
                this._container.RegisterInstance<TIntf>(value, new PerThreadLifetimeManager());
            }
        }

        private bool IsRegistered<TIntf>()
        {
            return this._container.Registrations.Any(x => x.RegisteredType == typeof(TIntf));
        }
    }
}