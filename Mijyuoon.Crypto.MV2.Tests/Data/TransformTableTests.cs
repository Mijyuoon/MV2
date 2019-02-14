using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mijyuoon.Crypto.MV2.Data;
using Mijyuoon.Crypto.MV2.Utils;

namespace Mijyuoon.Crypto.MV2.Tests.Data {
    [TestClass]
    public class TransformTableTests {
        [TestMethod]
        public void Encode() {
            var data = (new BitValue(0b__00011, 5), new BitValue(0b__10, 2)); // val = 0x23

            Assert.AreEqual(data, TransformTable.Encode(0x23));
        }

        [TestMethod]
        public void DecodeFlag() {
            var bytes = BitConverter.GetBytes(0b10011_10); // val = 0x33
            var bs = new BitReadStream(bytes);

            var (rlen, rmap) = TransformTable.DecodeFlag(bs);
            Assert.AreEqual<int>(5, rlen);

            var rval = (int)bs.Read(rlen);
            Assert.AreEqual<byte>(0x33, rmap[rval]);
        }
    }
}
