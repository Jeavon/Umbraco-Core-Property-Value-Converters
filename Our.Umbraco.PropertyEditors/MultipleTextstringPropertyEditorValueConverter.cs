using System;
using System.Collections.Generic;
using System.Xml;
using Umbraco.Core;
using Umbraco.Core.PropertyEditors;

namespace Our.Umbraco.PropertyEditorConverters
{
    public class MultipleTextstringPropertyEditorValueConverter : IPropertyEditorValueConverter
    {
        string _propertyTypeAlias = string.Empty;
        string _docTypeAlias = string.Empty;

        public bool IsConverterFor(Guid propertyEditorId, string docTypeAlias, string propertyTypeAlias)
        {
            _propertyTypeAlias = propertyTypeAlias;
            _docTypeAlias = docTypeAlias;
            return Guid.Parse("5359ad0b-06cc-4182-92bd-0a9117448d3f").Equals(propertyEditorId);
        }

        public Attempt<object> ConvertPropertyValue(object value)
        {
            var list = new List<string>();

            if (!string.IsNullOrEmpty(value.ToString()))
            {
                var xml = new XmlDocument();
                xml.LoadXml(value.ToString());

                var values = xml.SelectNodes("/values/value");

                if (values != null)
                {
                    foreach (XmlNode node in values)
                    {
                        list.Add(node.InnerText);
                    }

                    return Attempt<object>.Succeed(list);
                }
            }

            return Attempt<object>.Succeed(null);
        }
    }
}