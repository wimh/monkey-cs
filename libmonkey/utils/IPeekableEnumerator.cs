using System.Collections.Generic;

namespace libmonkey.utils
{
    public interface IPeekableEnumerator<T> : IEnumerator<T>
    {
        T PeekNext { get; }
    }
}