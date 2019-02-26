using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Mijyuoon.Crypto.MV2.KDF;

namespace Mijyuoon.Crypto.MV2 {
    internal class KeyData {
        public const int KeySets = 32;

        private Random prng;

        private byte[] xorBytes;
        private byte[,] encodeLUT;
        private byte[,] decodeLUT;

        public KeyData(BaseKDF kdf) {
            prng = new Random();

            var kext = new uint[256];
            encodeLUT = new byte[KeySets, 256];
            kdf.FillTables(encodeLUT, kext);

            decodeLUT = new byte[KeySets, 256];
            for(int ki = 0; ki < KeySets; ki++) {
                for(int i = 0; i < 256; i++) {
                    byte ev = encodeLUT[ki, i];
                    decodeLUT[ki, ev] = (byte)i;
                }
            }

            xorBytes = new byte[1024];
            unsafe {
                fixed(uint* pkey = kext) {
                    Marshal.Copy((IntPtr)pkey, xorBytes, 0, xorBytes.Length);
                }
            }
        }

        public byte Encode(byte val, int kset) => encodeLUT[kset % KeySets, val];

        public byte Decode(byte val, int kset) => decodeLUT[kset % KeySets, val];

        public byte XorByte(byte val, int key) => (byte)(val ^ xorBytes[key & 1023]);

        public int GetNextKeyset() => prng.Next(KeySets);
    }
}
