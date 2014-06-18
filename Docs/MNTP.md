# Multi-Node Tree Picker (MNTP) #

`Returns: IEnumerable<IPublishedContent>`

Note: The property converter will work with both XML and CSV native storage types.

## Typed Example: ##

```c#
	@{
	    IEnumerable<IPublishedContent> typedMultiNodeTreePicker = Model.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("mntpFeaturePickerXML");
	    foreach (IPublishedContent item in typedMultiNodeTreePicker)
	    {       
	        <p>@item.Name</p>           
	    }       
	}
```

## Dynamic Example: ##

```c#
	@{
	    var dynamicMultiNodeTreePicker = CurrentPage.mntpFeaturePickerXML;
	    foreach (var item in dynamicMultiNodeTreePicker)
	    {       
	        <p>@item.Name</p>           
	    }    
	}
```