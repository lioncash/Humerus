// Implementation of saturation arithmetic instructions

namespace Humerus
{
    public static partial class Disassembler
    {
        internal static string Saturate(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var satImm = opcode.ExtractBits(16, 20);
            var shiftImm = opcode.ExtractBits(7, 11);
            var shift = DisassemblerUtils.DecodeImmediateShift(opcode.ExtractBit(6) << 1, shiftImm);
            var name = "USAT";

            // SSAT encodes the saturate amount as 'amount - 1'. 
            if (!opcode.IsBitSet(22))
            {
                name = "SSAT";
                satImm += 1;
            }

            return $"{name}{cond} {rd}, #{satImm}, {rn}{shift}";
        }

        internal static string Saturate16(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var imm4 = opcode.ExtractBits(16, 19);
            var name = "USAT16";

            // SSAT16 encodes the saturate amount as 'amount - 1'.
            if (!opcode.IsBitSet(22))
            {
                name = "SSAT16";
                imm4 += 1;
            }

            return $"{name}{cond} {rd}, #{imm4}, {rn}";
        }
    }
}
