namespace Our.Umbraco.PropertyConverters.Models
{
    using Newtonsoft.Json;
    public class RelatedLinkData : RelatedLinkBase
    {
        [JsonProperty("internal")]
        public int? Internal { get; set; }
        [JsonProperty("edit")]
        public bool Edit { get; set; }
        [JsonProperty("internalName")]
        public string InternalName { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
