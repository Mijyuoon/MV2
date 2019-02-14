using System;
using System.Collections.Generic;
using System.Linq;
using Mijyuoon.Crypto.MV2.Utils;

namespace Mijyuoon.Crypto.MV2.Data {
    internal class PrefixDecoder<T> {
        private int maxDepth;
        private (bool, T)[] nodes;

        public PrefixDecoder(IEnumerable<(BitValue, T)> values) {
            maxDepth = values.Max(x => x.Item1.Length);
            nodes = new(bool, T)[1 << (maxDepth + 1)];

            foreach(var (bits, val) in values) {
                ulong node = 0, bval = bits.Value;

                for(int i = 0; i < bits.Length; i++) {
                    node = (bval & 1) != 0 ?
                        (node * 2) + 2 : (node * 2) + 1;
                    bval >>= 1;
                }

                nodes[node] = (true, val);
            }
        }

        public T Decode(BitReadStream brs) {
            ulong node = 0;

            for(int i = 0; i < maxDepth; i++) {
                node = brs.Read() ?
                    (node * 2) + 2 : (node * 2) + 1;

                var (ok, value) = nodes[node];
                if(ok) return value;
            }

            throw new KeyNotFoundException("Invalid prefix code");
        }
    }
}
