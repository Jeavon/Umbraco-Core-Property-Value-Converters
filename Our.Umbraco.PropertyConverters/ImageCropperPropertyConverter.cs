namespace Crumpled.Logic.ValueConverters
{
    using Newtonsoft.Json;
    using System;
    using Umbraco.Core;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.PropertyEditors;
    using Umbraco.Web.Models;

    public class ImageCropperPropertyConverter : PropertyValueConverterBase, IPropertyValueConverterMeta
    {
        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            try
            {
                return JsonConvert.DeserializeObject<ImageCropDataSet>(source.ToString());
            }
            catch
            {
                return null;
            }
        }

        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return Constants.PropertyEditors.ImageCropperAlias.InvariantEquals(propertyType.PropertyEditorAlias);
        }

        public PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType, PropertyCacheValue cacheValue)
        {
            return PropertyCacheLevel.Content;
        }

        public Type GetPropertyValueType(PublishedPropertyType propertyType)
        {
            return typeof(ImageCropDataSet);
        }
    }
}
