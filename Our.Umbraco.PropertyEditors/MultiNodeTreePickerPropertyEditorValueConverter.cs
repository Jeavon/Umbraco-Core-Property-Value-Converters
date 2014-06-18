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
    public class MultiNodeTreePickerPropertyEditorValueConverter : IPropertyEditorValueConverter
    {
        string _propertyTypeAlias = string.Empty;
        string _docTypeAlias = string.Empty;

        public bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            _propertyTypeAlias = propertyTypeAlias;
            _docTypeAlias = docTypeAlias;
            return Guid.Parse("7e062c13-7c41-4ad9-b389-41d88aeef87c").Equals(propertyEditorId);
        }
        public Attempt<object> ConvertPropertyValue(object value)
        {
            IEnumerable<IPublishedContent> multiNodeTreePicker = Enumerable.Empty<IPublishedContent>();
            if (UmbracoContext.Current != null)
            {
                int[] nodeIds = new int[0];
                if (XmlHelper.CouldItBeXml(value.ToString()))
                {
                    nodeIds = uQuery.GetXmlIds(value.ToString());
                }
                else
                {
                    nodeIds = value.ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                }
                var umbHelper = new UmbracoHelper(UmbracoContext.Current);

                IEnumerable<IPublishedContent> multiNodeTreePickerContent = Enumerable.Empty<IPublishedContent>();

                if (nodeIds != null && nodeIds.Length > 0) {
                    if (uQuery.GetUmbracoObjectType(nodeIds[0]) == uQuery.UmbracoObjectType.Document)
                    {
                        multiNodeTreePickerContent = umbHelper.TypedContent(nodeIds).Where(x => x != null);
                    } else if (uQuery.GetUmbracoObjectType(nodeIds[0]) == uQuery.UmbracoObjectType.Media){
                        multiNodeTreePickerContent = umbHelper.TypedMedia(nodeIds).Where(x => x != null);
                    }               
                    else
                    {
                        return Attempt<object>.Succeed(multiNodeTreePicker);
                    }
                }

                if (multiNodeTreePickerContent != null)
                {
                    multiNodeTreePicker = multiNodeTreePickerContent;
                }

                return Attempt<object>.Succeed(multiNodeTreePicker);
            }
            else
            {
                return Attempt<object>.Fail();
            }
        }
    }
}
