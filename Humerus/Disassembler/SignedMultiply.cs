// Implementation of dual signed multiply instructions

namespace Humerus
{
    public static partial class Disassembler
    {
        internal static string SignedDualMultiply(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var raIndex = opcode.ExtractBits(12, 15);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(8, 11));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var swap = opcode.IsBitSet(5) ? "X" : "";

            // Technically bits 5-7, but only 6-7 are relevant here.
            var op2 = opcode.ExtractBits(6, 7);

            var name = "";
            if (op2 == 0)
            {
                if (raIndex == 0xF)
                    name = "SMUAD";
                else
                    name = "SMLAD";
            }
            else if (op2 == 1)
            {
                if (raIndex == 0xF)
                    name = "SMUSD";
                else
                    name = "SMLSD";
            }

            if (raIndex == 0xF)
                return $"{name}{swap}{cond} {rd}, {rn}, {rm}";

            var ra = DisassemblerUtils.RegisterName(raIndex);
            return $"{name}{swap}{cond} {rd}, {rn}, {rm}, {ra}";
        }

        internal static string SignedLongDualMultiply(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(8, 11));
            var rdlo = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rdhi = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var swap = opcode.IsBitSet(5) ? "X" : "";

            // Technically 5-7, but only 6-7 is relevant here.
            var op2 = opcode.ExtractBits(6, 7);
            var name = op2 == 0 ? "SMLALD" : "SMLSLD";

            return $"{name}{swap}{cond} {rdlo}, {rdhi}, {rn}, {rm}";
        }

        internal static string SignedMostSignificantWordMultiply(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(8, 11));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var raIndex = opcode.ExtractBits(12, 15);
            var round = opcode.IsBitSet(5) ? "R" : "";

            // Technically 5-7, but only 6-7 is relevant here.
            var op2 = opcode.ExtractBits(6, 7);

            if (op2 == 0 && raIndex == 0xF)
                return $"SMMUL{round}{cond} {rd}, {rn}, {rm}";

            var name = "";
            if (op2 == 0)
                name = "SMMLA";
            else if (op2 == 3)
                name = "SMMLS";

            var ra = DisassemblerUtils.RegisterName(raIndex);
            return $"{name}{round}{cond} {rd}, {rn}, {rm}, {ra}";
        }
    }
}
