using System.Diagnostics;

namespace Humerus
{
    /// <summary>
    /// Utility class for providing extension methods that deal with bit extraction.
    /// </summary>
    internal static class BitExtensions
    {
        // Kind of unfortunate arithmetic on generics isn't possible in a straightforward
        // manner in C#, otherwise most of this code could be much simpler.

        // Number of bits within a byte.
        private const int BitsInByte = 8;

        #region BitSize

        // Retrieves the size of an integral data type in bits.
        // The sizeof operator only returns size in bytes, which
        // is inadequate when doing bit level operations.

        public static int BitSize(this sbyte integral)
        {
            return sizeof(sbyte) * BitsInByte;
        }

        public static int BitSize(this byte integral)
        {
            return sizeof(byte) * BitsInByte;
        }

        public static int BitSize(this short integral)
        {
            return sizeof(short) * BitsInByte;
        }

        public static int BitSize(this ushort integral)
        {
            return sizeof(ushort) * BitsInByte;
        }

        public static int BitSize(this int integral)
        {
            return sizeof(int) * BitsInByte;
        }

        public static int BitSize(this uint integral)
        {
            return sizeof(uint) * BitsInByte;
        }

        public static int BitSize(this long integral)
        {
            return sizeof(long) * BitsInByte;
        }

        public static int BitSize(this ulong integral)
        {
            return sizeof(ulong) * BitsInByte;
        }

        #endregion

        #region ExtractBit

        // Extracts a single bit from a given integral value and returns it.

        public static sbyte ExtractBit(this sbyte integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (sbyte)((integral >> bit) & 1);
        }

        public static byte ExtractBit(this byte integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (byte)((integral >> bit) & 1);
        }

        public static short ExtractBit(this short integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (short)((integral >> bit) & 1);
        }

        public static int ExtractBit(this int integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (integral >> bit) & 1;
        }

        public static uint ExtractBit(this uint integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (integral >> bit) & 1;
        }

        public static long ExtractBit(this long integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (integral >> bit) & 1;
        }

        public static ulong ExtractBit(this ulong integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (integral >> bit) & 1;
        }

        #endregion

        #region ExtractBits

        // Extracts a range of bits from a given type.

        public static sbyte ExtractBits(this sbyte integral, int start, int end)
        {
            Debug.Assert(start < end && end < integral.BitSize());

            return (sbyte)(integral << ((integral.BitSize() - 1) - end) >> (integral.BitSize() - end + start - 1));
        }

        public static byte ExtractBits(this byte integral, int start, int end)
        {
            Debug.Assert(start < end && end < integral.BitSize());

            return (byte)(integral << ((integral.BitSize() - 1) - end) >> (integral.BitSize() - end + start - 1));
        }

        public static short ExtractBits(this short integral, int start, int end)
        {
            Debug.Assert(start < end && end < integral.BitSize());

            return (short)(integral << ((integral.BitSize() - 1) - end) >> (integral.BitSize() - end + start - 1));
        }

        public static ushort ExtractBits(this ushort integral, int start, int end)
        {
            Debug.Assert(start < end && end < integral.BitSize());

            return (ushort)(integral << ((integral.BitSize() - 1) - end) >> (integral.BitSize() - end + start - 1));
        }

        public static int ExtractBits(this int integral, int start, int end)
        {
            Debug.Assert(start < end && end < integral.BitSize());

            return integral << ((integral.BitSize() - 1) - end) >> (integral.BitSize() - end + start - 1);
        }

        public static uint ExtractBits(this uint integral, int start, int end)
        {
            Debug.Assert(start < end && end < integral.BitSize());

            return integral << ((integral.BitSize() - 1) - end) >> (integral.BitSize() - end + start - 1);
        }

        public static long ExtractBits(this long integral, int start, int end)
        {
            Debug.Assert(start < end && end < integral.BitSize());

            return integral << ((integral.BitSize() - 1) - end) >> (integral.BitSize() - end + start - 1);
        }

        public static ulong ExtractBits(this ulong integral, int start, int end)
        {
            Debug.Assert(start < end && end < integral.BitSize());

            return integral << ((integral.BitSize() - 1) - end) >> (integral.BitSize() - end + start - 1);
        }

        #endregion

        #region IsBitSet

        // Function that simply checks if a bit is set or not.

        public static bool IsBitSet(this sbyte integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize() - 1);

            return ExtractBit(integral, bit) == 1;
        }

        public static bool IsBitSet(this byte integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return ExtractBit(integral, bit) == 1;
        }

        public static bool IsBitSet(this short integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return ExtractBit(integral, bit) == 1;
        }

