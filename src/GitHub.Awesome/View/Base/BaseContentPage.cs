

using System;
using GitHub.Awesome.Infra.Common.IoC;
using GitHub.Awesome.View.Interfaces;
using GitHub.Awesome.ViewModel.Base;
using Xamarin.Forms;

namespace GitHub.Awesome.View.Base
{
    /// <summary>
    /// Base Page with IPageProxy interface.
    /// </summary>
	public class BaseContentPage : ContentPage, IPageProxy
    {
        #region Constructor

        protected BaseContentPage() { }

        #endregion

        #region Methods

        /// <summary>
        /// Injects an object as BindingContext.
        /// </summary>
        /// <param name="context">object instance.</param>
        public void SetContext(object context)
        {
            BindingContext = context;

            SubscribeEvents();
        }

        /// <summary>
        /// Injects an object as BindingContext.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel type.</typeparam>
        public void SetContext<TViewModel>()
            where TViewModel : class
        {
			BindingContext = AppIoC.Container.Resolve<TViewModel>();

            SubscribeEvents();
        }

        /// <summary>
        /// Injects an object as BindingContext.
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        /// <typeparam name="TViewModel">Viewmodel type.</typeparam>
        public void SetContext<TViewModel>(object parameter)
            where TViewModel : class
        {
			if (!(AppIoC.Container.Resolve<TViewModel>() is IViewModel bindingContext))
				throw new ArgumentException();

			bindingContext.UpdateNavigationProxy((IPageProxy)parameter);

			BindingContext = bindingContext;

			SubscribeEvents();
        }

        /// <summary>
        /// Injects an object as BindingContext.
        /// </summary>
        /// <typeparam name="TViewModel">Viewmodel type.</typeparam>
        /// <param name="parameters">Input parameters.</param>
        public void SetContext<TViewModel>(object[] parameters)
            where TViewModel : class
        {
            var bindingContext = Activator.CreateInstance(typeof(TViewModel), parameters);
            
            BindingContext = bindingContext;

            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            Appearing    += RaisedViewModelEvent;
            Disappearing += RemoveViewModelEvent;
        }

        private void RaisedViewModelEvent(object sender, EventArgs e)
        {
            if (BindingContext is IViewModel viewmodel)
                viewmodel.OnAppearing();
        }

        private void UnsubscribeEvents()
        {
            Appearing -= RaisedViewModelEvent;
            Disappearing -= RaisedViewModelEvent;
        }

        private void RemoveViewModelEvent(object sender, EventArgs e)
        {
            if (BindingContext is IViewModel viewmodel)
                viewmodel.OnDisappearing();
        }

        /// <summary>
        /// Method responsible for call operations from viewmodel 
        /// that occurs during the BindingConext changing.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is IViewModel viewmodel)
                viewmodel.OnBindingContextChanged();
        }

        #endregion
    }
}
