namespace Humerus
{
    // Synchronization primitives
    public static partial class Disassembler
    {
        internal static string SWP(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rt2 = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var b = opcode.IsBitSet(22) ? "B" : "";

            return $"SWP{b}{cond} {rt}, {rt2}, [{rn}]";
        }

        internal static string LoadExclusive(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rtIndex = opcode.ExtractBits(12, 15);
            var rt = DisassemblerUtils.RegisterName(rtIndex);
            var op = opcode.ExtractBits(20, 23);

            if (op == 11)
            {
                var rt2 = DisassemblerUtils.RegisterName(rtIndex + 1);
                return $"LDREXD{cond} {rt}, {rt2}, [{rn}]";
            }

            var name = "";
            switch (op)
            {
            case 9:
                name = "LDREX";
                break;
            case 13:
                name = "LDREXB";
                break;
            case 15:
                name = "LDREXH";
                break;
            }

            return $"{name}{cond} {rt}, [{rn}]";
        }

        internal static string StoreExclusive(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rtIndex = opcode.ExtractBits(0, 3);
            var rt = DisassemblerUtils.RegisterName(rtIndex);
            var op = opcode.ExtractBits(20, 23);

            if (op == 10)
            {
                var rt2 = DisassemblerUtils.RegisterName(rtIndex + 1);
                return $"STREXD{cond} {rd}, {rt}, {rt2}, [{rn}]";
            }

            var name = "";
            switch (op)
            {
            case 8:
                name = "STREX";
                break;
            case 12:
                name = "STREXB";
                break;
            case 14:
                name = "STREXH";
                break;
            }

            return $"{name}{cond} {rd}, {rt}, [{rn}]";
        }
    }
}
