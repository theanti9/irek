using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace libirek.Urls
{
    public class UrlMap<T> : ICollection<T> where T : UrlMapItem
    {
        protected ArrayList _innerArray;
        protected bool _IsReadOnly;
        
        public UrlMap()
        {
            _innerArray = new ArrayList();
        }

        public virtual T this[int index]
        {
            get
            {
                return (T)_innerArray[index];
            }
            set
            {
                _innerArray[index] = value;
            }
        }

        public virtual int Count
        {
            get
            {
                return _innerArray.Count;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return _IsReadOnly;
            }
        }

        public virtual void Add(T urlmapitem)
        {
            _innerArray.Add(urlmapitem);
        }

        public virtual bool Remove(T urlmapitem)
        {
            bool result = false;

            for (int i = 0; i < _innerArray.Count; i++)
            {
                T obj = (T)_innerArray[i];

                if (obj.UrlPattern == urlmapitem.UrlPattern)
                {
                    _innerArray.RemoveAt(i);
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Contains(T urlmapitem)
        {
            foreach (T obj in _innerArray)
            {
                if (obj.UrlPattern == urlmapitem.UrlPattern)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void CopyTo(T[] urlmapitemarray, int index)
        {
            throw new Exception("Method not used for this implementatioin!");
        }

        public virtual void Clear()
        {
            _innerArray.Clear();
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return new UrlMapEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new UrlMapEnumerator<T>(this);
        }

        /// <summary>
        /// Finds the matching method.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The MethodCall object for the URL.</returns>
        public MethodCall FindMatchingMethod(string url)
        {
            foreach (T obj in _innerArray)
            {
                if (obj.IsMatch(url))
                {
                    return obj.GetMethodCall(url);
                }
            }
            return null;
        }
    }

    public class UrlMapEnumerator<T> : IEnumerator<T> where T : UrlMapItem
    {
        protected UrlMap<T> _collection;
        protected int index;
        protected T _current;

        public UrlMapEnumerator()
        {

        }

        public UrlMapEnumerator(UrlMap<T> collection)
        {
            _collection = collection;
            index = -1;
            _current = default(T);
        }

        public virtual T Current
        {
            get
            {
                return _current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return _current;
            }
        }

        public virtual void Dispose()
        {
            _collection = null;
            _current = default(T);
            index = -1;
        }

        public virtual bool MoveNext()
        {
            if (++index >= _collection.Count)
            {
                return false;
            }
            else
            {
                _current = _collection[index];
            }
            return true;
        }

        public virtual void Reset()
        {
            _current = default(T);

            index = -1;
        }
    }
}
