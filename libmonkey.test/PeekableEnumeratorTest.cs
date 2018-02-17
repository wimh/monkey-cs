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

            Assert.AreEqual("1", sut.PeekNext);
            Assert.IsNull(sut.Current);
            Assert.IsTrue(sut.MoveNext());
            Assert.AreEqual("1", sut.Current);
            Assert.AreEqual("2", sut.PeekNext);
            Assert.IsTrue(sut.MoveNext());
            Assert.AreEqual("3", sut.PeekNext);
            Assert.IsTrue(sut.MoveNext());
            Assert.IsNull(sut.PeekNext);
            Assert.IsFalse(sut.MoveNext());
            sut.Reset();
            Assert.AreEqual("1", sut.PeekNext);
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
    }
}