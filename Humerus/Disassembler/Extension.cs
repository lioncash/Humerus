using System;

namespace Humerus
{
    // Extension instructions
    public static partial class Disassembler
    {
        private static string RotationToString(uint rotation)
        {
            switch (rotation)
            {
            case 0:
                return "";
            case 1:
                return ", ROR #8";
            case 2:
                return ", ROR #16";
            case 3:
                return ", ROR #24";
            }

            throw new ArgumentException("Invalid rotation value.", nameof(rotation));
        }

        internal static string Extend(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rotation = RotationToString(opcode.ExtractBits(10, 11));
            var op = opcode.ExtractBits(20, 22);

            var name = "";
            switch (op)
            {
            case 0:
                name = "SXTB16";
                break;
            case 2:
                name = "SXTB";
                break;
            case 3:
                name = "SXTH";
                break;
            case 4:
                name = "UXTB16";
                break;
            case 6:
                name = "UXTB";
                break;
            case 7:
                name = "UXTH";
                break;
            }

            return $"{name}{cond} {rd}, {rm}{rotation}";
        }

        internal static string ExtendAndAdd(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rotation = RotationToString(opcode.ExtractBits(10, 11));
            var op = opcode.ExtractBits(20, 22);

            var name = "";
            switch (op)
            {
            case 0:
                name = "SXTAB16";
                break;
            case 2:
                name = "SXTAB";
                break;
            case 3:
                name = "SXTAH";
                break;
            case 4:
                name = "UXTAB16";
                break;
            case 6:
                name = "UXTAB";
                break;
            case 7:
                name = "UXTAH";
                break;
            }

            return $"{name}{cond} {rd}, {rn}, {rm}{rotation}";
        }
    }
}
