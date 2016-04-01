using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Our.Umbraco.PropertyConverters;
using TestSite.Logic.Models.DocumentTypes;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.PublishedContent;

namespace TestSite.Logic.PropertyConverters
{
	public class MultipleMediaPickerConverter : MultipleMediaPickerPropertyConverter
	{
		public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
		{
			var publishedContent = base.ConvertSourceToObject(propertyType, source, preview);
			var isMultiplePicker = IsMultipleDataType(propertyType.DataTypeId);

			if (IsImageProperty(propertyType) )
			{
				// check if multiple picker and should be converted to a collection of images
				if (isMultiplePicker)
				{
					return ((IEnumerable<IPublishedContent>) publishedContent).Where(t => t.GetType() == typeof(Image)).Select(i => i as Image);
				}

				// return as a image
				return publishedContent as Image;
			}

			if (IsFileProperty(propertyType))
			{
				// check if multiple picker and should be converted to a collection of files
				if (isMultiplePicker)
				{
					return ((IEnumerable<IPublishedContent>)publishedContent).Where(t => t.GetType() == typeof(Models.DocumentTypes.File)).Select(i => i as Models.DocumentTypes.File);
				}

				// return as a file
				return publishedContent as Models.DocumentTypes.File;
			}

			if (IsFolderProperty(propertyType))
			{
				// check if multiple picker and should be converted to a collection of folders
				if (isMultiplePicker)
				{
					return ((IEnumerable<IPublishedContent>)publishedContent).Where(t => t.GetType() == typeof(Folder)).Select(i => i as Folder);
				}

				// return as a folder
				return publishedContent as Folder;
			}

			// return IPublishedContent
			return publishedContent;
		}
		public override Type GetPropertyValueType(PublishedPropertyType propertyType)
		{
			var baseType = base.GetPropertyValueType(propertyType);

			if (IsImageProperty(propertyType))
			{
				return IsMultipleDataType(propertyType.DataTypeId) ? typeof (IEnumerable<Image>) : typeof(Image);
			}

			if (IsFileProperty(propertyType))
			{
				return IsMultipleDataType(propertyType.DataTypeId) ? typeof(IEnumerable<Models.DocumentTypes.File>) : typeof(Models.DocumentTypes.File);
			}

			if (IsFolderProperty(propertyType))
			{
				return IsMultipleDataType(propertyType.DataTypeId) ? typeof(IEnumerable<Folder>) : typeof(Folder);
			}

			return baseType;
		}

		private static bool IsImageProperty(PublishedPropertyType propertyType)
		{
			var propertyAliases = new[] {"multiMedia", "bannerPicker", "multiMediaSingle" };

			return propertyAliases.InvariantContains(propertyType.PropertyTypeAlias);
		}

		private static bool IsFileProperty(PublishedPropertyType propertyType)
		{
			var propertyAliases = new[] { "multiMediaFile"};

			return propertyAliases.InvariantContains(propertyType.PropertyTypeAlias);
		}
		private static bool IsFolderProperty(PublishedPropertyType propertyType)
		{
			var propertyAliases = new[] { "multiMediaFolder" };

			return propertyAliases.InvariantContains(propertyType.PropertyTypeAlias);
		}
	}
}
