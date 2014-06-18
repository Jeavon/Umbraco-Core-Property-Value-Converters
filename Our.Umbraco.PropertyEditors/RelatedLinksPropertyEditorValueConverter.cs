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
using System.Xml;
using umbraco;
namespace Our.Umbraco.PropertyEditorConverters
{
    public class RelatedLinksPropertyEditorValueConverter : IPropertyEditorValueConverter
    {
        string _propertyTypeAlias = string.Empty;
        string _docTypeAlias = string.Empty;

        public bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            _propertyTypeAlias = propertyTypeAlias;
            _docTypeAlias = docTypeAlias;
            return Guid.Parse("71b8ad1a-8dc2-425c-b6b8-faa158075e63").Equals(propertyEditorId);
        }
        public Attempt<object> ConvertPropertyValue(object value)
        {
            if (UmbracoContext.Current != null)
            {
                return Attempt<object>.Succeed(new RelatedLinksList(value.ToString()));
            }
            else
            {
                return Attempt<object>.Fail();
            }
        }

    }

    public class RelatedLinksList : List<RelatedLinkItem>
    {
        private readonly string _propertyData;

        public RelatedLinksList(string propertyData)
        {
            _propertyData = propertyData;

            if (!string.IsNullOrEmpty(PropertyData))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(PropertyData);

                foreach (XmlNode item in doc.SelectNodes("descendant::links/*"))
                {
                    this.Add(new RelatedLinkItem(item));
                }
            }
        }

        public string PropertyData
        {
            get
            {
                return _propertyData;
            }
        }
    }

    public class RelatedLinkItem{
        private string _title;
        private string _newWindow;
        private string _link;
        private string _type;
        private readonly XmlNode _xmlNode;

        public RelatedLinkItem(XmlNode xml)
        {
            _xmlNode = xml;
        }

        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(_title))
                {
                    _title = _xmlNode.Attributes["title"].Value;
                }
                return _title;
            }
        }

        public string NewWindow
        {
            get
            {
                if (string.IsNullOrEmpty(_newWindow))
                {
                    _newWindow = _xmlNode.Attributes["newwindow"].Value;
                }
                return _newWindow;
            }
        }

        public string Type
        {
            get
            {
                if (string.IsNullOrEmpty(_type))
                {
                    _type = _xmlNode.Attributes["type"].Value;
                }
                return _type;
            }
        }

        public string Link
        {
            get
            {
                if (string.IsNullOrEmpty(_link))
                {
                    if (this.Type.Equals("external"))
                    {
                        _link = _xmlNode.Attributes["link"].Value;
                    }
                    else if (this.Type.Equals("internal"))
                    {
                        var umbHelper = new UmbracoHelper(UmbracoContext.Current);
                        _link = umbHelper.NiceUrl(int.Parse(_xmlNode.Attributes["link"].Value));
                    }
                }
                return _link;
            }
        }

    }
}
