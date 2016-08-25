using System;
using Umbraco.Core;

namespace Our.Umbraco.PropertyConverters.Cache
{
    internal static class LocalCache
    {
        internal static T GetLocalCacheItem<T>(string cacheKey)
        {
            var runtimeCache = ApplicationContext.Current.ApplicationCache.RuntimeCache;
            var cachedItem = runtimeCache.GetCacheItem(cacheKey);
            return (T)cachedItem;
        }

        internal static void InsertLocalCacheItem<T>(string cacheKey, Func<object> getCacheItem)
        {
            var runtimeCache = ApplicationContext.Current.ApplicationCache.RuntimeCache;
            runtimeCache.InsertCacheItem(cacheKey, getCacheItem);
        }

        internal static void ClearLocalCacheItem(string key)
        {
            var runtimeCache = ApplicationContext.Current.ApplicationCache.RuntimeCache;
            runtimeCache.ClearCacheItem(key);
        }
    }
}
