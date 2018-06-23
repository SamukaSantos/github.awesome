
using GitHub.Awesome.ViewModel.Base;
using Newtonsoft.Json;

namespace GitHub.Awesome.ViewModel.Input
{
    public class HeadViewModel : BaseViewModel
    {
        [JsonProperty("repo")]
        public RepoViewModel Repo { get; set; }
       
    }
}
