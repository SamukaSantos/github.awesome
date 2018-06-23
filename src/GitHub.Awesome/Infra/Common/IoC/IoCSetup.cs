
using GitHub.Awesome.Infra.Common.IoC.Enum;
using System;

namespace GitHub.Awesome.Infra.Common.IoC
{
    /// <summary>
    /// Simple representation of IoC container.
    /// </summary>
    public interface IoCSetup 
    {
        /// <summary>
        /// Register a new service.
        /// </summary>
        /// <param name="service">Type of Service.</param>
        /// <param name="impl">Type of implementation class.</param>
        /// <param name="instance">Instance of implementation class.</param>
        /// <param name="lifeCycle">Lifecycle.</param>
        void Register(Type service, Type impl, object instance, EDependencyLifeCycle lifeCycle);
        /// <summary>
        /// Register a new service.
        /// </summary>
        /// <typeparam name="TService">Service.</typeparam>
        /// <typeparam name="TImpl">Implementation class.</typeparam>
        /// <param name="instance">Instance of implementation class.</param>
        /// <param name="lifeCycle">Lifecycle.</param>
        void Register<TService, TImpl>(object instance, EDependencyLifeCycle lifeCycle)
            where TService : class
            where TImpl : class, TService;
        /// <summary>
        /// Register a new service.
        /// </summary>
        /// <typeparam name="TService">Service.</typeparam>
        /// <typeparam name="TImpl">Implementation class.</typeparam>
        void Register<TService, TImpl>()
            where TImpl : class;
        /// <summary>
        /// Get TService instance.
        /// </summary>
        /// <typeparam name="TService">Service.</typeparam>
        /// <returns>TService instance.</returns>
        TService Resolve<TService>() where TService : class;
    }
}
