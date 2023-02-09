using System.Collections;
using System.Collections.Concurrent;
using System.Linq;
using TestTask.Data;
using Route = TestTask.Data.Route;

namespace TestTask.Services
{
    public class LocalStorageService : IEnumerable<Route>
    {

        ConcurrentDictionary<Guid, Route> routesDictionary = new ConcurrentDictionary<Guid, Route>();
        ConcurrentDictionary<Route, Guid> routeReversDictionary = new ConcurrentDictionary<Route, Guid>();

        ReaderWriterLockSlim _storageLock = new ReaderWriterLockSlim();
        public LocalStorageService() { }

        public Route Add(Route route)
        {
            _storageLock.EnterWriteLock();
            try
            {
                if (!routeReversDictionary.ContainsKey(route))
                {
                    route.Id = Guid.NewGuid();
                    routeReversDictionary.TryAdd(route, route.Id);
                    routesDictionary.TryAdd(route.Id, route);
                }

                return routesDictionary[routeReversDictionary[route]];
            }
            finally { _storageLock.ExitWriteLock(); }
        }

        public IEnumerable<Route> AddRange(IEnumerable<Route> routes)
        {
            _storageLock.EnterWriteLock();
            try
            {
                foreach (var route in routes)
                {
                    if (!routeReversDictionary.ContainsKey(route))
                    {
                        route.Id = Guid.NewGuid();
                        routeReversDictionary.TryAdd(route, route.Id);
                        routesDictionary.TryAdd(route.Id, route);
                    }

                    yield return routesDictionary[routeReversDictionary[route]];
                }
            }
            finally { _storageLock.ExitWriteLock(); }
        }

        public Route? Get(Guid id)
        {
            _storageLock.EnterReadLock();
            try
            {
                routesDictionary.TryGetValue(id, out Route? val); return val;
            }
            finally { _storageLock.ExitReadLock(); }

        }

        public IEnumerator<Route> GetEnumerator()
        {
            _storageLock.EnterReadLock();
            try
            {
                return routesDictionary.Values.GetEnumerator();
            }
            finally { _storageLock.ExitReadLock(); }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            _storageLock.EnterReadLock();
            try
            {
                return routesDictionary.Values.GetEnumerator();
            }
            finally { _storageLock.ExitReadLock(); }
        }
    }
}
