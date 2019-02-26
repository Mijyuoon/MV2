using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Mijyuoon.Crypto.MV2.Utils;

namespace Mijyuoon.Crypto.MV2.KDF {
    class KDF256 : BaseKDF {
        private readonly uint[] T = new[] {
            0x726a8f3bU, 0xe69a3b5cU, 0xd3c71fe5U, 0xab3c73d2U,
            0x4d3a8eb3U, 0x0396d6e8U, 0x3d4c2f7aU, 0x9ee27cf3U,
        };

        private byte[] key;

        public KDF256(byte[] key) {
            Debug.Assert(key.Length == 32);
            this.key = key;
        }

        public override unsafe void FillTables(byte[,] lut) => FillTables(lut, new uint[256]);

        public unsafe override void FillTables(byte[,] lut, uint[] kx) {
            uint rol(uint x, int n) =>
                (x << n) | (x >> (32 - n));

            uint g(uint x2, uint x1, uint x0) =>
                (x2 & x1) ^ (x1 & x0) ^ (x0 & x2);

            uint gr(uint x2, int n2, uint x1, int n1, uint x0, int n0, int nr) =>
                rol(g(rol(x2, n2), rol(x1, n1), rol(x0, n0)), nr);

            fixed (byte* kptr8 = key) {
                var kptr = (uint*)kptr8;

                for(int i = 0; i < 8; i++) {
                    kx[i] = kptr[i];
                }

                for(int i = 8; i < 256; i++) {
                    uint s0 = (((kx[i - 4] ^ kx[i - 3]) & kx[i - 2]) ^ kx[i - 3]) + T[0] + rol(kx[i - 1], 13);
                    uint s1 = (((kx[i - 8] ^ kx[i - 7]) & kx[i - 6]) ^ kx[i - 7]) + T[1] + rol(kx[i - 5], 13);
                    uint s2 = (((kx[i - 6] ^ kx[i - 5]) & kx[i - 4]) ^ kx[i - 5]) + T[2] + rol(kx[i - 3], 13);
                    uint s3 = (((kx[i - 1] ^ kx[i - 2]) & kx[i - 8]) ^ kx[i - 2]) + T[3] + rol(kx[i - 7], 13);
                    uint s4 = (((kx[i - 8] ^ kx[i - 6]) & kx[i - 4]) ^ kx[i - 6]) + T[4] + rol(kx[i - 2], 13);
                    uint s5 = (((kx[i - 7] ^ kx[i - 5]) & kx[i - 3]) ^ kx[i - 5]) + T[5] + rol(kx[i - 1], 7);
                    uint s6 = T[6] + 
                        ((kx[i - 7] & kx[i - 4])
                        ^ (kx[i - 6] & kx[i - 2]) 
                        ^ (kx[i - 5] & kx[i - 3]) 
                        ^ (kx[i - 2] & kx[i - 1])
                        ^ kx[i - 1]);
                    uint s7 = T[7] +
                        ((kx[i - 8] & kx[i - 6] & kx[i - 4] & kx[i - 2])
                        ^ (kx[i - 8] & kx[i - 7] & kx[i - 6] & kx[i - 5])
                        ^ (kx[i - 5] & kx[i - 4] & kx[i - 3] & kx[i - 2])
                        ^ kx[i - 4]);
                    uint s8 = gr(s2, 0, s3, 0, s4, 0, 2);
                    uint s9 = gr(s0, 0, s5, 0, s6, 0, 11);
                    uint s10 = gr(s1, 0, s2, 14, s7, 0, 13);
                    uint s11 = gr(s3, 6, s4, 4, s5, 12, 9);
                    uint s12 = gr(s0, 7, s1, 17, s6, 20, 3);
                    uint s13 = gr(s2, 10, s4, 12, s7, 16, 7);
                    uint s14 = gr(s0, 13, s3, 7, s5, 11, 16);
                    uint s15 = gr(s1, 5, s6, 12, s7, 10, 5);
                    uint x = s8 ^ s9 ^ s10 ^ s11 ^ s12 ^ s13 ^ s14 ^ s15;
                    x = x + rol(x, 11);
                    x = x ^ rol(x, 5);
                    kx[i] = x;
                }

                for(int i = 0; i < 256; i++) {
                    kx[i] = kx[i] ^ (kx[(i + 23) & 255] + kx[kx[(i + 19) & 255] & 255]);
                    for(int j = 0; j < 32; j++) {
                        lut[j, i] = (byte)i;
                    }
                }

                for(int i = 255; i > 0; i--) {
                    uint x = kx[i] + rol(kx[(i + 113) & 255], 11);
                    uint s0 = 0, s1 = 0, s2 = 0, s3 = 0, s4 = 0, s5 = 0, s6 = 0, s7 = 0;

                    for(int j = 0; j < 32; j++) {
                        int k = (int)(x & 255) % (i + 1);
                        byte t = lut[j, k];
                        lut[j, k] = lut[j, i];
                        lut[j, i] = t;

                        s0 = g(s0, s2, rol(kx[(i + j + 47) & 255], (j + 3) & 15));
                        s7 = g(s7, s1, rol(kx[(i + j + 59) & 255], (j + 3) & 15));
                        s4 = g(s4, s3, rol(kx[(i + j + 67) & 255], (j + 3) & 15));
                        s6 = g(s6, s5, rol(kx[(i + j + 73) & 255], (j + 3) & 15));
                        s2 = g(s2, s1, rol(kx[(i + j + 83) & 255], (j + 3) & 15));
                        s3 = g(s3, s5, rol(kx[(i + j + 97) & 255], (j + 3) & 15));
                        s5 = g(s5, s1, rol(kx[(i + j + 103) & 255], (j + 3) & 15));
                        s1 = g(s1, s0, rol(kx[(i + j + 109) & 255], (j + 3) & 15));
                        s3 = s3 - s0;
                        s6 = s6 + s2;
                        s7 = s7 ^ s5;
                        s1 = s1 - s4;
                        s0 = s0 - s7;
                        s4 = s4 ^ s6;
                        s2 = s2 + s3;
                        s5 = s3 + s1;
                        s1 = s1 + rol(s7, 11);
                        s3 = s3 ^ rol(s6, 17);
                        s2 = s2 - rol(s5, 13);
                        s0 = s0 + rol(s4, (int)s3 & 15);
                        s1 = s1 ^ s3;
                        s0 = s0 ^ rol(s2, 5);
                        x = x * (x >> 1) + ((x >> 17) ^ rol(kx[(i + j + 127) & 255], j & 15) ^ (s1 + s0));
                    }
                }
            }
        }
    }
}
