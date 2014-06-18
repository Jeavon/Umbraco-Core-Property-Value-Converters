namespace Our.Umbraco.PropertyConverters
{
    using System;
    using System.Linq;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Logging;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;

    [PropertyValueType(typeof(IPublishedContent))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class MultipleMediaPickerPropertyConverter : PropertyValueConverterBase
    {
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.MultipleMediaPickerAlias);
        }
        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null) return null;
            var sourceString = source.ToString();

            bool multiPicker;
            var dts = ApplicationContext.Current.Services.DataTypeService;
            var multiPickerPreValue = 
                dts.GetPreValuesCollectionByDataTypeId(propertyType.DataTypeId)
                    .PreValuesAsDictionary.FirstOrDefault(
                        x => String.Equals(x.Key, "multiPicker", StringComparison.InvariantCultureIgnoreCase)).Value;

            if (multiPickerPreValue != null)
            {
                multiPicker = multiPickerPreValue.Value.TryConvertTo<bool>().Result;
            }
            else
            {
                multiPicker = false;
            }

            if (UmbracoContext.Current == null) return null;
            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            if (multiPicker)
            {
                var multiMediaPicker = Enumerable.Empty<IPublishedContent>();
                var nodeIds =
                    source.ToString()
                        .Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();
                if (nodeIds.Length > 0)
                {
                    multiMediaPicker = umbHelper.TypedMedia(nodeIds).Where(x => x != null);
                }
                return multiMediaPicker;
            }
            int nodeId; //check value is node id
            if (int.TryParse(sourceString, out nodeId))
            {
                return umbHelper.TypedMedia(nodeId);
            }
            else
            {
                LogHelper.Warn<MultipleMediaPickerPropertyConverter>(string.Format("Data type \"{0}\" is not set to allow multiple items but appears to contain multiple items, check the setting and save the data type again",
                    dts.GetDataTypeDefinitionById(propertyType.DataTypeId).Name));
            }
            return null;
        }
    }
}