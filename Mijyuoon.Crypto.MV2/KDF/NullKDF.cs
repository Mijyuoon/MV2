using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mijyuoon.Crypto.MV2.KDF {
    class NullKDF : BaseKDF {
        public override void FillTables(byte[,] lut) {
            for(int ki = 0; ki < lut.GetLength(1); ki++) {
                for(int i = 0; i < lut.GetLength(0); i++) {
                    lut[i, ki] = (byte)i;   
                }
            }
        }
    }
}
