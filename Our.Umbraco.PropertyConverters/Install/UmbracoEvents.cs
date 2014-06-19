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
            if (!ConfigNameSpaceModifier.Check(File, XPath, NameSpace))
            {
                ConfigNameSpaceModifier.Add(File, XPath, NameSpace);
            }
            InstalledPackage.BeforeDelete += InstalledPackage_BeforeDelete;
        }

        void InstalledPackage_BeforeDelete(InstalledPackage sender, EventArgs e)
        {
            if (sender.Data.Name == "Umbraco Core Property Value Converters")
            {
                ConfigNameSpaceModifier.Remove(File, XPath + "/namespaces", NameSpace);
            }
        }
    }
}
