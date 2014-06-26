// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentPickerPropertyConverter.cs" company="OurUmbraco">
//   Our.Umbraco
// </copyright>
// <summary>
//   The content picker property editor converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Our.Umbraco.PropertyConverters
{
    using System.Collections.Generic;
    using System.Globalization;

    using Our.Umbraco.PropertyConverters.Utilities;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;

    /// <summary>
    /// The content picker property value converter.
    /// </summary>
    [PropertyValueType(typeof(IPublishedContent))]
    [PropertyValueCache(PropertyCacheValue.Source, PropertyCacheLevel.Content)]
    [PropertyValueCache(PropertyCacheValue.Object, PropertyCacheLevel.ContentCache)]
    [PropertyValueCache(PropertyCacheValue.XPath, PropertyCacheLevel.Content)]
    public class ContentPickerPropertyConverter : PropertyValueConverterBase
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
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.ContentPickerAlias);
        }

        /// <summary>
        /// Convert the raw string into a nodeId integer
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
            var attemptConvertInt = source.TryConvertTo<int>();
            if (attemptConvertInt.Success)
            {
                return attemptConvertInt.Result;
            }

            return null;
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

            var propertiesToExclude = new List<string>()
                                          {
                                              Constants.Conventions.Content.InternalRedirectId.ToLower(CultureInfo.InvariantCulture),
                                              Constants.Conventions.Content.Redirect.ToLower(CultureInfo.InvariantCulture)
                                          };

            if (UmbracoContext.Current != null)
            {
                if (!(propertyType.PropertyTypeAlias != null && propertiesToExclude.Contains(propertyType.PropertyTypeAlias.ToLower(CultureInfo.InvariantCulture))))
                {
                    var umbHelper = new UmbracoHelper(UmbracoContext.Current);

                    return ConverterHelper.DynamicInvocation() ? umbHelper.Content(source) : umbHelper.TypedContent(source);
                }
            }

            return source;
        }
    }
}