# Media Picker #

`Returns: IPublishedContent`

## Typed Example: ##

```c#
    @{
        IPublishedContent typedMediaPicker = Model.Content.GetPropertyValue<IPublishedContent>("mainImage");
        if (typedMediaPicker != null)
        {
            <p>@typedMediaPicker.Name</p>                                                
        } 
    }
```

## Dynamic Example: ##

```c#
    @{
        var dynamicMediaPicker = CurrentPage.mainImage;
        if (dynamicMediaPicker != null)
        {
            <p>@dynamicMediaPicker.Name</p>                                                
        } 
    }   
```