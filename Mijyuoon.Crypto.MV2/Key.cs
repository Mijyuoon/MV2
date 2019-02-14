using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mijyuoon.Crypto.MV2.KDF;

namespace Mijyuoon.Crypto.MV2 {
    public class Key {
        internal KeyData Data { get; }

        public Key(byte[] key) {
            var kdf = new NullKDF(); // TODO: Replace with real key derivation function
            Data = new KeyData(kdf);
        }
    }
}
