using System.Collections.Generic;

namespace libmonkey.utils
{
    public interface IPeekableEnumerable<T> : IEnumerable<T>
    {
        IPeekableEnumerator<T> GetPeekableEnumerator();
    }
}