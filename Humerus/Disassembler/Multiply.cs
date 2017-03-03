// Implementation of multiply instructions

namespace Humerus
{
    public static partial class Disassembler
    {
        internal static string Multiply(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var raIndex = opcode.ExtractBits(12, 15);
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(8, 11));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var update = opcode.IsBitSet(20) ? "S" : "";

            if (raIndex == 0)
                return $"MUL{update}{cond} {rd}, {rn}, {rm}";

            var ra = DisassemblerUtils.RegisterName(raIndex);

            if (opcode.IsBitSet(22))
                return $"MLS{cond} {rd}, {rn}, {rm}, {ra}";

            return $"MLA{update}{cond} {rd}, {rn}, {rm}, {ra}";
        }

        internal static string MultiplyLong(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rdlo = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rdhi = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(8, 11));

            var update = opcode.IsBitSet(20) ? "S" : "";
            var name   = opcode.IsBitSet(21) ? "MLAL" : "MULL";
            var sign   = opcode.IsBitSet(22) ? 'S' : 'U';

            return $"{sign}{name}{update}{cond} {rdlo}, {rdhi}, {rn}, {rm}";
        }

        internal static string UMAAL(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rdhi = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rdlo = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 16));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(8, 11));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));

            return $"UMAAL{cond} {rdlo}, {rdhi}, {rn}, {rm}";
        }
    }
}
