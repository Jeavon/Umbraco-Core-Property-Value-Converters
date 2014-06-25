﻿// --------------------------------------------------------------------------------------------------------------------
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
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;

    using Our.Umbraco.PropertyConverters.Utilities;

    /// <summary>
    /// The content picker property editor converter.
    /// </summary>
    [PropertyValueType(typeof(IPublishedContent))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.ContentCache)]
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
        public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            if (source == null)
            {
                return null;
            }

            var sourceString = source.ToString();

            var propertiesToExclude = new List<string>()
                                          {
                                              Constants.Conventions.Content.InternalRedirectId.ToLower(CultureInfo.InvariantCulture),
                                              Constants.Conventions.Content.Redirect.ToLower(CultureInfo.InvariantCulture)
                                          };

            int nodeId; 

            // check value is node id
            if (UmbracoContext.Current != null && int.TryParse(sourceString, out nodeId))
            {
                if (!(propertyType.PropertyTypeAlias != null && propertiesToExclude.Contains(propertyType.PropertyTypeAlias.ToLower(CultureInfo.InvariantCulture))))
                {
                    var umbHelper = new UmbracoHelper(UmbracoContext.Current);

                    return ConverterHelper.DynamicInvocation() ? umbHelper.Content(nodeId) : umbHelper.TypedContent(nodeId);
                }
            }

            return sourceString;
        }
    }
}