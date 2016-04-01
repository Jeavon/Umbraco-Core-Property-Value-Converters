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
    using System;
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
    public class ContentPickerPropertyConverter : IPropertyValueConverterMeta
    {
        /// <summary>
        /// The properties to exclude.
        /// </summary>
        private static readonly List<string> PropertiesToExclude = new List<string>()
        {
            Constants.Conventions.Content.InternalRedirectId.ToLower(CultureInfo.InvariantCulture),
            Constants.Conventions.Content.Redirect.ToLower(CultureInfo.InvariantCulture)
        };

        /// <summary>
        /// Checks if this converter can convert the property editor and registers if it can.
        /// </summary>
        /// <param name="propertyType">
        /// The published property type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsConverter(PublishedPropertyType propertyType)
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
        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
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
        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
            {
                return null;
            }

            if (UmbracoContext.Current != null)
            {
                if (!(propertyType.PropertyTypeAlias != null && PropertiesToExclude.Contains(propertyType.PropertyTypeAlias.ToLower(CultureInfo.InvariantCulture))))
                {
                    var content = UmbracoContext.Current.ContentCache.GetById((int)source);
                    return ConverterHelper.DynamicInvocation() ? content.AsDynamic() : content;
                }
            }

            return source;
        }

        /// <summary>
        /// The convert source to xPath.
        /// </summary>
        /// <param name="propertyType">
        /// The property type.
        /// </param>
        /// <param name="source">
        /// The source.
        /// </param>
        /// <param name="preview">
        /// The preview.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            return source.ToString();
        }

        /// <summary>
        /// The CLR type that the value converter returns.
        /// </summary>
        /// <param name="propertyType">
        /// The property type.
        /// </param>
        /// <returns>
        /// The <see cref="Type"/>.
        /// </returns>
        public virtual Type GetPropertyValueType(PublishedPropertyType propertyType)
        {
            return typeof(IPublishedContent);
        }

        /// <summary>
        /// The get property cache level.
        /// </summary>
        /// <param name="propertyType">
        /// The property type.
        /// </param>
        /// <param name="cacheValue">
        /// The cache value.
        /// </param>
        /// <returns>
        /// The <see cref="PropertyCacheLevel"/>.
        /// </returns>
        public PropertyCacheLevel GetPropertyCacheLevel(PublishedPropertyType propertyType, PropertyCacheValue cacheValue)
        {
            PropertyCacheLevel returnLevel;
            switch (cacheValue)
            {
                case PropertyCacheValue.Object:
                    returnLevel = ConverterHelper.ModeFixed() ? PropertyCacheLevel.ContentCache : PropertyCacheLevel.Request;
                    break;
                case PropertyCacheValue.Source:
                    returnLevel = PropertyCacheLevel.Content;
                    break;
                case PropertyCacheValue.XPath:
                    returnLevel = PropertyCacheLevel.Content;
                    break;
                default:
                    returnLevel = PropertyCacheLevel.None;
                    break;
            }

            return returnLevel;
        }
    }
}