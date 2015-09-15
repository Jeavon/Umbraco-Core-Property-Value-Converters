using Our.Umbraco.PropertyConverters.Models;

namespace Crumpled.Logic.Utilities
{
    using System.Globalization;
    using System.Text;

    using Umbraco.Web.Models;

    public static class ImageCropperTemplateExtensions
    {
        public static string GetCropUrl(this Image image, string cropAlias)
        {
            return image.Cropper.GetCropUrl(cropAlias: cropAlias, useCropDimensions: true);
        }

        public static string GetCropUrl(this ImageCropDataSet imageCropper, string cropAlias)
        {
            return imageCropper.GetCropUrl(cropAlias: cropAlias, useCropDimensions: true);
        }

        public static string GetCropUrl(
            this ImageCropDataSet imageCropper,
            int? width = null,
            int? height = null,
            string cropAlias = null,
            int? quality = null,
            ImageCropMode? imageCropMode = null,
            ImageCropAnchor? imageCropAnchor = null,
            bool preferFocalPoint = false,
            bool useCropDimensions = false,
            string cacheBusterValue = null,
            string furtherOptions = null,
            ImageCropRatioMode? ratioMode = null,
            bool upScale = true)
        {
            if (imageCropper != null && string.IsNullOrEmpty(imageCropper.Src) == false)
            {
                var imageProcessorUrl = new StringBuilder();

                if (imageCropMode == ImageCropMode.Crop || imageCropMode == null)
                {

                    var crop = imageCropper.GetCrop(cropAlias);

                    imageProcessorUrl.Append(imageCropper.Src);

                    var cropBaseUrl = imageCropper.GetCropBaseUrl(cropAlias, preferFocalPoint);
                    if (cropBaseUrl != null)
                    {
                        imageProcessorUrl.Append(cropBaseUrl);
                    }
                    else
                    {
                        return null;
                    }

                    if (crop != null & useCropDimensions)
                    {
                        width = crop.Width;
                        height = crop.Height;
                    }

                    if (quality != null)
                    {
                        imageProcessorUrl.Append("&quality=" + quality);
                    }

                    if (width != null && ratioMode != ImageCropRatioMode.Width)
                    {
                        imageProcessorUrl.Append("&width=" + width);
                    }

                    if (height != null && ratioMode != ImageCropRatioMode.Height)
                    {
                        imageProcessorUrl.Append("&height=" + height);
                    }

                    if (ratioMode == ImageCropRatioMode.Width && height != null)
                    {
                        //if only height specified then assume a sqaure
                        if (width == null)
                        {
                            width = height;
                        }
                        var widthRatio = (decimal)width / (decimal)height;
                        imageProcessorUrl.Append("&widthratio=" + widthRatio.ToString(CultureInfo.InvariantCulture));
                    }

                    if (ratioMode == ImageCropRatioMode.Height && width != null)
                    {
                        //if only width specified then assume a sqaure
                        if (height == null)
                        {
                            height = width;
                        }
                        var heightRatio = (decimal)height / (decimal)width;
                        imageProcessorUrl.Append("&heightratio=" + heightRatio.ToString(CultureInfo.InvariantCulture));
                    }

                    if (upScale == false)
                    {
                        imageProcessorUrl.Append("&upscale=false");
                    }

                    if (furtherOptions != null)
                    {
                        imageProcessorUrl.Append(furtherOptions);
                    }

                    if (cacheBusterValue != null)
                    {
                        imageProcessorUrl.Append("&rnd=").Append(cacheBusterValue);
                    }

                    return imageProcessorUrl.ToString();
                }

                return string.Empty;
            }

            return null;
        }
    }
}