        public static bool IsBitSet(this ushort integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return ExtractBit(integral, bit) == 1;
        }

        public static bool IsBitSet(this int integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return ExtractBit(integral, bit) == 1;
        }

        public static bool IsBitSet(this uint integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return ExtractBit(integral, bit) == 1;
        }

        public static bool IsBitSet(this long integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return ExtractBit(integral, bit) == 1;
        }

        public static bool IsBitSet(this ulong integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return ExtractBit(integral, bit) == 1;
        }

        #endregion

        #region SignExtend

        // Function that manually performs sign extension on all integral types.
        // This makes it more indicative when sign extension is required, and also
        // allows simulating sign-extension on unsigned types, which is beneficial
        // when dealing with opcodes, as it'll reduce the amount of times where casts
        // are needed.

        public static sbyte SignExtend(this sbyte integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (sbyte)SignExtend((short)integral, bit);
        }

        public static byte SignExtend(this byte integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (byte)SignExtend((ushort)integral, bit);
        }

        public static short SignExtend(this short integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (short)SignExtend((int)integral, bit);
        }

        public static ushort SignExtend(this ushort integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (ushort)SignExtend((uint)integral, bit);
        }

        public static int SignExtend(this int integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (int)SignExtend((long)integral, bit);
        }

        public static uint SignExtend(this uint integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            return (uint)SignExtend((ulong)integral, bit);
        }

        public static long SignExtend(this long integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            var mask = (long)((1UL << bit) - 1);
            var signBit = IsBitSet(integral, bit - 1);

            if (signBit)
                return integral | ~mask;

            return integral;
        }

        public static ulong SignExtend(this ulong integral, int bit)
        {
            Debug.Assert(bit >= 0 && bit < integral.BitSize());

            var mask = (1UL << bit) - 1;
            var signBit = IsBitSet(integral, bit - 1);

            if (signBit)
                return integral | ~mask;

            return integral;
        }

        #endregion

        #region SignBitToChar

        // Uses the value of the sign bit in the type (note: *not* the signedness of the actual type)
        // and uses that value to return either '+' or '-' (when the sign bit is zero or one, respectively).

        public static char SignBitToChar(this sbyte val)
        {
            return val < 0 ? '-' : '+';
        }

        public static char SignBitToChar(this byte val)
        {
            return val.IsBitSet(val.BitSize() - 1) ? '-' : '+';
        }

        public static char SignBitToChar(this short val)
        {
            return val < 0 ? '-' : '+';
        }

        public static char SignBitToChar(this ushort val)
        {
            return val.IsBitSet(val.BitSize() - 1) ? '-' : '+';
        }

        public static char SignBitToChar(this int val)
        {
            return val < 0 ? '-' : '+';
        }

        public static char SignBitToChar(this uint val)
        {
            return val.IsBitSet(val.BitSize() - 1) ? '-' : '+';
        }

        public static char SignBitToChar(this long val)
        {
            return val < 0 ? '-' : '+';
        }

        public static char SignBitToChar(this ulong val)
        {
            return val.IsBitSet(val.BitSize() - 1) ? '-' : '+';
        }

        #endregion

        #region Rotate Left

        public static byte RotateLeft(this byte value, int count)
        {
            if (count == 0)
                return value;

            return (byte)((value << count) | (value >> (value.BitSize() - count)));
        }

        public static ushort RotateLeft(this ushort value, int count)
        {
            if (count == 0)
                return value;

            return (ushort)((value << count) | (value >> (value.BitSize() - count)));
        }

        public static uint RotateLeft(this uint value, int count)
        {
            if (count == 0)
                return value;

            return (value << count) | (value >> (value.BitSize() - count));
        }

        public static ulong RotateLeft(this ulong value, int count)
        {
            if (count == 0)
                return value;

            return (value << count) | (value >> (value.BitSize() - count));
        }

        #endregion

        #region Rotate Right

        public static byte RotateRight(this byte value, int count)
        {
            if (count == 0)
                return value;

            return (byte)((value >> count) | (value << (value.BitSize() - count)));
        }

        public static ushort RotateRight(this ushort value, int count)
        {
            if (count == 0)
                return value;

            return (ushort)((value >> count) | (value << (value.BitSize() - count)));
        }

        public static uint RotateRight(this uint value, int count)
        {
            if (count == 0)
                return value;

            return (value >> count) | (value << (value.BitSize() - count));
        }

        public static ulong RotateRight(this ulong value, int count)
        {
            if (count == 0)
                return value;

            return (value >> count) | (value << (value.BitSize() - count));
        }

        #endregion
    }
}
