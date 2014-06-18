namespace Our.Umbraco.PropertyConverters.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Collections;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class RelatedLinks : IEnumerable<RelatedLink>
    {
        private readonly string _propertyData;
        private readonly List<RelatedLink> _relatedLinks = new List<RelatedLink>(); 

        public RelatedLinks(string propertyData)
        {
            _propertyData = propertyData;

            if (!string.IsNullOrEmpty(propertyData))
            {
                var relatedLinks = JsonConvert.DeserializeObject<JArray>(propertyData);

                foreach (var item in relatedLinks)
                {
                    _relatedLinks.Add(new RelatedLink(item));
                }
            }
        }

        public string PropertyData
        {
            get
            {
                return _propertyData;
            }
        }

        public bool Any()
        {
            return Enumerable.Any(this);
        }

        public IEnumerator<RelatedLink> GetEnumerator()
        {
            return this._relatedLinks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
