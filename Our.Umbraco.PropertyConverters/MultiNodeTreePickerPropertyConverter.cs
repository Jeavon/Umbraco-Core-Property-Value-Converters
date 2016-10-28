﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiNodeTreePickerPropertyConverter.cs" company="OurUmbraco">
//   Our.Umbraco
// </copyright>
// <summary>
//   The multi node tree picker property editor value converter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Our.Umbraco.PropertyConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Globalization;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;
    using global::Umbraco.Web.Models;

    using Our.Umbraco.PropertyConverters.Utilities;

    /// <summary>
    /// The multi node tree picker property editor value converter.
    /// </summary>
    public class MultiNodeTreePickerPropertyConverter : PropertyValueConverterBase, IPropertyValueConverterMeta
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
        public override bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.MultiNodeTreePickerAlias);
        }

        /// <summary>
        /// Convert the raw string into a nodeId integer array
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
            var nodeIds =
                source.ToString()
                .Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            return nodeIds;
        }

        /// <summary>
        /// Convert the source nodeId into a IEnumerable of IPublishedContent (or DynamicPublishedContent)
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
            // Get the data type "content" or "media" setting
            /*
            var dts = ApplicationContext.Current.Services.DataTypeService;
            var startNodePreValue =
                dts.GetPreValuesCollectionByDataTypeId(propertyType.DataTypeId)
                    .PreValuesAsDictionary.FirstOrDefault(x => x.Key.ToLowerInvariant() == "startNode".ToLowerInvariant()).Value.Value;

            var startNodeObj = JsonConvert.DeserializeObject<JObject>(startNodePreValue);
            var pickerType = startNodeObj.GetValue("type").Value<string>();
            */

            if (source == null)
            {
                return null;
            }

            if (UmbracoContext.Current != null)
            {
                var nodeIds = (int[])source;

                if (!(propertyType.PropertyTypeAlias != null && PropertiesToExclude.InvariantContains(propertyType.PropertyTypeAlias)))
                {
                    var multiNodeTreePicker = new List<IPublishedContent>();

                    var dynamicInvocation = ConverterHelper.DynamicInvocation();
                    
                    var umbHelper = new UmbracoHelper(UmbracoContext.Current);

                    if (nodeIds.Length > 0)
                    {
                        var objectType = UmbracoObjectTypes.Unknown;

                        foreach (var nodeId in nodeIds)
                        {
                            var multiNodeTreePickerItem = GetPublishedContent(nodeId, ref objectType, UmbracoObjectTypes.Document, umbHelper.TypedContent)
                                        ?? GetPublishedContent(nodeId, ref objectType, UmbracoObjectTypes.Media, umbHelper.TypedMedia)
                                        ?? GetPublishedContent(nodeId, ref objectType, UmbracoObjectTypes.Member, umbHelper.TypedMember);

                            if (multiNodeTreePickerItem != null)
                            {
                                multiNodeTreePicker.Add(dynamicInvocation ? multiNodeTreePickerItem.AsDynamic() : multiNodeTreePickerItem);
                            }
                        }
                    }
                    return dynamicInvocation
                                ? new DynamicPublishedContentList(multiNodeTreePicker.Where(x => x != null))
                                : multiNodeTreePicker.Yield().Where(x => x != null);
                }
                
                // return the first nodeId as this is one of the excluded properties that expects a single id
                return nodeIds.FirstOrDefault();
            }
            else
            {
                return null;
            }
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
            return typeof(IEnumerable<IPublishedContent>);
        }

        /// <summary>
        /// Attempt to get an IPublishedContent instance based on ID and content type
        /// </summary>
        /// <param name="nodeId">The content node ID</param>
        /// <param name="actualType">The type of content being requested</param>
        /// <param name="expectedType">The type of content expected/supported by <paramref name="contentFetcher"/></param>
        /// <param name="contentFetcher">A function to fetch content of type <paramref name="expectedType"/></param>
        /// <returns>The requested content, or null if either it does not exist or <paramref name="actualType"/> does not match <paramref name="expectedType"/></returns>
        private IPublishedContent GetPublishedContent(int nodeId, ref UmbracoObjectTypes actualType, UmbracoObjectTypes expectedType, Func<int, IPublishedContent> contentFetcher)
        {
            // is the actual type supported by the content fetcher?
            if (actualType != UmbracoObjectTypes.Unknown && actualType != expectedType)
            {
                // no, return null
                return null;
            }

            // attempt to get the content
            try
            {
                var content = contentFetcher(nodeId);
                if (content != null)
                {
                    // if we found the content, assign the expected type to the actual type so we don't have to keep looking for other types of content
                    actualType = expectedType;
                }

                return content;
            }
            catch (Exception ex)
            {
                // we're catching the errors which are thrown from the Umbraco
                // in our case from: https://github.com/umbraco/Umbraco-CMS/blob/5397f2c53acbdeb0805e1fe39fda938f571d295a/src/Umbraco.Web/Security/MembershipHelper.cs
                // it's checking for the entity type and the order is content media then member and if membership provider is the custom one then the error will be thrown
                LogHelper.Error<MultiNodeTreePickerPropertyConverter>(string.Format("Error during fetching IPublishedContent for nodeId: {0}", nodeId), ex);
            }
            return null;
        }

    }
}
