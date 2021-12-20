using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSManual.Storing
{


    public class FetchCache<T>
    {
        public enum Status
        {
            Init,
            Fetching,
            Success,
            Error
        }

        public delegate Task<T> CacheFetcher();

        private const int cacheCapacity = 5;
        private Queue<string> keysCache = new Queue<string>();
        private Dictionary<string, T> cache = new Dictionary<string, T>();

        public T current { get; private set; }
        public Status type
        {
            get
            {
                return this._type;
            }
            private set
            {
                this._type = value;
                this.OnChange(this, value);
            }
        }

        private Status _type = Status.Init;

        public event EventHandler<Status> OnChange = delegate { };

        private CacheFetcher fetcher;

        public FetchCache(CacheFetcher fetcher)
        {
            this.fetcher = fetcher;
        }

        public async Task<T> GetState(params string[] keys)
        {
            var key = string.Join(".", keys);
            T state;

            if (cache.TryGetValue(key, out state))
            {
                return state;
            }

            this.type = Status.Fetching;

            // Fetching..
            try
            {
                state = await this.fetcher();
                this.type = Status.Success;

                if (keysCache.Count > cacheCapacity)
                {
                    var oldKey = keysCache.Dequeue();
                    this.cache.Remove(oldKey);
                }

                keysCache.Enqueue(key);
            }
            catch (Exception e)
            {
                this.type = Status.Error;
            }

            return state;
        }
    }

    public interface ICacheConnect
    {
        void CacheUpdate(List<CacheResource> updates);
        void CacheReset();
    }

    public abstract class CacheResource
    {
        public bool isDirty { get; private set; } = true;
        public CacheResource()
        {
            App.store.OnChanged += (o, e) =>
            {
                this.isDirty = false;
            };
        }
        public T Mutate<T>() where T : CacheResource
        {
            this.isDirty = true;
            return (T)this;
        }
        public void Mutate()
        {
            this.isDirty = true;
        }
    }

    public class Store
    {
        protected class CacheState
        {
            public bool isDirty;
        }

        protected List<CacheResource> cache = new List<CacheResource>();
        public event EventHandler<IEnumerable<CacheResource>> OnChange = delegate { };
        public event EventHandler OnChanged = delegate { };

        public T GetCache<T>() where T : CacheResource, new()
        {
            var cache = this.cache.Find(c => c is T) as T;
            if (cache == null)
            {
                cache = new T();
                this.cache.Add(cache);
            }
            return cache;
        }



        private bool isInvalidating { get; set; } = false;
        public int Invalidations { get; private set; } = 0;

        public void Invalidate(bool force = false)
        {


            Invalidations++;

            isInvalidating = true;
            var result = new List<CacheResource>();
            foreach (var pair in this.cache)
            {
                var value = pair;
                if (value.isDirty)
                    result.Add(value);
            }

            this.OnChange(this, result);

            this.OnChanged(this, null);
            Invalidations--;
        }
    }
}
