using System;

namespace Mijyuoon.Crypto.MV2.Utils {
    internal struct BitValue {
        const int MAX_LENGTH = 64;

        public ulong Value { get; }
        public int Length { get; }

        public BitValue(ulong value, int length) {
            if(length < 0 || length > MAX_LENGTH) {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            ulong mask = (1UL << length) - 1UL;

            this.Value = value & mask;
            this.Length = length;
        }

        public BitValue Concat(BitValue rhs) {
            if(Length + rhs.Length > MAX_LENGTH) {
                throw new ArgumentException("Resulting BitValue is too long");
            }

            ulong val = (Value << rhs.Length) | rhs.Value;
            return new BitValue(val, Length + rhs.Length);
        }

        public static BitValue operator +(BitValue lhs, BitValue rhs) => lhs.Concat(rhs);

        public override string ToString() => $"b'{Convert.ToString((long)Value, 2).PadLeft(Length, '0')}";

        #region Equality Checking

        public override bool Equals(object obj) {
            if(!(obj is BitValue)) {
                return false;
            }

            var value = (BitValue)obj;
            return Value == value.Value &&
                   Length == value.Length;
        }

        public override int GetHashCode() {
            var hashCode = 1491549487;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + Length.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(BitValue value1, BitValue value2) {
            return value1.Equals(value2);
        }

        public static bool operator !=(BitValue value1, BitValue value2) {
            return !(value1 == value2);
        }

        #endregion
    }
}
