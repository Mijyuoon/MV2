using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Mijyuoon.Crypto.MV2.Data;
using Mijyuoon.Crypto.MV2.Utils;

namespace Mijyuoon.Crypto.MV2 {
    public class DecoderException : Exception {
        public DecoderException(Exception inner) : base("MV2 decoding failed", inner) { }
    }

    public class Decoder {
        private KeyData key;

        public Decoder(Key key) {
            this.key = key.Data;
        }

        public byte[] Decode(byte[] flag, byte[] residual) {
            var flagIn = new BitReadStream(flag);
            var resOut = new ByteBuffer(Encoder.BlockSize);

            // Actual residual length
            int resLength = residual.Length;

            try {
                while(!flagIn.IsEOF) {
                    // Parse residual header
                    int kset = residual[0] >> 3;
                    int fbits = residual[0] & 7;

                    // Initialize residual reader and discard header
                    var resIn = new BitReadStream(residual, resLength * 8 - fbits);
                    resIn.Read(8);

                    // Perform inverse MV2 substitutions
                    while(!resIn.IsEOF) {
                        var (rlen, rmap) = TransformTable.DecodeFlag(flagIn);
                        byte val = key.Decode(rmap[resIn.Read(rlen)], kset);
                        resOut.Write(val);
                    }

                    // Skip flag padding
                    flagIn.FinishByte();

                    // Forward the residual to the next round
                    residual = resOut.GetBytesInternal();
                    resLength = resOut.Position;
                    resOut.Reset();

                    // Permute bits on per-block basis
                    for(int ofs = 0; ofs < resLength; ofs += Encoder.BlockSize) {
                        PermuteBlock(residual, ofs, kset);
                    }
                }
            } catch(ArgumentException ex) {
                throw new DecoderException(ex);
            }

            // Trim final output
            var output = new byte[resLength];
            Array.Copy(residual, output, resLength);

            return output;
        }

        private void PermuteBlock(byte[] block, int offset, int kset) {
            // This is bad, replace with proper code later
            int maxl = Math.Min(block.Length, offset + Encoder.BlockSize);
            for(int i = offset; i < maxl; i++) {
                block[i] ^= key.Encode((byte)((i << 11) & 0xff), kset);
            }
        }
    }
}
