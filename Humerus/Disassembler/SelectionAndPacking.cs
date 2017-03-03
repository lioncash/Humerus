// Implementation of select and pack instructions

namespace Humerus
{
    public static partial class Disassembler
    {
        internal static string SEL(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));

            return $"SEL{cond} {rd}, {rn}, {rm}";
        }

        internal static string PKH(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var imm5 = opcode.ExtractBits(7, 11);

            var tb_bit = opcode.ExtractBit(6);
            var form = tb_bit == 1 ? "TB" : "BT";
            var shift = DisassemblerUtils.DecodeImmediateShift(tb_bit << 1, imm5);

            return $"PKH{form}{cond} {rd}, {rn}, {rm}{shift}";
        }
    }
}
