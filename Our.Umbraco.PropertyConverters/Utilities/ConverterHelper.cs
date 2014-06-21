using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Our.Umbraco.PropertyConverters.Utilities
{
    using System.Configuration;
    using System.Diagnostics;

    public static class ConverterHelper
    {
        public static bool DynamicInvocation()
        {

            var mode = ConfigurationManager.AppSettings["Our.Umbraco.CoreValueConverters:Mode"];

            if (mode != null)
            {
                if (mode == "typed")
                {
                    return false;
                }
                if (mode == "dynamic")
                {
                    return true;
                }
            }

            var st = new StackTrace();

            var invokedTypes = st.GetFrames().Select(x =>
            {
                var declaringType = x.GetMethod().DeclaringType;
                return declaringType != null ? declaringType.Name : null;
            }).ToList();

            return invokedTypes.Contains("DynamicPublishedContent");
        }
    }
}
