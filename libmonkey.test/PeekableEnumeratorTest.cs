using System.Collections;
using System.Collections.Generic;
using libmonkey.utils;
using NUnit.Framework;

namespace libmonkey.test
{
    [TestFixture]
    public class PeekableEnumeratorTest
    {
        [Test]
        public void WrapList()
        {
            var list = new List<string>(new[] {"1", "2", "3"});
            var sut = new PeekableEnumerator<string>(list.GetEnumerator());

            Assert.AreEqual(3, list.Count, "This function assumes a list with 3 elements");
            Assert.IsNull(sut.Current);
            Assert.IsTrue(sut.MoveNext());
            Assert.AreEqual("1", sut.Current);
            Assert.AreEqual("1", ((IEnumerator)sut).Current);
            Assert.IsTrue(sut.MoveNext());
            Assert.AreEqual("2", sut.Current);
            Assert.IsTrue(sut.MoveNext());
            Assert.AreEqual("3", sut.Current);
            Assert.IsFalse(sut.MoveNext());
            sut.Reset();
            Assert.IsNull(sut.Current);
            Assert.IsTrue(sut.MoveNext());
            Assert.AreEqual("1", sut.Current);
        }

        [Test]
        public void PeekNext()
        {
            var list = new List<string>(new[] {"1", "2", "3"});
            var sut = new PeekableEnumerator<string>(list.GetEnumerator());

            CompareListToPeekableEnumerator(list, sut);
        }

        [Test]
        public void WrapEmptyList()
        {
            var list = new List<string>();
            var sut = new PeekableEnumerator<string>(list.GetEnumerator());

            Assert.IsNull(sut.Current);
            Assert.IsNull(((IEnumerator)sut).Current);
            Assert.IsNull(sut.PeekNext);

            Assert.IsFalse(sut.MoveNext());
            sut.Reset();
            Assert.IsNull(sut.Current);
        }

        [Test]
        public void ListIncludesNull()
        {
            var list = new List<string>(new[] {"1", "2", null});
            var sut = new PeekableEnumerator<string>(list.GetEnumerator());

            CompareListToPeekableEnumerator(list, sut);
        }

        private class YieldReturnEnum : IEnumerable<string>
        {
            public IEnumerator<string> GetEnumerator()
            {
                yield return "1";
                yield return "2";
                yield return "3";
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [Test]
        public void EnumerateYieldReturn()
        {
            var list = new List<string>(new[] {"1", "2", "3"});
            var sut = new PeekableEnumerator<string>(new YieldReturnEnum().GetEnumerator());

            CompareListToPeekableEnumerator(list, sut);
        }

        [Test]
        public void ThrowsResetUnavailable()
        {
            var sut = new PeekableEnumerator<string>(new YieldReturnEnum().GetEnumerator());

            Assert.Throws<System.NotSupportedException>(() => sut.Reset());
        }

        private void CompareListToPeekableEnumerator(List<string> list, IPeekableEnumerator<string> enumerator)
        {
            Assert.AreEqual(list[0], enumerator.PeekNext);
            Assert.IsNull(enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(list[0], enumerator.Current);
            Assert.AreEqual(list[0], ((IEnumerator)enumerator).Current);
            Assert.AreEqual(list[1], enumerator.PeekNext);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(list[2], enumerator.PeekNext);
            Assert.AreEqual(list[1], enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.IsNull(enumerator.PeekNext);
            Assert.AreEqual(list[2], enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
            Assert.IsNull(enumerator.Current);
            Assert.IsNull(enumerator.PeekNext);

            try
            {
                enumerator.Reset();
                Assert.IsNull(enumerator.Current);
                Assert.AreEqual(list[0], enumerator.PeekNext);
                Assert.IsTrue(enumerator.MoveNext());
                Assert.AreEqual(list[0], enumerator.Current);
            }
            catch (System.NotSupportedException)
            {
                // ignore if enumerator.Reset() is unavailable
            }
        }
    }
}