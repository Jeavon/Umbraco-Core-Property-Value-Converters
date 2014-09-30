﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RelatedLinks.cs" company="OurUmbraco">
//   Our.Umbraco
// </copyright>
// <summary>
//   Defines the RelatedLinks type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Our.Umbraco.PropertyConverters.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using global::Umbraco.Core.Logging;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The related links model
    /// </summary>
    public class RelatedLinks : IEnumerable<RelatedLink>
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// The _property data.
        /// </summary>
        private readonly string _propertyData;

        /// <summary>
        /// The _related links.
        /// </summary>
        private readonly List<RelatedLink> _relatedLinks = new List<RelatedLink>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedLinks"/> class.
        /// </summary>
        /// <param name="propertyData">
        /// The property data.
        /// </param>
        public RelatedLinks(string propertyData)
        {
            this._propertyData = propertyData;

            if (!string.IsNullOrEmpty(propertyData))
            {
                var relatedLinks = JsonConvert.DeserializeObject<JArray>(propertyData);

                foreach (var item in relatedLinks)
                {
                    var relatedLink = new RelatedLink(item);
                    if (!relatedLink.InternalLinkDeleted)
                    {
                        this._relatedLinks.Add(relatedLink);
                    }
                    else
                    {
                        LogHelper.Warn<RelatedLinks>(
                            string.Format("Related Links value converter skipped a link as the node has been unpublished/deleted (Id: {0}), ", relatedLink.IsInternal));
                    }
                }
            }
        }

        /// <summary>
        /// Gets the property data.
        /// </summary>
        public string PropertyData
        {
            get
            {
                return this._propertyData;
            }
        }

        /// <summary>
        /// The any.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Any()
        {
            return Enumerable.Any(this);
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<RelatedLink> GetEnumerator()
        {
            return this._relatedLinks.GetEnumerator();
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
