// Implementation of unsigned sum of absolute difference instructions

namespace Humerus
{
    public static partial class Disassembler
    {
        internal static string UnsignedSumAbsoluteDifference(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var raIndex = opcode.ExtractBits(12, 15);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(8, 11));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));

            if (raIndex == 0xF)
                return $"USAD8{cond} {rd}, {rn}, {rm}";

            var ra = DisassemblerUtils.RegisterName(raIndex);
            return $"USADA8{cond} {rd}, {rn}, {rm}, {ra}";
        }
    }
}
