using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mijyuoon.Crypto.MV2.Utils.Tests {
    [TestClass]
    public class BitReadStreamTests {
        [TestMethod]
        public void ReadOneBit() {
            var bytes = BitConverter.GetBytes(0b00110110_11001001);
            var bs = new BitReadStream(bytes);

            var expected = new[] {
                true, false, false, true, false, false, true, true,
                false, true, true, false, true, true, false, false,
            };

            foreach(var bit in expected) {
                Assert.AreEqual(bit, bs.Read());
            }
        }

        [TestMethod]
        public void ReadSimple() {
            var bytes = BitConverter.GetBytes(0b11001001);
            var bs = new BitReadStream(bytes);

            Assert.AreEqual<ulong>(0b1001, bs.Read(4));
            Assert.AreEqual<ulong>(0b1100, bs.Read(4));

            Assert.AreEqual(8, bs.Position);
        }

        [TestMethod]
        public void ReadMultiByte() {
            var bytes = BitConverter.GetBytes(0b11001001_11001001);
            var bs = new BitReadStream(bytes);

            Assert.AreEqual<ulong>(0b1001, bs.Read(4));
            Assert.AreEqual<ulong>(0b10011100, bs.Read(8));

            Assert.AreEqual(12, bs.Position);
        }

        [TestMethod]
        public void ReadOutOfRange() {
            var bytes = new[] { (byte)0xFF };
            var bs = new BitReadStream(bytes);

            bs.Read(5);

            try {
                bs.Read(4);
                Assert.Fail();
            } catch(ArgumentException) {
                // Should happen due to out of range read
                Assert.AreEqual<long>(5, bs.Position);
            }
        }
    }
}