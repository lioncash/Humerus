namespace Humerus
{
    // Disassembly methods for hint-class instructions
    public static partial class Disassembler
    {
        internal static string DBG(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var option = opcode.ExtractBits(0, 3);

            return $"DBG{cond} #{option}";
        }

        internal static string NOP(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);

            return $"NOP{cond}";
        }

        internal static string PLDImm(uint opcode)
        {
            var imm = opcode.ExtractBits(0, 11);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var w = opcode.IsBitSet(22) ? "" : "W";
            var operand = opcode.IsBitSet(23) ? '+' : '-';

            return $"PLD{w} [{rn}, #{operand}0x{imm:X3}]";
        }

        internal static string PLDReg(uint opcode)
        {
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var w = opcode.IsBitSet(22) ? "" : "W";
            var operand = opcode.IsBitSet(23) ? '+' : '-';
            var shiftType = opcode.ExtractBits(5, 6);
            var imm5 = opcode.ExtractBits(7, 11);

            return $"PLD{w} [{rn}, {operand}{rm}{DisassemblerUtils.DecodeImmediateShift(shiftType, imm5)}]";
        }

        internal static string PLIImm(uint opcode)
        {
            var imm = opcode.ExtractBits(0, 11);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var op = opcode.IsBitSet(23) ? '+' : '-';

            return $"PLI [{rn}, #{op}0x{imm:X3}]";
        }

        internal static string PLIReg(uint opcode)
        {
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var shiftImm = opcode.ExtractBits(7, 11);
            var shiftType = opcode.ExtractBits(5, 6);
            var op = opcode.IsBitSet(23) ? '+' : '-';
            var shift = DisassemblerUtils.DecodeImmediateShift(shiftType, shiftImm);

            return $"PLI [{rn}, {op}{rm}{shift}]";
        }

        internal static string SEV(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);

            return $"SEV{cond}";
        }

        internal static string WFE(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);

            return $"WFE{cond}";
        }

        internal static string WFI(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);

            return $"WFI{cond}";
        }

        internal static string YIELD(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);

            return $"YIELD{cond}";
        }
    }
}
