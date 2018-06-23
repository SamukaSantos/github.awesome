
using GitHub.Awesome.ViewModel.Base;
using Newtonsoft.Json;

namespace GitHub.Awesome.ViewModel.Input
{
    public class UserViewModel : BaseViewModel
    {
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("login")]
        public string Name { get; set; }
        [JsonProperty("html_url")]
		public string HtmlUrl { get; set; }
    }
}
