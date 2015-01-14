# Change Log #

- v2.1.5 - Revert fix for #1 as it's now fixed in Umbraco Core v7.2.1 and will reduce DB hits. Method slightly improved. 
- v2.1.4 - Fix for issue #4 - MNTP should return a DynamicPublishedContentList when used with dynamics
- v2.1.3 - Fix for issue #1 - Umbraco Core method EntityService.GetObjectType throws when item has been deleted
- v2.1.2 - Fix mistake in RelatedLinks skip logging
- v2.1.1 - Added a check to skip links in the RelatedLinks if they are internal and deleted or unpublished
- v2.1.0 - Added support for Members to Multinode tree picker, implemented IPropertyValueConverterMeta and correctly implemented both ConvertDataToSource and ConvertSourceToObject where applicable
- v2.0.5 - Fixed usage with dynamics (CurrentPage), also added optional AppSetting to fix mode to dynamic or typed to optimise performance if needed
- v2.0.4 - Added umbracoRedirect to properties to not convert & automatically add/remove namespace to Razor web.config on install/uninstall
- v2.0.3-beta - Fix for Content Picker so that it works with Archetype (Archetype doesn't pass in PropertyTypeAlias)
- v2.0.2-beta - Corrected Namespace to Our.Umbraco.PropertyConverters (breaking change for RelatedLinks)
- v2.0.1-beta - Related links now returns IEnumerable and RelatedLinks moved to Models namespace (breaking change)
- v2.0.0-beta - Umbraco v7 Support
- v1.0.3-beta - Ultimate Picker return type now dependant on "Type" setting