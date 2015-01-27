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

        internal static IEnumerable<T> Yield<T>(this IEnumerable<T> source)
        {
            foreach (var element in source)
            {
                yield return element;
            }
        }
    }
}
