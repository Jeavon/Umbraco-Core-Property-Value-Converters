using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Our.Umbraco.PropertyConverters.Utilities
{
    using System.Diagnostics;

    public static class ConverterHelper
    {
        public static bool DynamicInvocation()
        {
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
