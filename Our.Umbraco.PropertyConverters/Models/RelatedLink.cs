namespace Our.Umbraco.PropertyConverters.Models
{
    using System;

    using Our.Umbraco.PropertyConverters.Models;

    using Newtonsoft.Json.Linq;

    using global::Umbraco.Web;

    public class RelatedLink
    {
        private string _caption;
        private bool? _newWindow = null;
        private bool? _isInternal = null;
        private string _link;
        private RelatedLinkType _type;
        private readonly JToken _linkItem;

        public RelatedLink(JToken linkItem)
        {
            _linkItem = linkItem;
        }

        public string Caption
        {
            get
            {
                if (string.IsNullOrEmpty(_caption))
                {
                    _caption = _linkItem.Value<string>("caption");
                }
                return _caption;
            }
        }

        public bool NewWindow
        {
            get
            {
                if (this._newWindow == null)
                {
                    this._newWindow = _linkItem.Value<bool>("newWindow");
                }
                return this._newWindow.GetValueOrDefault();
            }
        }

        public bool IsInternal
        {
            get
            {
                if (this._isInternal == null)
                {
                    this._isInternal = _linkItem.Value<bool>("isInternal");
                }
                return this._isInternal.GetValueOrDefault();
            }
        }

        public RelatedLinkType? Type
        {
            get
            {
                if (Enum.TryParse(_linkItem.Value<string>("type"),true, out _type))
                {
                    return _type;    
                }
                return null;
            }
        }

        public string Link
        {
            get
            {
                if (string.IsNullOrEmpty(_link))
                {
                    if (this.IsInternal)
                    {
                        if (UmbracoContext.Current == null) return null;
                        var umbHelper = new UmbracoHelper(UmbracoContext.Current);
                        _link = umbHelper.NiceUrl(_linkItem.Value<int>("internal"));
                    }
                    else
                    {
                        _link = _linkItem.Value<string>("link");
                    }
                }
                return _link;
            }
        }

    }

}
