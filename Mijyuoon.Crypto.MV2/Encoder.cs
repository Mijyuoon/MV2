﻿using System;
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
        internal const int MinResidual = 8;

        private KeyData key;
        private int rounds;

        public Encoder(Key key, int rounds = 16) {
            this.key = key.Data;
            this.rounds = rounds;
        }

        public EncodeResult Encode(byte[] data) {
            var flagOut = new List<BitWriteStream>();
            var resOut = new BitWriteStream();

            // Actual data length
            int dataLength = data.Length;

            // Perform specified number of rounds
            for(int ri = 0; ri < rounds; ri++) {
                flagOut.Add(new BitWriteStream());

                // Placeholder for header
                resOut.Write(0UL, 8);

                // Generate key set number
                int kset = key.GetNextKeyset();

                unsafe {
                    // Use raw pointers to skip array bounds check
                    fixed (byte* dptr = data) {
                        // Perform MV2 substitutions
                        for(int i = 0; i < dataLength; i++) {
                            // Scramble byte values before MV2 lookup
                            byte val = key.Encode(key.XorByte(dptr[i], i), kset);
                            var (res, flag) = TransformTable.Encode(val);

                            resOut.Write(res);
                            flagOut[ri].Write(flag);
                        }
                    }
                }

                // Num. free bits in last byte
                long fbits = 8 - (resOut.Position & 7);

                // Fill the header
                resOut[0] = (byte)((fbits & 7) | (kset << 3));

                // Forward the residual to next round
                data = resOut.GetBytesInternal();
                dataLength = resOut.BytesWritten;
                resOut.Reset();

                // KLUDGE: Pad the flag bitstream to full byte
                flagOut[ri].FinishByte();

                // Terminate early if residual is too small
                if(dataLength <= MinResidual) break;
            }

            // Concatenate flag buffers into final array
            var flagFinal = BitWriteStream.Combine(flagOut.Reverse<BitWriteStream>());

            // Trim the residual
            var resFinal = new byte[dataLength];
            Array.Copy(data, resFinal, dataLength);

            return new EncodeResult { Residual = resFinal, Flag = flagFinal };
        }
    }
}
