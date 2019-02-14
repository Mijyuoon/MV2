using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mijyuoon.Crypto.MV2.Utils {
    internal class BitWriteStream {
        const float GrowFactor = 1.5f;
        const ulong PaddingBits = 0b10010011;

        private int bptr;
        private int elptr;
        private byte[] data;

        public BitWriteStream(int capacity) {
            Reset(capacity);
        }

        public BitWriteStream() : this(4) {}

        public long Position => bptr + elptr * 8;

        public int BytesWritten => elptr + (bptr + 7) / 8;

        public long Length => data.Length * 8;

        public byte this[int bidx] {
            get => data[bidx];
            set => data[bidx] = value;
        }

        public byte[] GetBytes() {
            byte[] res = new byte[BytesWritten];
            Array.Copy(data, res, res.Length);
            return res;
        }

        public byte[] GetBytesInternal() => data;

        public void Write(ulong bits, int count) {
            if(count < 1 || count > 64) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            
            if(bptr > 0) {
                int n = Math.Min(count, 8 - bptr);
                AddBits(ref bits, n);
                count -= n;
            }
            while(count >= 8) {
                data[elptr] = (byte)bits;
                Advance(8, false);
                bits >>= 8;
                count -= 8;
            }
            if(count > 0) {
                AddBits(ref bits, count);
            }
        }
        
        public void Write(bool value) => Write(value ? 1U : 0U, 1);

        public void Write(BitValue bv) => Write(bv.Value, bv.Length);

        public void Seek(int count, bool relative) {
            long rel = relative ? Position : 0;
            if(count < 1 || count >= Length - rel) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if(!relative) {
                bptr = 0;
                elptr = 0;
            }

            Advance(count, true);
        }

        public void Reset(int capacity) {
            if(capacity < 4) {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            elptr = bptr = 0;
            data = new byte[capacity];
        }

        public void Reset() => Reset(4);

        public void FinishByte() {
            if(bptr == 0) return;

            ulong bits = PaddingBits;
            AddBits(ref bits, 8 - bptr);
        }

        private void Advance(int n, bool nogrow) {
            elptr += (bptr + n) / 8;
            bptr = (bptr + n) % 8;

            if(!nogrow) { EnsureSize(); }
        }

        private void EnsureSize(int extra = 0) {
            if(elptr + extra >= data.Length) {
                Array.Resize(ref data, (int)(data.Length * GrowFactor));
            }
        }

        private void PutBits(ulong b, int n) {
            ref byte v = ref data[elptr];
            byte bm = (byte)((1 << n) - 1);
            byte bv = (byte)((b & bm) << bptr);
            v = (byte)(v & ~(bm << bptr) | bv);
        }

        private void AddBits(ref ulong b, int n) {
            //EnsureSize();
            PutBits(b, n);
            Advance(n, false);
            b >>= n;
        }
    }
}
