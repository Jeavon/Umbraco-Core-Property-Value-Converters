# Legacy Media Picker #

`Alias: Umbraco.MediaPicker`

`Returns: IPublishedContent`

## Typed Example: ##

```c#
    @{
        var typedMediaPickerSingle = Model.Content.GetPropertyValue<IPublishedContent>("multiMediaSingle");
        if (typedMediaPickerSingle != null)
        {
            <p>@typedMediaPickerSingle.Url</p>
        }
    }    
```

## Dynamic Example: ##

```c#
    @{
        if (CurrentPage.HasValue("mainImage"))
        {
            var dynamicMediaPicker = CurrentPage.mainImage;
            <p>Url: @dynamicMediaPicker.Url</p>
            <p>UmbracoFile: @dynamicMediaPicker.umbracoFile</p>
        }
    }       
```