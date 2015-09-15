using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Our.Umbraco.PropertyConverters.Models;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Our.Umbraco.PropertyConverters.Utilities
{
    public static class UmbracoHelperExtensions
    {
        public static Image TypedImage(this UmbracoHelper umbracoHelper, int id)
        {
            var mediaItem = umbracoHelper.TypedMedia(id);

            if (mediaItem == null)
                return null;

            return new Image(mediaItem);
        }

        public static Image TypedImage(this UmbracoHelper umbracoHelper, IPublishedContent publishedContent)
        {
            return new Image(publishedContent);
        }
    }
}
