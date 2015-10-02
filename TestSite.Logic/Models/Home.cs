namespace TestSite.Logic.Models
{
    using System.Collections.Generic;

    using Umbraco.Core.Models;

    using Our.Umbraco.PropertyConverters.Models;
    
    public class Home
    {
        public IPublishedContent ContentPicker { get; set; }
        public IPublishedContent MainImage { get; set; }
        public IEnumerable<IPublishedContent> FeaturedNews { get; set; }
        public IEnumerable<IPublishedContent> FeaturedMedia { get; set; }
        public IEnumerable<IPublishedContent> FeaturedMember { get; set; }
        public IEnumerable<IPublishedContent> MultiMedia { get; set; }
        public IPublishedContent MultiMediaSingle { get; set; }
        public RelatedLinks RelatedLinks { get; set; }
    }
}
