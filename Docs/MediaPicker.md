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
        var dynamicMediaPickerSingle = CurrentPage.multiMediaSingle;
        if (dynamicMediaPickerSingle != null)
        {
            <img src="@dynamicMediaPickerSingle.Url" style="width:200px" />
        }
    }     
```