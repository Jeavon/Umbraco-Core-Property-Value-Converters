# Multiple Media Picker #

`Alias: Umbraco.MultipleMediaPicker`

`Returns: IEnumerable<IPublishedContent>` or `IPublishedContent`

This converter returns a single item if the "Pick multiple items" data type setting is disabled or a collection if it is enabled.

![](Images/MultipleMediaPicker.png)

## Typed Example (multiple enabled): ##

```c#
    @{
        var typedMultiMediaPicker = Model.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("multiMedia");
        foreach (var item in typedMultiMediaPicker)
        {
            <img src="@item.Url" style="width:200px"/>
        }
    }
```

## Typed Example (multiple disabled): ##

```c#
    @{
        var typedMediaPickerSingle = Model.Content.GetPropertyValue<IPublishedContent>("multiMediaSingle");
        if (typedMediaPickerSingle != null)
        {
            <p>@typedMediaPickerSingle.Url</p>
            <img src="@typedMediaPickerSingle.Url" style="width:200px" alt="@typedMediaPickerSingle.GetPropertyValue("alt")" />
        }
    }      
```

## Dynamic Example (multiple enabled): ##

```c#
    @{
        var dynamicMultiMediaPicker = CurrentPage.multiMedia;
        foreach (var item in dynamicMultiMediaPicker)
        {
            <img src="@item.Url" style="width:200px" alt="@item.alt" />
        }
    }       
```


## Dynamic Example (multiple disabled): ##

```c#
    @{
        if (CurrentPage.HasValue("multiMediaSingle"))
        {
            var dynamicMediaPickerSingle = CurrentPage.multiMediaSingle;
            <img src="@dynamicMediaPickerSingle.Url" style="width:200px" alt="@dynamicMediaPickerSingle.alt" />
        }
    }    
```