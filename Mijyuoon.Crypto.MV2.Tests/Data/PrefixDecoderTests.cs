using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mijyuoon.Crypto.MV2.Data;
using Mijyuoon.Crypto.MV2.Utils;

namespace Mijyuoon.Crypto.MV2.Tests.Data {
    [TestClass]
    public class PrefixDecoderTests {
        [TestMethod]
        public void SimpleDecode() {
            var bytes = BitConverter.GetBytes(0b100000110110);
            var bs = new BitReadStream(bytes);

            var pd = new PrefixDecoder<int>(new[] {
                (new BitValue(0b100, 3), 1),
                (new BitValue(0b000, 3), 2),
                (new BitValue(0b_11, 2), 3),
                (new BitValue(0b_01, 2), 4),
                (new BitValue(0b_10, 2), 5),
            });

            Assert.AreEqual<int>(5, pd.Decode(bs));
            Assert.AreEqual<int>(4, pd.Decode(bs));
            Assert.AreEqual<int>(3, pd.Decode(bs));
            Assert.AreEqual<int>(2, pd.Decode(bs));
            Assert.AreEqual<int>(1, pd.Decode(bs));
        }
    }
}
