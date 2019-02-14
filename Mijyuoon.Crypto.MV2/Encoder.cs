using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Mijyuoon.Crypto.MV2.Data;
using Mijyuoon.Crypto.MV2.Utils;

namespace Mijyuoon.Crypto.MV2 {
    public struct EncodeResult {
        public byte[] Flag;
        public byte[] Residual;
    }

    public class Encoder {
        internal const int BlockSize = 16;
        internal const int RandomBits = 8;

        private KeyData key;
        private int rounds;

        private Random prng;

        public Encoder(Key key, int rounds = 16) {
            this.key = key.Data;
            this.rounds = rounds;
            
            this.prng = new Random();
        }

        public EncodeResult Encode(byte[] data) {
            /*
            if(data.Length % BlockSize != 0) {
                throw new ArgumentException("Data size must be multiple of block size", nameof(data));
            }
            */

            var flagOut = new BitWriteStream[rounds];
            var resOut = new BitWriteStream();

            // Actual data length
            int dataLength = data.Length;

            // Perform specified number of rounds
            for(int ri = 0; ri < rounds; ri++) {
                flagOut[ri] = new BitWriteStream();

                // Placeholder for header
                resOut.Write(0UL, 8);

                // Generate key set number
                int kset = prng.Next(KeyData.KeySets);

                // Permute bits on per-block basis
                for(int ofs = 0; ofs < dataLength; ofs += BlockSize) {
                    PermuteBlock(data, ofs, kset);
                }

                // Perform MV2 substitutions
                for(int i = 0; i < dataLength; i++) {
                    byte val = key.Encode(data[i], kset);
                    var (res, flag) = TransformTable.Encode(val);

                    resOut.Write(res);
                    flagOut[ri].Write(flag);
                }

                // Num. free bits in last byte
                long fbits = 8 - resOut.Position % 8;

                // Fill the header
                resOut[0] = (byte)(fbits | (kset << 3));

                // Forward the residual to next round
                data = resOut.GetBytesInternal();
                dataLength = resOut.BytesWritten;
                resOut.Reset();

                // KLUDGE: Pad the flag bitstream to full byte
                flagOut[ri].FinishByte();
            }

            // Concatenated flag buffer
            var flagFinal = new byte[flagOut.Sum(x => x.BytesWritten)];

            // Concatenate flag streams into final array
            int offset = 0;
            for(int i = flagOut.Length - 1; i >= 0; i--) {
                int len = flagOut[i].BytesWritten;
                Array.Copy(flagOut[i].GetBytesInternal(), 0, flagFinal, offset, len);
                offset += len;
            }

            // Trim the residual
            var resFinal = new byte[dataLength];
            Array.Copy(data, resFinal, dataLength);

            return new EncodeResult { Residual = resFinal, Flag = flagFinal };
        }

        private void PermuteBlock(byte[] block, int offset, int kset) {
            // TODO
        }
    }
}
