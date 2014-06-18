using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Our.Umbraco.PropertyEditorConverters
{
    public class PropertyEditorConverterHelper
    {

        public IEnumerable<string> GetDataTypeSettings(string docTypeAlias, string propertyTypeAlias)
        {            
            return this.GetDataTypePreValues(docTypeAlias, propertyTypeAlias);
        }

        public IEnumerable<string> GetDataTypeSettings(string docTypeAlias, string propertyTypeAlias, char separator)
        {
            var preValue = this.GetDataTypePreValue(docTypeAlias, propertyTypeAlias);

            if (!string.IsNullOrWhiteSpace(preValue))
            {
                var settings = new List<string>(preValue.Split(separator));
                if (settings != null && settings.Any())
                {
                    return settings;
                }
            }
            return null;
        }

        public T GetDataTypeSettings<T>(string docTypeAlias, string propertyTypeAlias)
        {
            var preValue = this.GetDataTypePreValue(docTypeAlias, propertyTypeAlias);

            if (!string.IsNullOrWhiteSpace(preValue))
            {
                // TODO: An an option to choose the format of deserialization.
                var serializer = new JavaScriptSerializer();
                return serializer.Deserialize<T>(preValue);
            }

            return default(T);
        }

        private string GetDataTypePreValue(string docTypeAlias, string propertyTypeAlias)
        {
            var preValues = this.GetDataTypePreValues(docTypeAlias, propertyTypeAlias);

            if (preValues != null && preValues.Count() > 0)
            {
                return preValues.FirstOrDefault();
            }

            return null;
        }

        private IEnumerable<string> GetDataTypePreValues(string docTypeAlias, string propertyTypeAlias)
        {
            if (ApplicationContext.Current != null)
            {
                var cts = ApplicationContext.Current.Services.ContentTypeService;
                var ct = (ContentType)cts.GetContentType(docTypeAlias);
                var pt = ct.PropertyTypes.FirstOrDefault(x => x.Alias.InvariantEquals(propertyTypeAlias));
                var dts = ApplicationContext.Current.Services.DataTypeService;

                return dts.GetPreValuesByDataTypeId(pt.DataTypeDefinitionId);
            }

            return null;
        }
    }
}