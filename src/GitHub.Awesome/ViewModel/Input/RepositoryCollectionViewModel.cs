
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace GitHub.Awesome.ViewModel.Input
{
    public class RepositoryCollectionViewModel
    {
		#region Properties

		[JsonProperty("total_count")]
        public long TotalCount { get; set; }

        [JsonProperty("incomplete_results")]
        public bool HasIncompleteResult { get; set; }

        [JsonProperty("items")]
        public ObservableCollection<RepositoryItemViewModel> Items { get; set; }

		#endregion      
    }
}
