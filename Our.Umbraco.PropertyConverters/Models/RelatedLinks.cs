namespace Our.Umbraco.PropertyConverters.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    public class RelatedLinks : IEnumerable<RelatedLink>
    {
        private readonly List<RelatedLink> _relatedLinks;

        public RelatedLinks(List<RelatedLink> relatedLinks)
        {
            _relatedLinks = relatedLinks;
        }

        public bool Any()
        {
            return Enumerable.Any(this);
        }

        public IEnumerator<RelatedLink> GetEnumerator()
        {
            return _relatedLinks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
