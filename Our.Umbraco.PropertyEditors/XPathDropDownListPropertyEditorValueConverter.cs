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
using umbraco;

namespace Our.Umbraco.PropertyEditorConverters
{
    public class XPathDropDownListPropertyEditorValueConverter : IPropertyEditorValueConverter
    {
        string _propertyTypeAlias = string.Empty;
        string _docTypeAlias = string.Empty;

        public bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            _propertyTypeAlias = propertyTypeAlias;
            _docTypeAlias = docTypeAlias;
            return Guid.Parse("173a96ae-00ed-4a7c-9f76-4b53d4a0a1b9").Equals(propertyEditorId);
        }
        public Attempt<object> ConvertPropertyValue(object value)
        {
            int nodeId; //check value is node id and not stored as node names
            if (UmbracoContext.Current != null && int.TryParse(value.ToString(), out nodeId))
            {                
                var umbHelper = new UmbracoHelper(UmbracoContext.Current);
                IPublishedContent xPathDropDownListPickerContent = null;

                if (uQuery.GetUmbracoObjectType(nodeId) == uQuery.UmbracoObjectType.Document)
                {
                    xPathDropDownListPickerContent = umbHelper.TypedContent(nodeId);                    
                }
                else if (uQuery.GetUmbracoObjectType(nodeId) == uQuery.UmbracoObjectType.Media)
                {
                    xPathDropDownListPickerContent = umbHelper.TypedMedia(nodeId);
                }
                else if (uQuery.GetUmbracoObjectType(nodeId) == uQuery.UmbracoObjectType.Member)
                {
                    //Revist this when Members are supported by IPublishedContent
                    return Attempt<object>.Fail(); 
                }
                else if (nodeId == -1)  //check for no value and return null otherwise dynamic gets returned -1
                {
                    return Attempt<object>.Succeed(null);
                }
                else
                {
                    return Attempt<object>.Fail(); 
                }
                return Attempt<object>.Succeed(xPathDropDownListPickerContent);
            }
            else
            {
                return Attempt<object>.Fail();
            }
        }
    }
}
