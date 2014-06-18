# Content Picker #

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
        var dynamicContentPicker = CurrentPage.contentPicker;
        if (dynamicContentPicker != null)
        {
            <p>@dynamicContentPicker.Name</p>                                                
        } 
    }
```