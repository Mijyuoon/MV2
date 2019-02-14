using System;

namespace Mijyuoon.Crypto.MV2.Utils {
    internal class BitReadStream {
        private int bptr;
        private int elptr;
        private byte[] data;
        private long ilen;

        public BitReadStream(byte[] data, long length) {
            Reset(data, length);
        }

        public BitReadStream(byte[] data) {
            Reset(data);
        }

        public long Position => bptr + elptr * 8;

        public long Length => ilen;

        public bool IsEOF => Position >= Length;

        public byte[] GetBytesInternal() => data;

        public ulong Read(int count) {
            if(count < 1 || count > 64) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if(Position + count > Length) {
                throw new ArgumentException($"Cannot read past the end of stream");
            }

            ulong accum = 0;
            int len = count;

            if(bptr > 0) {
                int n = Math.Min(count, 8 - bptr);
                accum |= GetBits(n);
                count -= n;
            }
            while(count >= 8) {
                accum |= GetBits(8) << (len - count);
                count -= 8;
            }
            if(count > 0) {
                accum |= GetBits(count) << (len - count);
            }

            return accum;
        }

        public bool Read() => GetBits(1) != 0;

        public void Seek(int count, bool relative) {
            long rel = relative ? Position : 0;
            if(count < 1 || count >= Length - rel) {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if(!relative) {
                bptr = 0;
                elptr = 0;
            }

            Advance(count);
        }

        public void Reset() {
            this.bptr = 0;
            this.elptr = 0;
        }

        public void Reset(byte[] data, long length) {
            if(length > data.Length * 8) {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            this.bptr = 0;
            this.elptr = 0;
            this.data = data;
            this.ilen = length;
        }

        public void Reset(byte[] data) => Reset(data, data.Length * 8);

        public void FinishByte() {
            if(bptr == 0) return;

            bptr = 0;
            elptr++;
        }

        private void Advance(int n) {
            elptr += (bptr + n) / 8;
            bptr = (bptr + n) % 8;
        }

        private uint GetBits(int n) {
            uint x = data[elptr];
            uint v = (x >> bptr) & ((1U << n) - 1U);

            Advance(n);
            return v;
        }
    }
}
