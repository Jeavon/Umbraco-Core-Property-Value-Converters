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
    public class ContentPickerPropertyEditorValueConverter : IPropertyEditorValueConverter
    {
        string _propertyTypeAlias = string.Empty;
        string _docTypeAlias = string.Empty;

        public bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            _propertyTypeAlias = propertyTypeAlias;
            _docTypeAlias = docTypeAlias;
            return Guid.Parse("158aa029-24ed-4948-939e-c3da209e5fba").Equals(propertyEditorId);
        }
        public Attempt<object> ConvertPropertyValue(object value)
        {
            int nodeId; //check value is node id
            if (UmbracoContext.Current != null && int.TryParse(value.ToString(), out nodeId) && _propertyTypeAlias != "umbracoInternalRedirectId")
            {                
                var umbHelper = new UmbracoHelper(UmbracoContext.Current);
                IPublishedContent contentPickerContent = null;

                if (uQuery.GetUmbracoObjectType(nodeId) == uQuery.UmbracoObjectType.Document)
                {
                    contentPickerContent = umbHelper.TypedContent(value.ToString());                    
                }                
                else
                {
                    return Attempt<object>.Fail();
                }
                return Attempt<object>.Succeed(contentPickerContent);
            }
            else
            {
                return Attempt<object>.Fail();
            }
        }
    }
}
