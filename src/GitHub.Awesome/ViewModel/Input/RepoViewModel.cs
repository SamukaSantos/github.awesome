
using GitHub.Awesome.ViewModel.Base;
using Humanizer;
using Newtonsoft.Json;

namespace GitHub.Awesome.ViewModel.Input
{
    public class RepoViewModel : BaseViewModel
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        public string CustomDescription
        {
            get { return Description.Truncate(150, "..."); }
        }
    }
}
