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
	public class MultipleMediaPickerConverterr : MultipleMediaPickerPropertyConverter
	{
		public override Type GetPropertyValueType(PublishedPropertyType propertyType)
		{
			var baseType = base.GetPropertyValueType(propertyType);

			if (baseType == typeof(IEnumerable<IPublishedContent>))
			{
				return !this.IsConverterDefault(propertyType) ? typeof(IEnumerable<Image>) : baseType;
			}

			return !this.IsConverterDefault(propertyType) ? typeof(Image) : baseType;
		}

		private bool IsConverterDefault(PublishedPropertyType propertyType)
		{
			return propertyType.PropertyTypeAlias.InvariantEquals("image");
		}
	}
}
