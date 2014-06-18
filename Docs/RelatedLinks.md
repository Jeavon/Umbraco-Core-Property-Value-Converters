# Related Links #

`Returns: RelatedLinksList`

## Typed Example: ##

```c#
    @using Our.Umbraco.PropertyEditorConverters;
    @{            
        var typedRelatedLinksConverted = Model.Content.GetPropertyValue<RelatedLinksList>("relatedLinks");
   
        if (typedRelatedLinksConverted.Any()){
        <ul>    
        @foreach (var item in typedRelatedLinksConverted)
        {       
            var linkTarget = (item.NewWindow.Equals("1")) ? " target=\"_blank\"" : String.Empty;
            <li><a href="@item.Link"@Html.Raw(linkTarget)>@item.Title</a>  </li>                
        }
        </ul>            
        }    
    }
```

## Dynamic Example: ##

```c#
    @using Our.Umbraco.PropertyEditorConverters;
    @{            
        RelatedLinksList dynamicRelatedLinksConverted = CurrentPage.relatedLinks;
        if (dynamicRelatedLinksConverted.Any())
        {
        <ul>    
        @foreach (var item in dynamicRelatedLinksConverted)
        {       
            var linkTarget = (item.NewWindow.Equals("1")) ? " target=\"_blank\"" : String.Empty;
            <li><a href="@item.Link"@Html.Raw(linkTarget)>@item.Title</a>  </li>                
        }
        </ul>            
        }        
    }
```
