// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultipleMediaPickerPropertyConverter.cs" company="OurUmbraco">
//   Our.Umbraco
// </copyright>
// <summary>
//   The multiple media picker property editor converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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

    using Our.Umbraco.PropertyConverters.Utilities;

    /// <summary>
    /// The multiple media picker property editor converter.
    /// </summary>
    [PropertyValueType(typeof(IPublishedContent))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class MultipleMediaPickerPropertyConverter : PropertyValueConverterBase
    {
        /// <summary>
        /// Checks if this converter can convert the property editor and registers if it can.
        /// </summary>
        /// <param name="propertyType">
        /// The property type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.MultipleMediaPickerAlias);
        }

        /// <summary>
        /// Convert the raw source data into a object
        /// </summary>
        /// <param name="propertyType">
        /// The published property type.
        /// </param>
        /// <param name="source">
        /// The value of the property
        /// </param>
        /// <param name="preview">
        /// The preview.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public override object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
            {
                return null;
            }

            var sourceString = source.ToString();

            var dts = ApplicationContext.Current.Services.DataTypeService;
            var multiPickerPreValue = 
                dts.GetPreValuesCollectionByDataTypeId(propertyType.DataTypeId)
                    .PreValuesAsDictionary.FirstOrDefault(
                        x => string.Equals(x.Key, "multiPicker", StringComparison.InvariantCultureIgnoreCase)).Value;

            var multiPicker = multiPickerPreValue != null && multiPickerPreValue.Value.TryConvertTo<bool>().Result;

            if (UmbracoContext.Current == null)
            {
                return null;
            }

            var dynamicInvocation = ConverterHelper.DynamicInvocation();

            var umbHelper = new UmbracoHelper(UmbracoContext.Current);
            if (multiPicker)
            {
                var multiMediaPicker = Enumerable.Empty<IPublishedContent>();
                var nodeIds =
                    source.ToString()
                        .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();
                if (nodeIds.Length > 0)
                {
                    multiMediaPicker = dynamicInvocation ? umbHelper.Media(nodeIds) : umbHelper.TypedMedia(nodeIds).Where(x => x != null);
                }

                return multiMediaPicker;
            }

            int nodeId; // check value is node id
            if (int.TryParse(sourceString, out nodeId))
            {
                return dynamicInvocation ? umbHelper.Media(nodeId) : umbHelper.TypedMedia(nodeId);
            }
            else
            {
                LogHelper.Warn<MultipleMediaPickerPropertyConverter>(
                    string.Format(
                        "Data type \"{0}\" is not set to allow multiple items but appears to contain multiple items, check the setting and save the data type again",
                        dts.GetDataTypeDefinitionById(propertyType.DataTypeId).Name));
            }

            return null;
        }
    }
}