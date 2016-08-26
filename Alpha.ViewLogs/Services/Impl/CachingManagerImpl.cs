namespace Alpha.ViewLogs.Services.Impl
{
    using System;
    using Fanex.Caching;

    /// <summary>
    ///  <see cref="https://github.com/alastairtree/LazyCache"/>
    /// </summary>
    public class CachingManagerImpl : CachingManager
    {
        public CachingManagerImpl()
        {
            DefaultCacheDuration = 60 * 20;
        }

        public int DefaultCacheDuration { get; set; }

        private DateTimeOffset DefaultExpiryDateTime
        {
            get
            {
                return DateTimeOffset.Now.AddSeconds(DefaultCacheDuration);
            }
        }

        public void Add<T>(string key, T item)
        {
            Add(key, item, DefaultExpiryDateTime);
        }

        public void Add<T>(string key, T item, DateTimeOffset absoluteExpiration)
        {
            Add(key, item, new CacheItemOptions { AbsoluteExpiration = absoluteExpiration });
        }

        public void Add<T>(string key, T item, TimeSpan slidingExpiration)
        {
            Add(key, item, new CacheItemOptions { SlidingExpiration = slidingExpiration });
        }

        public void Add<T>(string key, T item, CacheItemOptions cacheItemOptions)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            ValidateKey(key);

            CacheService.Set(key, item, cacheItemOptions);
        }

        public T Get<T>(string key)
        {
            ValidateKey(key);

            var item = CacheService.Get<T>(key);

            if (item is T)
            {
                return (T)item;
            }

            return default(T);
        }

        public T GetOrAdd<T>(string key, Func<T> addItemFactory)
        {
            return GetOrAdd(key, addItemFactory, DefaultExpiryDateTime);
        }

        public T GetOrAdd<T>(string key, Func<T> addItemFactory, DateTimeOffset absoluteExpiration)
        {
            return GetOrAdd(key, addItemFactory, new CacheItemOptions { AbsoluteExpiration = absoluteExpiration });
        }

        public T GetOrAdd<T>(string key, Func<T> addItemFactory, TimeSpan slidingExpiration)
        {
            return GetOrAdd(key, addItemFactory, new CacheItemOptions { SlidingExpiration = slidingExpiration });
        }

        public T GetOrAdd<T>(string key, Func<T> addItemFactory, CacheItemOptions cacheItemOptions)
        {
            ValidateKey(key);

            var existingCacheItem = CacheService.GetOrSet<T>(key, addItemFactory, cacheItemOptions);

            if (existingCacheItem != null)
            {
                if (existingCacheItem is T)
                {
                    return existingCacheItem;
                }

                if (existingCacheItem is Func<T>)
                {
                    return addItemFactory();
                }

                return default(T);
            }

            try
            {
                return addItemFactory();
            }
            catch
            {
                // addItemFactory errored so do not cache the exception
                CacheService.Remove(key);
                throw;
            }
        }

        public void Remove(string key)
        {
            ValidateKey(key);
            CacheService.Remove(key);
        }

        private void ValidateKey(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentOutOfRangeException(nameof(key), @"Cache keys cannot be empty or whitespace");
            }
        }
    }
}