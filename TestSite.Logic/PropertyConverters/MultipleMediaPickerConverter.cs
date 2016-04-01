namespace TestSite.Logic.PropertyConverters
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Umbraco.Core;
	using Umbraco.Core.Models;
	using Umbraco.Core.Models.PublishedContent;

	using Our.Umbraco.PropertyConverters;

	using Models.DocumentTypes;

	public class MultipleMediaPickerConverter : MultipleMediaPickerPropertyConverter
	{
		private readonly string[] _imagePropertyAliases = { "multiMedia", "bannerPicker", "multiMediaSingle" };
		private readonly string[] _filePropertyAliases = { "multiMediaFile" };
		private readonly string[] _folderPropertyAliases = { "multiMediaFolder" };

		public override object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
		{
			var publishedContent = base.ConvertSourceToObject(propertyType, source, preview);
			var isMultiplePicker = IsMultipleDataType(propertyType.DataTypeId);

			if (IsPropertySpecific(propertyType, _imagePropertyAliases) )
			{
				// check if multiple picker and should be converted to a collection of images
				if (isMultiplePicker)
				{
					return ((IEnumerable<IPublishedContent>) publishedContent).Where(t => t.GetType() == typeof (Image)).Cast<Image>();
				}

				// return as a image
				return publishedContent as Image;
			}

			if (IsPropertySpecific(propertyType, _filePropertyAliases))
			{
				// check if multiple picker and should be converted to a collection of files
				if (isMultiplePicker)
				{
					return ((IEnumerable<IPublishedContent>)publishedContent).Where(t => t.GetType() == typeof(Models.DocumentTypes.File)).Cast<Models.DocumentTypes.File>();
				}

				// return as a file
				return publishedContent as Models.DocumentTypes.File;
			}

			if (IsPropertySpecific(propertyType, _folderPropertyAliases))
			{
				// check if multiple picker and should be converted to a collection of folders
				if (isMultiplePicker)
				{
					return ((IEnumerable<IPublishedContent>)publishedContent).Where(t => t.GetType() == typeof(Folder)).Cast<Folder>();
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

			var image = SpecificType(propertyType, _imagePropertyAliases, typeof (IEnumerable<Image>), typeof (Image));
			if (image != null)
			{
				return image;
			}

			var file = SpecificType(propertyType, _filePropertyAliases, typeof(IEnumerable<Models.DocumentTypes.File>), typeof(Models.DocumentTypes.File));
			if (file != null)
			{
				return file;
			}

			var folder = SpecificType(propertyType, _filePropertyAliases, typeof(IEnumerable<Folder>), typeof(Folder));
			if (folder != null)
			{
				return folder;
			}

			return baseType;
		}
		private static bool IsPropertySpecific(PublishedPropertyType propertyType, string[] propertyAliases)
		{
			return propertyAliases.InvariantContains(propertyType.PropertyTypeAlias);
		}

		private Type SpecificType(PublishedPropertyType propertyType, string[] propertyAliases, Type multi, Type single)
		{
			if (IsPropertySpecific(propertyType, propertyAliases))
			{
				return IsMultipleDataType(propertyType.DataTypeId) ? multi : single;
			}
			return null;
		}
	}
}