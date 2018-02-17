using System.Collections;
using System.Collections.Generic;

namespace libmonkey.utils
{
    /// <summary>
    /// Simple implementation of IPeekableEnumerator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    ///
    /// note: fails when members are null or wrapped type throws exceptions
    public class PeekableEnumerator<T> : IPeekableEnumerator<T> where T : class
    {
        private readonly IEnumerator<T> _inner;
        private T _current;

        public PeekableEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
            _current = null;
            _inner.MoveNext();
        }

        public bool MoveNext()
        {
            _current = _inner.Current;
            bool next = _inner.MoveNext();
            return next || _current != null;
        }

        public void Reset()
        {
            _inner.Reset();
            _current = null;
            _inner.MoveNext();
        }

        public T Current => _current;

        object IEnumerator.Current => Current;

        public T PeekNext => _inner.Current;

        public void Dispose()
        {
            _inner.Dispose();
        }
    }
}