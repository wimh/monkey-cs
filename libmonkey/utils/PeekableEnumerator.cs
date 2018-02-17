using System.Collections;
using System.Collections.Generic;

namespace libmonkey.utils
{
    /// <summary>
    /// Simple implementation of IPeekableEnumerator
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PeekableEnumerator<T> : IPeekableEnumerator<T> where T : class
    {
        private readonly IEnumerator<T> _inner;
        private T _current;
        private bool _canMoveNext;

        public PeekableEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
            _current = null;
            _canMoveNext = _inner.MoveNext();
        }

        public bool MoveNext()
        {
            if (!_canMoveNext)
            {
                _current = null;
                return false;
            }

            _current = _inner.Current;
            _canMoveNext = _inner.MoveNext();
            return true;
        }

        public void Reset()
        {
            _inner.Reset();
            _current = null;
            _canMoveNext = _inner.MoveNext();
        }

        public T Current => _current;

        object IEnumerator.Current => Current;

        public T PeekNext => !_canMoveNext ? null : _inner.Current;

        public void Dispose()
        {
            _inner.Dispose();
        }
    }
}