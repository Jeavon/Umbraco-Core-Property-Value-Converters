namespace Our.Umbraco.PropertyConverters.Models
{
    using Newtonsoft.Json;
    public class RelatedLinkData
    {
        [JsonProperty("caption")]
        public string Caption { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("newWindow")]
        public bool NewWindow { get; set; }
        [JsonProperty("internal")]
        public int? Internal { get; set; }
        [JsonProperty("edit")]
        public bool Edit { get; set; }
        [JsonProperty("isInternal")]
        public bool IsInternal { get; set; }
        [JsonProperty("internalName")]
        public string InternalName { get; set; }
        [JsonProperty("type")]
        public RelatedLinkType Type { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
