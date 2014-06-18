# Ultimate Picker #

`Returns: IPublishedContent or IEnumerable<IPublishedContent>`

## For single node selection types (Autocomplete, DropDownList, & RadioButtonList) ##

### Typed Example: ###

```c#
    @{
        IPublishedContent typedUltimatePickerNode = Model.Content.GetPropertyValue<IPublishedContent>("ultimatePicker");                        
        <p>@typedUltimatePickerNode.Name</p>                                     
    }
```

### Dynamic Example: ###

```c#
    @{
        var dynamicUltimatePickerNode = CurrentPage.ultimatePicker;
     	<p>@dynamicUltimatePickerNode.Name</p>              
    }
```

## For multi node selection types (CheckBoxList & ListBox) ##

### Typed Example: ###

```c#
    @{
        IEnumerable<IPublishedContent> typedUltimatePickerNodes = Model.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("ultimatePicker");                      
        foreach (IPublishedContent item in typedUltimatePickerNodes)
        {       
            <p>@item.Name</p>           
        }                            
    }
```

### Dynamic Example: ###

```c#
    @{
        var dynamicUltimatePickerNodes = CurrentPage.ultimatePicker;
        foreach (var item in dynamicUltimatePickerNodes)
        {       
            <p>@item.Name</p>           
        }    
    }
```