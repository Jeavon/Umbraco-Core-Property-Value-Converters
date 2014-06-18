# Umbraco Core Property Value Converters v2#

![](PropertyValueConverters.png)

Package: [http://our.umbraco.org/projects/developer-tools/umbraco-core-property-editor-converters](http://our.umbraco.org/projects/developer-tools/umbraco-core-property-editor-converters)

Once installed you cannot access the original raw value of the property by using `Model.Content.GetPropertyValue("propertyAlias")` however you can access it by using  `Model.Content.GetProperty("propertyAlias").Value.ToString()`

The v2 package for Umbraco v7 currently implements converters for the following built-in Umbraco property editors:

- [Content Picker](Docs/ContentPicker.md) - returns `IPublishedContent`
- [Media Picker](Docs/MediaPicker.md) - returns `IPublishedContent`
- [Multiple Media Picker](Docs/MultipleMediaPicker.md) - returns `IEnumerable<IPublishedContent> or IPublishedContent`
- [MultiNodeTreePicker](Docs/MultiNodeTreePicker.md) - returns `IEnumerable<IPublishedContent>`
- [Related Links](Docs/RelatedLinks.md) - returns `RelatedLinks`

[Change Log](Docs/ChangeLog.md)