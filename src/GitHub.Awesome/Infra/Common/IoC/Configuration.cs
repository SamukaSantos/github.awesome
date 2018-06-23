
using CotaFacil.Mobile.ViewModel.Navigatiion;
using GitHub.Awesome.Infra.Services;
using GitHub.Awesome.Infra.Services.Interfaces;
using GitHub.Awesome.View.Pages;
using GitHub.Awesome.View.Pages.Interfaces;
using GitHub.Awesome.ViewModel;
using GitHub.Awesome.ViewModel.Interfaces;
using GitHub.Awesome.ViewModel.Navigation.Interfaces;

namespace GitHub.Awesome.Infra.Common.IoC
{
	public class Configuration
    {
        /// <summary>
        /// Register the dependencies.
        /// </summary>
		public static void Start()
		{
            //Services.
			AppIoC.Container.Register<IRepositoryApiService, RepositoryApiService>();
            AppIoC.Container.Register<IPullRequestApiService, PullRequestApiService>();

			//ViewModel
			AppIoC.Container.Register<IMainMenuViewModel, MainMenuViewModel>();
            AppIoC.Container.Register<IRepositoryListViewModel, RepositoryListViewModel>();
            AppIoC.Container.Register<IPullRequestListViewModel, PullRequestListViewModel>();

			//View
			AppIoC.Container.Register<IErrorPage, ErrorPage>();
        }
	}
}