# XPath CheckBoxList #

`Returns: IEnumerable<IPublishedContent>`

Note: If data type is set to member, property converter will not implement, so a CSV string will be returned. "values" setting in the data type should be set to "Node Ids" not "Node Names". The property converter will work with both XML and CSV native storage types.

## Typed Example: ##

```c#
    @{
        IEnumerable<IPublishedContent> typedXPathCheckBoxList = Model.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("xPathCheckBoxList");            
        foreach (IPublishedContent item in typedXPathCheckBoxList)
        {       
            <p>@item.Name</p>           
        }       
    }
```

## Dynamic Example: ##

```c#
    @{
        var dynamicXPathCheckBoxList = CurrentPage.xPathCheckBoxList;
        foreach (var item in dynamicXPathCheckBoxList)
        {       
            <p>@item.Name</p>           
        }    
    }
```