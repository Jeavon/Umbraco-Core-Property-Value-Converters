﻿namespace Our.Umbraco.PropertyConverters
{
    using System.Collections.Generic;
    using System.Globalization;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;

    [PropertyValueType(typeof(IPublishedContent))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class ContentPickerPropertyConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.ContentPickerAlias);
        }
        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null) return null;
            var sourceString = source.ToString();

            var propertiesToExclude = new List<string>()
                                          {
                                              Constants.Conventions.Content.InternalRedirectId.ToLowerInvariant(),
                                              Constants.Conventions.Content.Redirect.ToLowerInvariant()
                                          };

            int nodeId; 

            // check value is node id
            if (UmbracoContext.Current != null && int.TryParse(sourceString, out nodeId))
            {
                if (!(propertyType.PropertyTypeAlias != null && propertiesToExclude.Contains(propertyType.PropertyTypeAlias.ToLower(CultureInfo.InvariantCulture))))
                {                    
                    var umbHelper = new UmbracoHelper(UmbracoContext.Current);
                    var contentPickerContent = umbHelper.TypedContent(nodeId);
                    return contentPickerContent;
                }
            }

            return sourceString;
        }
    }
}