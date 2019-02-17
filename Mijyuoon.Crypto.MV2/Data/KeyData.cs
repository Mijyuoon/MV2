using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mijyuoon.Crypto.MV2.KDF;

namespace Mijyuoon.Crypto.MV2 {
    internal class KeyData {
        public const int KeySets = 32;

        private Random prng;

        private byte[,] encodeLUT;
        private byte[,] decodeLUT;

        public KeyData(BaseKDF kdf) {
            prng = new Random();

            encodeLUT = new byte[KeySets, 256];
            kdf.FillTables(encodeLUT);

            decodeLUT = new byte[KeySets, 256];
            for(int ki = 0; ki < KeySets; ki++) {
                for(int i = 0; i < 256; i++) {
                    byte ev = encodeLUT[ki, i];
                    decodeLUT[ki, ev] = (byte)i;
                }
            }
        }

        public byte Encode(byte val, int kset) => encodeLUT[kset % KeySets, val];

        public byte Decode(byte val, int kset) => decodeLUT[kset % KeySets, val];

        public int GetNextKeyset() => prng.Next(KeySets);
    }
}
