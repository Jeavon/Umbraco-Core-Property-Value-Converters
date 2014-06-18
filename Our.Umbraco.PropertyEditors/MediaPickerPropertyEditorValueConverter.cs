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
    public class MediaPickerPropertyEditorValueConverter : IPropertyEditorValueConverter
    {
        string _propertyTypeAlias = string.Empty;
        string _docTypeAlias = string.Empty;

        public bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            _propertyTypeAlias = propertyTypeAlias;
            _docTypeAlias = docTypeAlias;
            return Guid.Parse("ead69342-f06d-4253-83ac-28000225583b").Equals(propertyEditorId);
        }
        public Attempt<object> ConvertPropertyValue(object value)
        {
            int nodeId; //check value is node id and not stored as node names
            if (UmbracoContext.Current != null && int.TryParse(value.ToString(), out nodeId))
            {                
                var umbHelper = new UmbracoHelper(UmbracoContext.Current);
                IPublishedContent mediaPickerContent = null;

                if (uQuery.GetUmbracoObjectType(nodeId) == uQuery.UmbracoObjectType.Media)
                {
                    mediaPickerContent = umbHelper.TypedMedia(value.ToString());                    
                }                
                else
                {
                    return Attempt<object>.Fail();
                }
                return Attempt<object>.Succeed(mediaPickerContent);
            }
            else
            {
                return Attempt<object>.Fail();
            }
        }
    }
}
