using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mijyuoon.Crypto.MV2.Utils {
    internal class ByteBuffer {
        const float GrowFactor = 1.5f;

        private int elptr;
        private byte[] data;

        public ByteBuffer(int capacity) {
            Reset(capacity);
        }

        public ByteBuffer() : this(4) {}

        public int Position => elptr;

        public int Length => data.Length;

        public byte[] GetBytes() {
            var res = new byte[Position];
            Array.Copy(data, res, res.Length);
            return res;
        }

        public byte[] GetBytesInternal() => data;

        public void Write(byte[] buf) {
            EnsureSize(buf.Length);

            Array.Copy(buf, 0, data, elptr, buf.Length);
            elptr += buf.Length;
        }

        public void Write(byte value) {
            EnsureSize(1);
            data[elptr++] = value;
        }

        public void Reset(int capacity) {
            if(capacity < 4) {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            this.elptr = 0;
            this.data = new byte[capacity];
        }

        public void Reset() => Reset(4);

        private void EnsureSize(int extra = 0) {
            if(elptr + extra >= data.Length) {
                Array.Resize(ref data, (int)(data.Length * GrowFactor));
            }
        }
    }
}
