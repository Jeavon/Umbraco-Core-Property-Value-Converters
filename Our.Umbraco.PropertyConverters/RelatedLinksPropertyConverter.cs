// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelatedLinksPropertyConverter.cs" company="OurUmbraco">
//   Our.Umbraco
// </copyright>
// <summary>
//   Defines the RelatedLinksPropertyConverter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Our.Umbraco.PropertyConverters
{
    using System.Collections.Generic;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models.PublishedContent;
    using global::Umbraco.Core.PropertyEditors;
    using global::Umbraco.Web;
    using global::Umbraco.Core.Logging;

    using Newtonsoft.Json;

    using Our.Umbraco.PropertyConverters.Models;

    /// <summary>
    /// The related links property value converter.
    /// </summary>
    [PropertyValueType(typeof(RelatedLinks))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.ContentCache)]
    public class RelatedLinksPropertyConverter : PropertyValueConverterBase
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
            return propertyType.PropertyEditorAlias.Equals(Constants.PropertyEditors.RelatedLinksAlias);
        }

        /// <summary>
        /// Convert the source nodeId into a RelatedLinks object
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

            var relatedLinksData = JsonConvert.DeserializeObject<IEnumerable<RelatedLinkData>>(sourceString);
            var relatedLinks = new List<RelatedLink>();

            foreach (var linkData in relatedLinksData)
            {
                var relatedLink = new RelatedLink()
                {
                    Caption = linkData.Caption,
                    NewWindow = linkData.NewWindow,
                    IsInternal = linkData.IsInternal,
                    Type = linkData.Type,
                    Id = linkData.Internal,
                    Link = linkData.Link
                };
                relatedLink = CreateLink(relatedLink);

                if (!relatedLink.IsDeleted)
                {
                    relatedLinks.Add(relatedLink);
                }
                else
                {
                    LogHelper.Warn<RelatedLinks>(
                        $"Related Links value converter skipped a link as the node has been unpublished/deleted (Internal Link NodeId: {relatedLink.Link}, Link Caption: \"{relatedLink.Caption}\")");
                }
            }

            return new RelatedLinks(relatedLinks);
        }

        private RelatedLink CreateLink(RelatedLink link)
        {
            if (link.IsInternal && link.Id != null)
            {
                if (UmbracoContext.Current == null)
                {
                    return null;
                }

                link.Link = UmbracoContext.Current.UrlProvider.GetUrl((int)link.Id);
                if (link.Link.Equals("#"))
                {
                    link.IsDeleted = true;
                    link.Link = link.Id.ToString();
                }
                else
                {
                    link.IsDeleted = false;
                }
            }

            return link;
        }
    }
}