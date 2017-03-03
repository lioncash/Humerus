using System.Diagnostics;

// Implementation of barrier instructions
namespace Humerus
{
    public static partial class Disassembler
    {
        private static string BarrierOptionToString(uint option)
        {
            switch (option)
            {
            case 2:
                return "OSHST";
            case 3:
                return "OSH";
            case 6:
                return "NSHST";
            case 7:
                return "NSH";
            case 10:
                return "ISHST";
            case 11:
                return "ISH";
            case 14:
                return "ST";
            case 15:
                return "SY";
            }

            Debug.Assert(false);
            return "Invalid barrier option.";
        }

        internal static string Barrier(uint opcode)
        {
            var bits4And5 = opcode.ExtractBits(4, 5);
            var option = BarrierOptionToString(opcode.ExtractBits(0, 3));

            switch (bits4And5)
            {
            case 0:
                return $"DSB {option}";
            case 1:
                return $"DMB {option}";
            case 2:
                return $"ISB {option}";
            default: // Can never happen.
                Debug.Assert(false);
                return "Invalid barrier instruction";
            }
        }
    }
}
