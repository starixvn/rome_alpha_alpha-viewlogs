namespace Alpha.ViewLogs.Services
{
    using System;
    using Fanex.Caching;

    public interface CachingManager
    {
        void Add<T>(string key, T item);

        void Add<T>(string key, T item, DateTimeOffset absoluteExpiration);

        void Add<T>(string key, T item, TimeSpan slidingExpiration);

        void Add<T>(string key, T item, CacheItemOptions cacheItemOptions);

        T Get<T>(string key);

        T GetOrAdd<T>(string key, Func<T> addItemFactory);

        T GetOrAdd<T>(string key, Func<T> addItemFactory, DateTimeOffset absoluteExpiration);

        T GetOrAdd<T>(string key, Func<T> addItemFactory, TimeSpan slidingExpiration);

        T GetOrAdd<T>(string key, Func<T> addItemFactory, CacheItemOptions cacheItemOptions);

        void Remove(string key);
    }
}