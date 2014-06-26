// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MediaPickerPropertyConverter.cs" company="OurUmbraco">
//   Our.Umbraco
// </copyright>
// <summary>
//  The legacy media picker value converter
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Our.Umbraco.PropertyConverters
{
    using Our.Umbraco.PropertyConverters.Utilities;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;

    /// <summary>
    /// The media picker property value converter.
    /// </summary>
    [PropertyValueType(typeof(IPublishedContent))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.ContentCache)]
    public class MediaPickerPropertyConverter : PropertyValueConverterBase
    {
        /// <summary>
        /// Checks if this converter can convert the property editor and registers if it can.
        /// </summary>
        /// <param name="propertyType">
        /// The published property type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.MediaPickerAlias);
        }

        /// <summary>
        /// Convert the source nodeId into a IPublishedContent (or DynamicPublishedContent)
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
        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
            {
                return null;
            }

            var sourceString = source.ToString();

            int nodeId; // check value is node id
            if (UmbracoContext.Current != null && int.TryParse(sourceString, out nodeId))
            {
                var umbHelper = new UmbracoHelper(UmbracoContext.Current);
                return ConverterHelper.DynamicInvocation() ? umbHelper.Media(nodeId) : umbHelper.TypedMedia(nodeId);
            }

            return null;
        }
    }
}