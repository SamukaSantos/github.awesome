
using GitHub.Awesome.ViewModel.Base;
using System.Collections.ObjectModel;

namespace GitHub.Awesome.ViewModel.Input
{
    public class PullRequestCollectionViewModel : BaseViewModel
    { 
        public ObservableCollection<PullRequestItemViewModel> Items { get; set; }
    }
}
