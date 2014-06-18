# MultiNode Tree Picker (MNTP) #

`Alias: Umbraco.MultiNodeTreePicker`

`Returns: IEnumerable<IPublishedContent>`

Works with both Content and Media

## Typed Example: ##

```c#
    @{
        var typedMultiNodeTreePicker = Model.Content.GetPropertyValue<IEnumerable<IPublishedContent>>("featuredNews");
        foreach (var item in typedMultiNodeTreePicker)
        {
            <p>@item.Name</p>
        }
    }
```

## Dynamic Example: ##

```c#
    @{
        var dynamicMultiNodeTreePicker = CurrentPage.featuredNews;
        foreach (var item in dynamicMultiNodeTreePicker)
        {
            <p>@item.Name</p>
        }
    }
```