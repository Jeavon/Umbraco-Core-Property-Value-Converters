namespace Our.Umbraco.PropertyConverters
{
    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;

    using Our.Umbraco.PropertyConverters.Utilities;

    [PropertyValueType(typeof(IPublishedContent))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.ContentCache)]
    public class MediaPickerPropertyConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.MediaPickerAlias);
        }
        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null) return null;
            var sourceString = source.ToString();

            int nodeId; //check value is node id
            if (UmbracoContext.Current != null && int.TryParse(sourceString, out nodeId))
            {
                var umbHelper = new UmbracoHelper(UmbracoContext.Current);
                return ConverterHelper.DynamicInvocation() ? umbHelper.Media(nodeId) : umbHelper.TypedMedia(nodeId);
            }
            return null;
        }
    }
}