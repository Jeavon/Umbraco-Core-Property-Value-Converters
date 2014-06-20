# Umbraco Core Property Value Converters v2#

![](PropertyValueConverters.png)

Umbraco Package: [http://our.umbraco.org/projects/developer-tools/umbraco-core-property-value-converters](http://our.umbraco.org/projects/developer-tools/umbraco-core-property-value-converters)
<br/>Nuget Package: `Install-Package Our.Umbraco.CoreValueConverters`

Once installed you cannot access the original raw value of the property by using `Model.Content.GetPropertyValue("propertyAlias")` however you can access it by using  `Model.Content.GetProperty("propertyAlias").Value.ToString()`

The v2 package for Umbraco v7 currently implements converters for the following built-in Umbraco property editors:

- [Content Picker](Docs/ContentPicker.md) - returns `IPublishedContent`
- [Media Picker](Docs/MediaPicker.md) - returns `IPublishedContent`
- [Multiple Media Picker](Docs/MultipleMediaPicker.md) - returns `IEnumerable<IPublishedContent> or IPublishedContent`
- [MultiNodeTreePicker](Docs/MultiNodeTreePicker.md) - returns `IEnumerable<IPublishedContent>`
- [Related Links](Docs/RelatedLinks.md) - returns `RelatedLinks`

[Change Log](Docs/ChangeLog.md)

This project is [MIT](http://opensource.org/licenses/mit-license.php) licensed