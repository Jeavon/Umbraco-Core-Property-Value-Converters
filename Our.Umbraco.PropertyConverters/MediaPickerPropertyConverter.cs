﻿namespace Our.Umbraco.PropertyConverters
{
    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;

    [PropertyValueType(typeof(IPublishedContent))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class MediaPickerPropertyConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.MediaPickerAlias);
        }
        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null) return null;
            var sourceString = source.ToString();

            int nodeId; //check value is node id
            if (UmbracoContext.Current != null && int.TryParse(sourceString, out nodeId))
            {
                var umbHelper = new UmbracoHelper(UmbracoContext.Current);
                var mediaPickerContent = umbHelper.TypedMedia(nodeId);
                return mediaPickerContent;
            }
            return null;
        }
    }
}