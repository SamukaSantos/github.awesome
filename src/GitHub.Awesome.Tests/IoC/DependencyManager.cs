
using GitHub.Awesome.Infra.Common.IoC;
using GitHub.Awesome.Infra.Common.IoC.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GitHub.Awesome.Tests.IoC
{
    public class DependencyManager : IoCSetup
    {
        
        #region Fields

        private static IDictionary<string, IDependencyItem> _items;
        private static IoCSetup _container;

        #endregion

        #region Constructor

        static DependencyManager()
        {
            _items = new Dictionary<string, IDependencyItem>();
        }

        #endregion

        #region Properties

        public static IoCSetup Container
        {
            get
            {
                if (_container == null)
                    _container = new DependencyManager();
                return _container;
            }
        }

        #endregion

        #region Methods

        private bool IsInDictionary(string key)
        {
            return _items.ContainsKey(key);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private void InternalRegister(IDependencyItem item)
        {
            if (item == null) return;

            if (!IsInDictionary(item.Impl.FullName))
                _items.Add(item.Impl.FullName, item);
            else
            {
                var fetchedItem = _items[item.Impl.FullName];

                fetchedItem
                    .ChangeValuesBeforeRegistration(item.Service, item.Impl, item.Instance, item.LifeCycle);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="impl"></param>
        /// <param name="instance"></param>
        /// <param name="lifeCycle"></param>
        public void Register(Type service, Type impl, object instance, EDependencyLifeCycle lifeCycle)
        {
            var dependencyItem = new DependencyItem(service, impl, instance, lifeCycle);

            InternalRegister(dependencyItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        /// <param name="instance"></param>
        /// <param name="lifeCycle"></param>
        public void Register<TService, TImpl>(object instance, EDependencyLifeCycle lifeCycle)
            where TService : class
            where TImpl : class, TService
        {
            var dependencyItem = new DependencyItem(typeof(TService), typeof(TImpl), instance, lifeCycle);

            InternalRegister(dependencyItem);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImpl"></typeparam>
        public void Register<TService, TImpl>()
            where TImpl : class
        {
            var dependencyItem = new DependencyItem(typeof(TService), typeof(TImpl), null, EDependencyLifeCycle.Transient);

            InternalRegister(dependencyItem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        public void Register<TService>(TService instance)
        {
            var dependencyItem = new DependencyItem(typeof(TService),
                                                    null,
                                                    instance,
                                                    EDependencyLifeCycle.Transient);
            InternalRegister(dependencyItem);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="fetchTarget"></param>
        /// <returns></returns>
        public TService Resolve<TService>()
            where TService : class
        {
            var item = (_items.FirstOrDefault(t => t.Value.Service == typeof(TService))).Value;

            if (item.Instance != null)
                return (TService)item.Instance;

            return (TService)Activator.CreateInstance(item.Impl);
        }

        #endregion

    }
}

