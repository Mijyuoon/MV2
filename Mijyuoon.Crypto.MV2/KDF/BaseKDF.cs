using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mijyuoon.Crypto.MV2.KDF {
    abstract class BaseKDF {
        public abstract void FillTables(byte[,] lut);
        public abstract void FillTables(byte[,] lut, uint[] kext);
    }
}
