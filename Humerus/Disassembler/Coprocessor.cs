namespace Humerus
{
    // Disassembly methods for coprocessor instructions.
    public static partial class Disassembler
    {
        private static string GetCoprocessorConditionString(uint opcode)
        {
            // If the condition field bits are all set,
            // its an unconditional instruction which
            // is indicated by a 2 (i.e. CDP2, MCR2, etc),
            // otherwise it's an actual condition code.
            var condBits = opcode.ExtractBits(28, 31);
            if (condBits == 0xF)
                return "2";

            return DisassemblerUtils.ARMConditionCode(opcode);
        }

        internal static string CDP(uint opcode)
        {
            var condStr = GetCoprocessorConditionString(opcode);

            var opc1 = opcode.ExtractBits(20, 23);
            var opc2 = opcode.ExtractBits(5, 7);

            var crd = opcode.ExtractBits(12, 15);
            var crm = opcode.ExtractBits(0, 3);
            var crn = opcode.ExtractBits(16, 19);
            var coprocesor = opcode.ExtractBits(8, 11);

            var opc2Str = opc2 == 0 ? "" : $", {{{opc2}}}";

            return $"CDP{condStr} p{coprocesor}, {opc1}, cr{crd}, cr{crn}, cr{crm}{opc2Str}";
        }

        internal static string LoadStoreCoprocessor(uint opcode)
        {
            var condStr = GetCoprocessorConditionString(opcode);

            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var crd = opcode.ExtractBits(12, 15);
            var coprocessor = opcode.ExtractBits(8, 11);
            var imm = opcode.ExtractBits(0, 7) << 2;

            var indexed   = opcode.IsBitSet(24);
            var add       = opcode.IsBitSet(23);
            var sign      = add ? "" : "-";
            var dForm     = opcode.IsBitSet(22) ? "L" : "";
            var writeback = opcode.IsBitSet(21);
            var name      = opcode.IsBitSet(20) ? "LDC" : "STC";

            var suffix = "";
            if (indexed)
            {
                if (writeback)
                {
                    suffix = $"[{rn}, #{sign}{imm}]!";
                }
                else
                {
                    var immStr = imm == 0 ? "" : $", #{sign}{imm}";
                    suffix = $"[{rn}{immStr}]";
                }
            }
            else
            {
                if (writeback)
                    suffix = $"[{rn}], #{sign}{imm}";
                else if (add)
                    suffix = $"[{rn}], {{{imm}}}";
            }

            return $"{name}{condStr}{dForm} p{coprocessor}, cr{crd}, {suffix}";
        }

        internal static string MoveOneCoprocessorRegister(uint opcode)
        {
            var condStr = GetCoprocessorConditionString(opcode);

            var opc1 = opcode.ExtractBits(21, 23);
            var opc2 = opcode.ExtractBits(5, 7);

            var crm = opcode.ExtractBits(0, 3);
            var crn = opcode.ExtractBits(16, 19);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var coprocessor = opcode.ExtractBits(8, 11);

            // Either a move from an ARM register to a coprocessor register,
            // or a move from a coprocessor register to an ARM register.
            var name = opcode.IsBitSet(20) ? "MRC" : "MCR";
            var opc2Str = opc2 == 0 ? "" : $", {{{opc2}}}";

            return $"{name}{condStr} p{coprocessor}, {opc1}, {rt}, cr{crn}, cr{crm}{opc2Str}";
        }

        internal static string MoveTwoCoprocessorRegisters(uint opcode)
        {
            var condStr = GetCoprocessorConditionString(opcode);

            var opc1 = opcode.ExtractBits(4, 7);
            var crm = opcode.ExtractBits(0, 3);
            var coprocessor = opcode.ExtractBits(8, 11);

            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rt2 = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));

            var name = opcode.IsBitSet(20) ? "MRRC" : "MCRR";

            return $"{name}{condStr} p{coprocessor}, {opc1}, {rt}, {rt2}, cr{crm}";
        }
    }
}
