# Working with Umbraco Models Builder #

The Umbraco Core Property Value Converters package works brilliantly with the Umbraco Model builder out of the box by converting the pickers into strongly typed `IPublishedContent`. 

However sometimes developers may want specific properties to return a custom strongly typed model, this can be achieved by disabling the default converter and creating a new class inheriting from the base class. Below is a example for the multiple media picker to convert from `IPublishedContent` to the ModelsBuilder generated `Image`, `File` and `Folder` models for specific properties that contain only those types anything else will continue to return `IPublishedContent`.

## Generated default models with `IPublishedContent` ##

	///<summary>
	/// Multi Media
	///</summary>
	[ImplementPropertyType("multiMedia")]
	public IEnumerable<IPublishedContent> MultiMedia
	{
		get { return this.GetPropertyValue<IEnumerable<IPublishedContent>>("multiMedia"); }
	}

	///<summary>
	/// Multi Media File
	///</summary>
	[ImplementPropertyType("multiMediaFile")]
	public IEnumerable<IPublishedContent> MultiMediaFile
	{
		get { return this.GetPropertyValue<IEnumerable<IPublishedContent>>("multiMediaFile"); }
	}

	///<summary>
	/// Multi Media Folder
	///</summary>
	[ImplementPropertyType("multiMediaFolder")]
	public IEnumerable<IPublishedContent> MultiMediaFolder
	{
		get { return this.GetPropertyValue<IEnumerable<IPublishedContent>>("multiMediaFolder"); }
	}

## Step 1: Disable default converter ##

    namespace TestSite.Logic.Events
    {
    	using Umbraco.Core;
    	using Umbraco.Core.PropertyEditors;
    
    	using Our.Umbraco.PropertyConverters;
    	public class RemoveDefaultConverters : ApplicationEventHandler
    	{
    		protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
    		{
    			PropertyValueConvertersResolver.Current.RemoveType<MultipleMediaPickerPropertyConverter>();
    		}
    	}
    }
    
## Step 2: Create a custom converter that inherits from MultipleMediaPickerPropertyConverter ##

We need to override two methods, `ConvertSourceToObject` and `GetPropertyValueType` and specify the property aliases that we want to convert to `Image`, `File` or `Folder`
    
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
    
    			var folder = SpecificType(propertyType, _folderPropertyAliases, typeof(IEnumerable<Folder>), typeof(Folder));
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

## Generated custom models with `Image`, `File` & `Folder` ##

		///<summary>
		/// Multi Media
		///</summary>
		[ImplementPropertyType("multiMedia")]
		public IEnumerable<TestSite.Logic.Models.DocumentTypes.Image> MultiMedia
		{
			get { return this.GetPropertyValue<IEnumerable<TestSite.Logic.Models.DocumentTypes.Image>>("multiMedia"); }
		}

		///<summary>
		/// Multi Media File
		///</summary>
		[ImplementPropertyType("multiMediaFile")]
		public IEnumerable<TestSite.Logic.Models.DocumentTypes.File> MultiMediaFile
		{
			get { return this.GetPropertyValue<IEnumerable<TestSite.Logic.Models.DocumentTypes.File>>("multiMediaFile"); }
		}

		///<summary>
		/// Multi Media Folder
		///</summary>
		[ImplementPropertyType("multiMediaFolder")]
		public IEnumerable<TestSite.Logic.Models.DocumentTypes.Folder> MultiMediaFolder
		{
			get { return this.GetPropertyValue<IEnumerable<TestSite.Logic.Models.DocumentTypes.Folder>>("multiMediaFolder"); }
		}