// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConverterHelper.cs" company="OurUmbraco">
//   Our.Umbraco
// </copyright>
// <summary>
//   Defines the ConverterHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Our.Umbraco.PropertyConverters.Utilities
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Web;

    /// <summary>
    /// A helper class for use with value converters
    /// </summary>
    public static class ConverterHelper
    {
        /// <summary>
        /// The mode fixed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        internal static bool ModeFixed()
        {
            var mode = ConfigurationManager.AppSettings["Our.Umbraco.CoreValueConverters:Mode"];
            return mode != null && (mode == "typed" || mode == "dynamic");
        }

        /// <summary>
        /// Method checks if converter was executed from Dynamic model code (CurrentPage)
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        internal static bool DynamicInvocation()
        {
            var mode = ConfigurationManager.AppSettings["Our.Umbraco.CoreValueConverters:Mode"];

            if (mode != null)
            {
                if (mode == "typed")
                {
                    return false;
                }

                if (mode == "dynamic")
                {
                    return true;
                }
            }

            var st = new StackTrace();

            var invokedTypes = st.GetFrames().Select(x =>
            {
                var declaringType = x.GetMethod().DeclaringType;
                return declaringType != null ? declaringType.Name : null;
            }).ToList();

            return invokedTypes.Contains("DynamicPublishedContent");
        }

        /// <summary>
        /// Method to return a collection of IPublishedContent from a array of nodeIds
        /// </summary>
        /// <param name="nodeIds">
        /// The node ids.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        internal static List<IPublishedContent> FetchPublishedContent(int[] nodeIds)
        {
            if (UmbracoContext.Current != null)
            {
                var umbHelper = new UmbracoHelper(UmbracoContext.Current);

                if (nodeIds.Length > 0)
                {
                    var objectType = ApplicationContext.Current.Services.EntityService.GetObjectType(nodeIds[0]);

                    if (objectType == UmbracoObjectTypes.Document)
                    {
                        return umbHelper.TypedContent(nodeIds).Where(x => x != null).ToList();
                    }

                    if (objectType == UmbracoObjectTypes.Media)
                    {
                        return umbHelper.TypedMedia(nodeIds).Where(x => x != null).ToList();
                    }

                    if (objectType == UmbracoObjectTypes.Member)
                    {
                        var members = new List<IPublishedContent>();

                        foreach (var nodeId in nodeIds)
                        {
                            var member = umbHelper.TypedMember(nodeId);
                            if (member != null)
                            {
                                members.Add(member);
                            }
                        }

                        return members;
                    }
                }
            }

            return new List<IPublishedContent>();
        }
    }
}
