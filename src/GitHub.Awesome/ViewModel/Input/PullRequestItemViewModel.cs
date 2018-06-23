
using GitHub.Awesome.ViewModel.Base;
using Humanizer;
using Newtonsoft.Json;
using System;

namespace GitHub.Awesome.ViewModel.Input
{
    public class PullRequestItemViewModel : BaseViewModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("user")]
        public UserViewModel User { get; set; }
        [JsonProperty("head")]
        public HeadViewModel Head { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
		[JsonProperty("state")]
		public string State { get; set; }
        public string CustomTitle
        {
            get { return Title.Truncate(150, "..."); }
        }
        public string Date
        {
            get { return UpdatedAt.ToString("dd/MM/yyyy"); }
        }
    }
}
