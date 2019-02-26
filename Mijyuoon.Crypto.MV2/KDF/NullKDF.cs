using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mijyuoon.Crypto.MV2.KDF {
    class NullKDF : BaseKDF {
        public override void FillTables(byte[,] lut) {
            for(int ki = 0; ki < KeyData.KeySets; ki++) {
                for(int j = 0; j < 256; j++) {
                    lut[ki, j] = (byte)j;
                }
            }
        }

        public override void FillTables(byte[,] lut, uint[] kext) {
            FillTables(lut);

            for(int j = 0; j < 256; j++) {
                kext[j] = 0U;
            }
        }
    }
}
