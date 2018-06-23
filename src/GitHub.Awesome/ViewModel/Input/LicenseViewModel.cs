
using Newtonsoft.Json;

namespace GitHub.Awesome.ViewModel.Input
{
    public class LicenseViewModel
    {      
		#region Properties

		[JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("spdx_id")]
        public string SpdxId { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("node_id")]
        public string NodeId { get; set; }

		#endregion      
    }
}
