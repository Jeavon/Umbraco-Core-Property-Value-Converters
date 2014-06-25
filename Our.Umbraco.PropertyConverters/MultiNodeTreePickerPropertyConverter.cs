// --------------------------------------------------------------------------------------------------------------------
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

    using Our.Umbraco.PropertyConverters.Utilities;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;

    /// <summary>
    /// The multi node tree picker property editor value converter.
    /// </summary>
    [PropertyValueType(typeof(IPublishedContent))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class MultiNodeTreePickerPropertyConverter : PropertyValueConverterBase
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
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.MultiNodeTreePickerAlias);
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

            var multiNodeTreePicker = Enumerable.Empty<IPublishedContent>();
            if (UmbracoContext.Current != null)
            {
                var nodeIds =
                    source.ToString()
                        .Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse)
                        .ToArray();

                var umbHelper = new UmbracoHelper(UmbracoContext.Current);

                if (nodeIds.Length > 0)
                {
                    var dynamicInvocation = ConverterHelper.DynamicInvocation();   
                 
                    var objectType = ApplicationContext.Current.Services.EntityService.GetObjectType(nodeIds[0]);

                    if (objectType == UmbracoObjectTypes.Document)
                    {
                        multiNodeTreePicker = dynamicInvocation ? umbHelper.Content(nodeIds) : umbHelper.TypedContent(nodeIds).Where(x => x != null);
                    }
                    else if (objectType == UmbracoObjectTypes.Media)
                    {
                        multiNodeTreePicker = dynamicInvocation ? umbHelper.Media(nodeIds) : umbHelper.TypedMedia(nodeIds).Where(x => x != null);
                    }
                    else if (objectType == UmbracoObjectTypes.Member)
                    {
                        var members = new List<IPublishedContent>();

                        foreach (var nodeId in nodeIds)
                        {
                            var member = umbHelper.TypedMember(nodeId);
                            if (member != null)
                            {
                                members.Add(dynamicInvocation ? member.AsDynamic() : member);
                            }
                        }

                        multiNodeTreePicker = members;
                    }
                    else
                    {
                        return null;
                    }
                }

                return multiNodeTreePicker;
            }
            else
            {
                return null;
            }
        }
    }
}
