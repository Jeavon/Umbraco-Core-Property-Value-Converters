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
