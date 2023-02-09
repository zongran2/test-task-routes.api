using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;

namespace TestTask.Services
{
    public class CacheDictionary<TKey, TValue>
    {
        ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim();
        Dictionary<TKey, LazyInit<TValue>> _cacheItemDictionary = new Dictionary<TKey, LazyInit<TValue>>();

        public TValue Fetch(TKey key, Func<TValue> producer)
        {
            LazyInit<TValue> cacheItem;
            bool found;

            _cacheLock.EnterReadLock();
            try
            {
                found = _cacheItemDictionary.TryGetValue(key, out cacheItem);
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }

            if (!found)
            {
                _cacheLock.EnterWriteLock();
                try
                {
                    if (!_cacheItemDictionary.TryGetValue(key, out cacheItem))
                    {
                        cacheItem = new LazyInit<TValue>(producer);
                        _cacheItemDictionary.Add(key, cacheItem);
                    }
                }
                finally
                {
                    _cacheLock.ExitWriteLock();
                }
            }

            return cacheItem.Value;
        }
    }
}

public class LazyInit<T>
 {
     Func<T> _producer;
     object _lock = new object();
     T _data;
     volatile bool _created;
  
     public LazyInit(Func<T> producer)
     {
         _producer = producer;
     }
  
     public T Value
     {
         get
         {
             if (!_created)
             {
                 lock (_lock)
                 {
                     if (!_created)
                     {
                         _data = _producer.Invoke();
                         _created = true;
                         _producer = null;
                     }
                 }
             }
             return _data;
         }
     }
 }

