
using System;
using GitHub.Awesome.Infra.Common.IoC.Enum;
using Xamarin.Forms;

namespace GitHub.Awesome.Infra.Common.IoC
{
    /// <summary>
    /// Simple representation of IoC container.
    /// </summary>
    public class AppIoC : IoCSetup
    {
        #region Fields

        private static IoCSetup _container;
        private static bool _isInternal;

        #endregion

        #region Properties

        /// <summary>
        /// Instance of IoCSetup.
        /// </summary>
        public static IoCSetup Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new AppIoC();
                    _isInternal = true;
                }

                return _container;
            }
        }

        #endregion

        #region Constructor

        private AppIoC() { }

        #endregion

        #region Methods

        /// <summary>
        /// Method to help inject the another container implementation.
        /// </summary>
        /// <param name="container">IoCSetup container.</param>
        public static void SetContainer(IoCSetup container)
        {
            _container = container;
        }

        /// <summary>
        /// Register a new service.
        /// </summary>
        /// <typeparam name="TService">Service.</typeparam>
        /// <typeparam name="TImpl">Implementation class.</typeparam>
        public void Register<TService, TImpl>()
            where TImpl : class
        {
            if (_isInternal)
                DependencyService.Register<TImpl>();
            else
                _container.Register<TService, TImpl>();
        }

        /// <summary>
        /// Register a new service.
        /// </summary>
        /// <param name="service">Type of Service.</param>
        /// <param name="impl">Type of implementation class.</param>
        /// <param name="instance">Instance of implementation class.</param>
        /// <param name="lifeCycle">Lifecycle.</param>
        public void Register(Type service, Type impl, object instance, EDependencyLifeCycle lifeCycle)
        {
            if (!_isInternal)
                _container.Register(service, impl, instance, lifeCycle);
        }

        /// <summary>
        /// Register a new service.
        /// </summary>
        /// <typeparam name="TService">Service.</typeparam>
        /// <typeparam name="TImpl">Implementation class.</typeparam>
        /// <param name="instance">Instance of implementation class.</param>
        /// <param name="lifeCycle">Lifecycle.</param>
        public void Register<TService, TImpl>(object instance, EDependencyLifeCycle lifeCycle)
            where TService : class
            where TImpl : class, TService
        {
            if (_isInternal)
                DependencyService.Register<TService, TImpl>();
            else
                _container.Register<TService, TImpl>();
        }

        /// <summary>
        /// Get TService instance.
        /// </summary>
        /// <typeparam name="TService">Service.</typeparam>
        /// <returns>TService instance.</returns>
        public TService Resolve<TService>()
            where TService : class
        {
            if (_isInternal)
                return DependencyService.Get<TService>(DependencyFetchTarget.GlobalInstance);
            else
                return _container.Resolve<TService>();
        }

        #endregion-
    }
}

