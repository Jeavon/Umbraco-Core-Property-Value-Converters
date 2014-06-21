# Content Picker #

`Alias: Umbraco.ContentPickerAlias`

`Returns: IPublishedContent`

## Typed Example: ##

```c#
    @{
        IPublishedContent typedContentPicker = Model.Content.GetPropertyValue<IPublishedContent>("contentPicker");
        if (typedContentPicker != null)
        {
            <p>@typedContentPicker.Name</p>                                                
        } 
    }
```

## Dynamic Example: ##

```c#
    @{
        if (CurrentPage.HasValue("contentPicker"))
        {
            var dynamicContentPicker = CurrentPage.contentPicker;
            <p>Name: @dynamicContentPicker.Name</p>
            <p>Title: @dynamicContentPicker.title</p>
        }                    
    }

```