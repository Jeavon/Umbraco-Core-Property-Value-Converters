using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Our.Umbraco.PropertyConverters.Models
{
    using System.Collections;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Models;
    using global::Umbraco.Web;

    using Our.Umbraco.PropertyConverters.Utilities;

    public class MultiNodeTreePicker : IEnumerable<IPublishedContent>
    {
        private readonly List<IPublishedContent> _pickedNodes = new List<IPublishedContent>();

        public override string ToString()
        {
            return string.Join(",", _pickedNodes.Select(x => x.Id));
        }

        public MultiNodeTreePicker(int[] nodeIds)
        {
            if (UmbracoContext.Current != null)
            {
                var umbHelper = new UmbracoHelper(UmbracoContext.Current);

                if (nodeIds.Length > 0)
                {
                    var dynamicInvocation = ConverterHelper.DynamicInvocation();

                    var objectType = ApplicationContext.Current.Services.EntityService.GetObjectType(nodeIds[0]);

                    if (objectType == UmbracoObjectTypes.Document)
                    {
                        _pickedNodes = dynamicInvocation ? umbHelper.Content(nodeIds) : umbHelper.TypedContent(nodeIds).Where(x => x != null).ToList();
                    }
                    else if (objectType == UmbracoObjectTypes.Media)
                    {
                        _pickedNodes = dynamicInvocation ? umbHelper.Media(nodeIds) : umbHelper.TypedMedia(nodeIds).Where(x => x != null).ToList();
                    }
                    else if (objectType == UmbracoObjectTypes.Member)
                    {
                        var members = new List<IPublishedContent>();

                        foreach (var nodeId in nodeIds)
                        {
                            var member = umbHelper.TypedMember(nodeId);
                            if (member != null)
                            {
                                members.Add(dynamicInvocation ? member.AsDynamic() : member);
                            }
                        }

                        _pickedNodes = members;
                    }
                }
            }
            
        }

        public IEnumerator<IPublishedContent> GetEnumerator()
        {
            return _pickedNodes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
