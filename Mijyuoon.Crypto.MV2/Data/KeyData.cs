using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mijyuoon.Crypto.MV2.KDF;

namespace Mijyuoon.Crypto.MV2 {
    internal class KeyData {
        public const int KeySets = 32;

        private byte[,] encodeLUT;
        private byte[,] decodeLUT;

        public KeyData(BaseKDF kdf) {
            encodeLUT = new byte[256, KeySets];
            kdf.FillTables(encodeLUT);

            decodeLUT = new byte[256, KeySets];
            for(int ki = 0; ki < KeySets; ki++) {
                for(int i = 0; i < 256; i++) {
                    byte ev = encodeLUT[i, ki];
                    decodeLUT[ev, ki] = (byte)i;
                }
            }
        }

        public byte Encode(byte val, int kset) => encodeLUT[val, kset % KeySets];

        public byte Decode(byte val, int kset) => decodeLUT[val, kset % KeySets];
    }
}
