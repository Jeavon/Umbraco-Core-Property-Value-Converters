# Related Links #

`Alias: Umbraco.RelatedLinks`

`Returns: RelatedLinks`

## Typed Example: ##

```c#
    @{
        var typedRelatedLinksConverted = Model.Content.GetPropertyValue<RelatedLinks>("relatedLinks");

        if (typedRelatedLinksConverted.Any())
        {
            <ul>
                @foreach (var item in typedRelatedLinksConverted)
                {
                    var linkTarget = (item.NewWindow) ? "_blank" : null;
                    <li><a href="@item.Link" target="@linkTarget">@item.Caption</a></li>
                }
            </ul>
        }
    }   
```

## Dynamic Example: ##

```c#
    @{
        var dynamicRelatedLinks = CurrentPage.relatedLinks;

        if (dynamicRelatedLinks.Any())
        {
            <ul>
                @foreach (var item in dynamicRelatedLinks)
                {
                    var linkTarget = (item.NewWindow) ? "_blank" : null;
                    <li><a href="@item.Link" target="@linkTarget">@item.Caption</a></li>
                }
            </ul>
        }
    }  
```
