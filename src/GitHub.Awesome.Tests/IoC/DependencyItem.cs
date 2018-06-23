
using GitHub.Awesome.Infra.Common.IoC.Enum;
using System;

namespace GitHub.Awesome.Tests.IoC
{
    public class DependencyItem : IDependencyItem
    {
        #region Properties

        public Type Impl { get; private set; }
        public Type Service { get; private set; }
        public object Instance { get; private set; }
        public EDependencyLifeCycle LifeCycle { get; private set; }

        #endregion

        #region Constructor

        public DependencyItem(Type service, Type impl, object instance, EDependencyLifeCycle lifeCycle)
        {
            Service = service;
            Impl    = impl;
            Instance = instance;
            LifeCycle = lifeCycle;
        }
        public void ChangeValuesBeforeRegistration(Type service, Type impl, object instance, EDependencyLifeCycle lifeCycle)
        {
            Service = service;
            Impl = impl;
            Instance = instance;
            LifeCycle = lifeCycle;
        }

        #endregion
    }

    public interface IDependencyItem
    {
        Type Impl { get; }
        Type Service { get; }
        object Instance { get; }
        EDependencyLifeCycle LifeCycle { get; }
        void ChangeValuesBeforeRegistration(Type service, Type impl, object instance, EDependencyLifeCycle lifeCycle);
    }
}
