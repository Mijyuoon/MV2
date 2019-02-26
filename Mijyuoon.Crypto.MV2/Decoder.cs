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
            var resOut = new ByteBuffer();

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
                    for(int i = 0; !resIn.IsEOF; i++) {
                        var (rlen, rmap) = TransformTable.DecodeFlag(flagIn);
                        byte val = rmap[resIn.Read(rlen)];

                        // Unscramble byte values after MV2 decoding
                        val = key.XorByte(key.Decode(val, kset), i);
                        resOut.Write(val);
                    }

                    // Skip flag padding
                    flagIn.FinishByte();

                    // Forward the residual to the next round
                    residual = resOut.GetBytesInternal();
                    resLength = resOut.Position;
                    resOut.Reset();
                }
            } catch(ArgumentException ex) {
                throw new DecoderException(ex);
            } catch(IndexOutOfRangeException ex) {
                throw new DecoderException(ex);
            }

            // Trim final output
            var output = new byte[resLength];
            Array.Copy(residual, output, resLength);

            return output;
        }
    }
}
