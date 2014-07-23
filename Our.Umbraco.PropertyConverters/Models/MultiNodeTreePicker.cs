// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MultiNodeTreePicker.cs" company="OurUmbraco">
//   Our.Umbraco
// </copyright>
// <summary>
//   Defines the MultiNodeTreePicker type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Our.Umbraco.PropertyConverters.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Our.Umbraco.PropertyConverters.Utilities;

    using global::Umbraco.Core.Models;

    /// <summary>
    /// The multi node tree picker.
    /// </summary>
    public class MultiNodeTreePicker : IEnumerable<IPublishedContent>
    {
        /// <summary>
        /// The picked nodes.
        /// </summary>
        private readonly List<IPublishedContent> pickedNodes = new List<IPublishedContent>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiNodeTreePicker"/> class.
        /// </summary>
        /// <param name="nodeIds">
        /// The node ids.
        /// </param>
        public MultiNodeTreePicker(int[] nodeIds)
        {
            this.pickedNodes = ConverterHelper.FetchPublishedContent(nodeIds);
        }

        /// <summary>
        /// The ToString method to convert the objects back to the original CSV
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string ToString()
        {
            return string.Join(",", this.pickedNodes.Select(x => x.Id));
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator<IPublishedContent> GetEnumerator()
        {
            return this.pickedNodes.GetEnumerator();
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
