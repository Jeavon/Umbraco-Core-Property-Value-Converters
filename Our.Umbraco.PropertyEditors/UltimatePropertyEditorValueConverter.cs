using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Models;
using umbraco.editorControls;
using Umbraco.Web;
using Umbraco.Core.Services;

namespace Our.Umbraco.PropertyEditorConverters
{
    public class UltimatePropertyEditorValueConverter : IPropertyEditorValueConverter
    {
        string _propertyTypeAlias = string.Empty;
        string _docTypeAlias = string.Empty;

        public bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            _propertyTypeAlias = propertyTypeAlias;
            _docTypeAlias = docTypeAlias;
            return Guid.Parse("cdbf0b5d-5cb2-445f-bc12-fcaaec07cf2c").Equals(propertyEditorId);
        }
        public Attempt<object> ConvertPropertyValue(object value)
        {
            if (UmbracoContext.Current != null)
            {

                var umbHelper = new UmbracoHelper(UmbracoContext.Current);
                IEnumerable<IPublishedContent> ultimatePicker = Enumerable.Empty<IPublishedContent>();
                string[] ultimatePickerNodeIds = value.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                IEnumerable<IPublishedContent> ultimatePickerContent = umbHelper.TypedContent(ultimatePickerNodeIds).Where(x => x != null);
                if (ultimatePickerContent != null && ultimatePickerNodeIds != null && ultimatePickerNodeIds.Length > 0)
                {
                    ultimatePicker = ultimatePickerContent;
                }

                PropertyEditorConverterHelper propertyEditorHelper = new PropertyEditorConverterHelper();
                IEnumerable<string> ultimatePickerSettings = propertyEditorHelper.GetDataTypeSettings(_docTypeAlias, _propertyTypeAlias, '|');                
                if (ultimatePickerSettings != null)
                {
                    //Multiple node selectors - return IEnumerable<IPublishedContent>
                    if (ultimatePickerSettings.FirstOrDefault() == "CheckBoxList" || ultimatePickerSettings.FirstOrDefault() == "ListBox")
                    {
                        return Attempt<object>.Succeed(ultimatePicker);
                    }
                    //Single node selectors - return IPublishedContent
                    else if (ultimatePickerSettings.FirstOrDefault() == "AutoComplete" || ultimatePickerSettings.FirstOrDefault() == "DropDownList" || ultimatePickerSettings.FirstOrDefault() == "RadioButtonList")
                    {
                        return Attempt<object>.Succeed(ultimatePicker.FirstOrDefault());
                    }
                    else
                    {
                        return Attempt<object>.Fail();
                    }
                }
                else
                {
                    return Attempt<object>.Fail();
                }

            }
            else
            {
                return Attempt<object>.Fail();
            }
        }
    }
}
