using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mijyuoon.Crypto.MV2.Utils;

namespace Mijyuoon.Crypto.MV2.Tests.Utils {
    [TestClass]
    public class BitWriteStreamTests {
        [TestMethod]
        public void WriteOneBit() {
            var bs = new BitWriteStream(4);

            uint expected = 0b10010011_11001001;

            for(ulong b = expected; b != 0; b >>= 1) {
                bs.Write(b, 1);
            }

            uint value = BitConverter.ToUInt32(bs.GetBytes(), 0);
            Assert.AreEqual<uint>(expected, value);
        }

        [TestMethod]
        public void WriteSimple() {
            var bs = new BitWriteStream(4);

            bs.Write(0b_____101, 3);
            bs.Write(0b_1001110, 7);
            bs.Write(0b____0010, 4);

            uint value = BitConverter.ToUInt32(bs.GetBytes(), 0);
            Assert.AreEqual<uint>(0b_0010_1001110_101, value);
        }

        [TestMethod]
        public void WriteExpand() {
            var bs = new BitWriteStream(1);

            bs.Write(0b_____101, 3);
            bs.Write(0b_1001110, 7);
            bs.Write(0b____0010, 4);
            bs.Write(0b11111111, 8);

            uint value = BitConverter.ToUInt32(bs.GetBytes(), 0);
            Assert.AreEqual<uint>(0b_11111111_0010_1001110_101, value);
        }
    }
}
