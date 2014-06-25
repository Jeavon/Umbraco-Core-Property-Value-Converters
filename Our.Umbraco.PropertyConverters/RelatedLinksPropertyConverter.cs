namespace Our.Umbraco.PropertyConverters
{
    using Our.Umbraco.PropertyConverters.Models;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;

    [PropertyValueType(typeof(RelatedLinks))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.ContentCache)]
    public class RelatedLinksPropertyConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.RelatedLinksAlias);
        }

        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null) return null;
            var sourceString = source.ToString();

            return UmbracoContext.Current != null ? new RelatedLinks(sourceString) : null;
        }
    }
}