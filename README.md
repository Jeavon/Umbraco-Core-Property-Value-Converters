# Umbraco Core Property Value Converters v2#

![](PropertyValueConverters.png)

[![Build status](https://ci.appveyor.com/api/projects/status/tnonusc0x47djvj5/branch/v2?svg=true)](https://ci.appveyor.com/project/JeavonLeopold/umbraco-core-property-value-converters/branch/v2)

Umbraco Package: [http://our.umbraco.org/projects/developer-tools/umbraco-core-property-value-converters](http://our.umbraco.org/projects/developer-tools/umbraco-core-property-value-converters)
<br/>Nuget Package: `Install-Package Our.Umbraco.CoreValueConverters`
<br/>MyGet Latest Build Package: [https://www.myget.org/gallery/umbraco-core-property-value-converters](https://www.myget.org/gallery/umbraco-core-property-value-converters)

Once installed you cannot access the original raw value of the property by using `Model.Content.GetPropertyValue("propertyAlias")` however you can access it by using  `Model.Content.GetProperty("propertyAlias").DataValue.ToString()`

The v2 package for Umbraco v7 currently implements converters for the following built-in Umbraco property editors:

- [Content Picker](Docs/ContentPicker.md) - returns `IPublishedContent`
- [Media Picker](Docs/MediaPicker.md) - returns `IPublishedContent`
- [Multiple Media Picker](Docs/MultipleMediaPicker.md) - returns `IEnumerable<IPublishedContent> or IPublishedContent`
- [MultiNodeTreePicker](Docs/MultiNodeTreePicker.md) - returns `IEnumerable<IPublishedContent>`
- [Related Links](Docs/RelatedLinks.md) - returns `RelatedLinks`

These converters work with both the typed IPublishedContent (Model.Content) and also the dynamic DynamicPublishedContent (CurrentPage). We use some dark magic (StackTrace) to make sure we return the correct model. However if you only use one model you can specify this by adding a optional key to app settings of your web.config, this will avoid the dark magic and therefore improve performance. The optional setting is as follows:

    <add key="Our.Umbraco.CoreValueConverters:Mode" value="typed" />
    <add key="Our.Umbraco.CoreValueConverters:Mode" value="dynamic" />

[Change Log](Docs/ChangeLog.md)

This project is [MIT](http://opensource.org/licenses/mit-license.php) licensed
