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
    public class XPathCheckBoxListPropertyEditorValueConverter : IPropertyEditorValueConverter
    {
        string _propertyTypeAlias = string.Empty;
        string _docTypeAlias = string.Empty;

        public bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            _propertyTypeAlias = propertyTypeAlias;
            _docTypeAlias = docTypeAlias;
            return Guid.Parse("34451d92-d270-49ba-8c7f-ee55bfeee1cb").Equals(propertyEditorId);
        }
        public Attempt<object> ConvertPropertyValue(object value)
        {
            IEnumerable<IPublishedContent> xPathCheckBoxListPicker = Enumerable.Empty<IPublishedContent>();
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

                IEnumerable<IPublishedContent> xPathCheckBoxListPickerContent = Enumerable.Empty<IPublishedContent>();

                if (nodeIds != null && nodeIds.Length > 0){
                
                    if (uQuery.GetUmbracoObjectType(nodeIds[0]) == uQuery.UmbracoObjectType.Document)
                    {
                        xPathCheckBoxListPickerContent = umbHelper.TypedContent(nodeIds).Where(x => x != null);
                    } else if (uQuery.GetUmbracoObjectType(nodeIds[0]) == uQuery.UmbracoObjectType.Media){
                        xPathCheckBoxListPickerContent = umbHelper.TypedMedia(nodeIds).Where(x => x != null);
                    }
                    else if (uQuery.GetUmbracoObjectType(nodeIds[0]) == uQuery.UmbracoObjectType.Member)
                    {
                        //Revist this when Members are supported by IPublishedContent
                        return Attempt<object>.Fail(); 
                    }
                    else
                    {
                        return Attempt<object>.Fail();
                    }
                }

                if (xPathCheckBoxListPickerContent != null)
                {
                    xPathCheckBoxListPicker = xPathCheckBoxListPickerContent;
                }             

                return Attempt<object>.Succeed(xPathCheckBoxListPicker);
            }
            else
            {
                return Attempt<object>.Fail();
            }
        }
    }
}
