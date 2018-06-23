
using System.Collections.Generic;
using GitHub.Awesome.Infra.Resources;
using GitHub.Awesome.View.Interfaces;
using GitHub.Awesome.View.Pages;
using GitHub.Awesome.ViewModel.Base;
using GitHub.Awesome.ViewModel.Navigation;
using GitHub.Awesome.ViewModel.Navigation.Interfaces;
using GitHub.Awesome.ViewTemplate.Styles;

namespace CotaFacil.Mobile.ViewModel.Navigatiion
{
	public class MainMenuViewModel : BaseViewModel<object>, IMainMenuViewModel
    {
        #region Fields

        private List<NavigationObject> _options;
              
        #endregion

        #region Properties

        /// <summary>
        /// Menu Options. 
        /// </summary>
        public List<NavigationObject> Options
        {
            get { return _options; }
            set { SetField(ref _options, value); }
        }
  
        #endregion

        #region Constructor
  
		public MainMenuViewModel()
			: base(){}

        public MainMenuViewModel(IPageProxy pageProxy)
			: base(pageProxy){}

        #endregion
              
        #region Methods
        
		public override void OnAppearing()
		{
			base.OnAppearing();

			LoadOptions();
		}
  
		private void LoadOptions()
        {
            Options = new List<NavigationObject>
            {
                new NavigationObject(AppResources.LABEL_REPOSITORIES,
                                     typeof(RepositoryListPage), 
				                     string.Empty, 
				                     FontAwesomeIcons.Book),

				new NavigationObject(AppResources.LABEL_ABOUT,
                                     typeof(AboutPage),
                                     string.Empty,
                                     FontAwesomeIcons.User)
            };
        }

        #endregion
    }
}
