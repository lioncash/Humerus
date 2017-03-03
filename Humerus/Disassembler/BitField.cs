// Implementation of bitfield instructions

namespace Humerus
{
    public static partial class Disassembler
    {
        internal static string BitFieldClearOrInsert(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rnIndex = opcode.ExtractBits(0, 3);
            var lsb = opcode.ExtractBits(7, 11);
            var msb = opcode.ExtractBits(16, 20);
            var width = msb - lsb + 1;

            if (rnIndex == 0xF)
                return $"BFC{cond} {rd}, #{lsb}, #{width}";

            var rn = DisassemblerUtils.RegisterName(rnIndex);
            return $"BFI{cond} {rd}, {rn}, #{lsb}, #{width}";
        }

        internal static string BitFieldExtract(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var lsb = opcode.ExtractBits(7, 11);
            var width = opcode.ExtractBits(16, 20) + 1;
            var name = opcode.IsBitSet(22) ? "UBFX" : "SBFX";

            return $"{name}{cond} {rd}, {rn}, #{lsb}, #{width}";
        }
    }
}
