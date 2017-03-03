// Implementation of halfword multiply instructions

namespace Humerus
{
    public static partial class Disassembler
    {
        internal static string HalfwordMultiply(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(8, 11));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var op = opcode.ExtractBit(5);
            var op1 = opcode.ExtractBits(21, 22);

            var x = op == 1 ? 'T' : 'B';
            var y = opcode.IsBitSet(6) ? 'T' : 'B';

            if (op1 == 3)
                return $"SMUL{x}{y}{cond} {rd}, {rn}, {rm}";

            if (op1 == 1 && op == 1)
                return $"SMULW{y}{cond} {rd}, {rn}, {rm}";

            var ra = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            if (op1 == 1 && op == 0)
                return $"SMLAW{y}{cond} {rd}, {rn}, {rm}, {ra}";

            if (op1 == 0)
                return $"SMLA{x}{y}{cond} {rd}, {rn}, {rm}, {ra}";

            return $"SMLAL{x}{y}{cond} {ra}, {rd}, {rn}, {rm}";
        }
    }
}
