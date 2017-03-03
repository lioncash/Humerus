// Implementation of bit reversal instructions

namespace Humerus
{
    public static partial class Disassembler
    {
        internal static string RBIT(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));

            return $"RBIT{cond} {rd}, {rm}";
        }

        internal static string ByteReverse(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var op1 = opcode.ExtractBits(20, 22);
            var op2 = opcode.ExtractBits(5, 7);
            var name = "";


            if (op1 == 7 && op2 == 5)
            {
                name = "REVSH";
            }
            else if (op1 == 3)
            {
                if (op2 == 1)
                    name = "REV";
                else if (op2 == 5)
                    name = "REV16";
            }

            return $"{name}{cond} {rd}, {rm}";
        }
    }
}
