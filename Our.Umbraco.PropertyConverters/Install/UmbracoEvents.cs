namespace Our.Umbraco.PropertyConverters.Install
{
    using System;

    using global::Umbraco.Core;

    using umbraco.cms.businesslogic.packager;

    public class UmbracoEvents : ApplicationEventHandler
    {
        private const string File = "~/Views/web.config";
        private const string XPath = "//configuration/system.web.webPages.razor/pages";
        private const string NameSpace = "Our.Umbraco.PropertyConverters.Models";

        protected override void ApplicationStarted(
            UmbracoApplicationBase umbracoApplication,
            ApplicationContext applicationContext)
        {
            if (!Install.NameSpace.Check(File, XPath, NameSpace))
            {
                Install.NameSpace.Add(File, XPath, NameSpace);
            }
            InstalledPackage.BeforeDelete += InstalledPackage_BeforeDelete;
        }

        void InstalledPackage_BeforeDelete(InstalledPackage sender, EventArgs e)
        {
            if (sender.Data.Name == "Umbraco Core Property Value Converters")
            {
                Install.NameSpace.Remove(File, XPath, NameSpace);
            }
        }
    }
}
