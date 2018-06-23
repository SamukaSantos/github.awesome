

using System;
using Microsoft.Extensions.Caching.Memory;

namespace GitHub.Awesome.Infra.Caching
{
    /// <summary>
    /// In-Memory Cache class that help speed up from continuous requests for the same resources, 
    /// or help alleviate issues due to poor network speed.
    /// </summary>
	public class SystemCache
	{
		#region Fields

		private static object obj = new object();
		private static IMemoryCache _cache;
		private static SystemCache _systemCache;

		#endregion

		#region Constructor

		private SystemCache(){}

		#endregion

		#region Methods

        /// <summary>
        /// System Cache instance.
        /// </summary>
        /// <value></value>
		public static SystemCache Current
		{
			get
			{
				lock(obj)
				{
					if (_cache == null)
					{
						_cache = new MemoryCache(new MemoryCacheOptions());
						_systemCache = new SystemCache();
					}

					return _systemCache;
				}
			}
		}

        /// <summary>
        /// Add elements to the cache.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="value">Value.</param>
        /// <param name="absoluteExpiry">Expiration DateTime.</param>
        /// <typeparam name="T">Generic parameter that represents the type of the value.</typeparam>
		public void Set<T>(string key, T value, DateTimeOffset absoluteExpiry)
        {
            _cache.Set(key, value, absoluteExpiry);
        }

        /// <summary>
        /// Fetch elements to the cache.
        /// </summary>
        /// <typeparam name="T">Generic parameter.</typeparam>
        /// <param name="key">Key.</param>
        /// <returns>Returns the fetched element(s).</returns>
        public T Get<T>(string key)
		{
			if (_cache.TryGetValue(key, out T value))
				return value;
			return default(T);
		}
			
        #endregion

	}
    
}

