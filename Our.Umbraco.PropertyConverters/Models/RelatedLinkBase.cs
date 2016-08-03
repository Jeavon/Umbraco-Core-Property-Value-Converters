namespace Our.Umbraco.PropertyConverters.Models
{
    using Newtonsoft.Json;
    public class RelatedLinkBase
    {
        [JsonProperty("caption")]
        public string Caption { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("newWindow")]
        public bool NewWindow { get; set; }
        [JsonProperty("isInternal")]
        public bool IsInternal { get; set; }
        [JsonProperty("type")]
        public RelatedLinkType Type { get; set; }
    }
}
