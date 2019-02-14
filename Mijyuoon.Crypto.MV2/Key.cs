using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Mijyuoon.Crypto.MV2.KDF;

namespace Mijyuoon.Crypto.MV2 {
    public enum KeySize {
        KNull = 0,
        K256 = 32,
    }

    public class Key {
        internal KeyData Data { get; }

        public Key(byte[] key) {
            BaseKDF kdf;

            switch((KeySize)key.Length) {
            case KeySize.KNull:
                kdf = new NullKDF();
                break;
            case KeySize.K256:
                kdf = new KDF256(key);
                break;
            default:
                throw new ArgumentException("Invalid key length", nameof(key));
            }

            Data = new KeyData(kdf);
        }

        public static byte[] Generate(KeySize size) {
            var outKey = new byte[(int)size];
            using(var rng = new RNGCryptoServiceProvider()) {
                rng.GetBytes(outKey);
            }
            return outKey;
        }
    }
}
