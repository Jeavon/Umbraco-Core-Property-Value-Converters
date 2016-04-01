using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Our.Umbraco.PropertyConverters;
using Umbraco.Core;
using Umbraco.Core.PropertyEditors;

namespace TestSite.Logic.Events
{
	public class RemoveDefaultConverters : ApplicationEventHandler
	{
		protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
		{
			PropertyValueConvertersResolver.Current.RemoveType<MultipleMediaPickerPropertyConverter>();
			//PropertyValueConvertersResolver.Current.RemoveType<MultiNodeTreePickerPropertyConverter>();
		}
	}
}
