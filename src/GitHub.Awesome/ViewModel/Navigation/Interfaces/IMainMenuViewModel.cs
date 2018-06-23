

using GitHub.Awesome.ViewModel.Base;
using System.Collections.Generic;

namespace GitHub.Awesome.ViewModel.Navigation.Interfaces
{
	public interface IMainMenuViewModel : IViewModel
    {
        /// <summary>
        /// Menu Options. 
        /// </summary>
        List<NavigationObject> Options { get; set; }
    }
}
