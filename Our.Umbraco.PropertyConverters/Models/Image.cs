using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Our.Umbraco.PropertyConverters.Models
{
    public class Image : PublishedContentModel
    {
        public Image(IPublishedContent content) : base(content)
        {
            this.Cropper = content.GetPropertyValue<ImageCropDataSet>(Constants.Conventions.Media.File);
        }

        public ImageCropDataSet Cropper { get; set; }

    }
}
