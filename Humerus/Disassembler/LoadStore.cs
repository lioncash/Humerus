// Load/Store instruction implementations

namespace Humerus
{
    public static partial class Disassembler
    {
        private static string LoadStoreImmediate(uint opcode)
        {
            var indexed = opcode.IsBitSet(24);
            var add = opcode.IsBitSet(23);
            var writeback = opcode.IsBitSet(21);

            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var sign = add ? "" : "-";
            var isExtraLoad = (opcode & 0x0C000000) == 0;

            // If bits 26 and 27 are zero, it's an extra load/store instruction
            // which has a different immediate arrangement.
            var immediate = 0u;
            if (isExtraLoad)
                immediate = opcode.ExtractBits(8, 11) << 4 | opcode.ExtractBits(0, 3);
            else
                immediate = opcode.ExtractBits(0, 11);

            if (indexed)
            {
                // Pre-indexed variant
                if (writeback)
                    return $"[{rn}, #{sign}{immediate}]!";

                // Offset variant
                var offsetImmediateStr = immediate == 0 ? "" : $", #{sign}{immediate}";
                return $"[{rn}{offsetImmediateStr}]";
            }

            // Post-indexed variant.
            var postIndexedImmediateStr = immediate == 0 ? "" : $", #{sign}{immediate}";
            return $"[{rn}]{postIndexedImmediateStr}";
        }

        private static string LoadStoreRegister(uint opcode)
        {
            var indexed = opcode.IsBitSet(24);
            var add = opcode.IsBitSet(23);
            var writeback = opcode.IsBitSet(21);

            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var sign = add ? "" : "-";
            var isExtraStore = (opcode & 0x0C000000) == 0;

            // Extra load stores cannot perform shifts.
            var shift = "";
            if (!isExtraStore)
            {
                var shiftImm = opcode.ExtractBits(7, 11);
                var shiftType = opcode.ExtractBits(5, 6);
                shift = DisassemblerUtils.DecodeImmediateShift(shiftType, shiftImm);
            }

            if (indexed)
            {
                var writebackBang = writeback ? "!" : "";
                return $"[{rn}, {sign}{rm}{shift}]{writebackBang}";
            }

            return $"[{rn}], {sign}{rm}{shift}";
        }

        internal static string LDRImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDR{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDR{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string LDRTImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRT{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRTReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRT{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string LDRBImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRB{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRBReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRB{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string LDRBTImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRBT{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRBTReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRBT{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string LDRDImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rtIndex = opcode.ExtractBits(12, 15);
            var rt  = DisassemblerUtils.RegisterName(rtIndex);
            var rt2 = DisassemblerUtils.RegisterName(rtIndex + 1);

            return $"LDRD{cond} {rt}, {rt2}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRDReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rtIndex = opcode.ExtractBits(12, 15);
            var rt = DisassemblerUtils.RegisterName(rtIndex);
            var rt2 = DisassemblerUtils.RegisterName(rtIndex + 1);

            return $"LDRD{cond} {rt}, {rt2}, {LoadStoreRegister(opcode)}";
        }

        internal static string LDRHImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRH{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRHReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRH{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string LDRHTImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRHT{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRHTReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRHT{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string LDRSBImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRSB{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRSBReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRSB{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string LDRSBTImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRSBT{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRSBTReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRSBT{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string LDRSHImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRSH{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRSHReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRSH{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string LDRSHTImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRSHT{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string LDRSHTReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"LDRSHT{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string STRImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STR{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string STRReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STR{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string STRTImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STRT{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string STRTReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STRT{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string STRBImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STRB{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string STRBReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STRB{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string STRBTImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STRBT{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string STRBTReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STRBT{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string STRDImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rtIndex = opcode.ExtractBits(12, 15);
            var rt  = DisassemblerUtils.RegisterName(rtIndex);
            var rt2 = DisassemblerUtils.RegisterName(rtIndex + 1);

            return $"STRD{cond} {rt}, {rt2}, {LoadStoreImmediate(opcode)}";
        }

        internal static string STRDReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rtIndex = opcode.ExtractBits(12, 15);
            var rt = DisassemblerUtils.RegisterName(rtIndex);
            var rt2 = DisassemblerUtils.RegisterName(rtIndex + 1);

            return $"STRD{cond} {rt}, {rt2}, {LoadStoreRegister(opcode)}";
        }

        internal static string STRHImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STRH{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string STRHReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STRH{cond} {rt}, {LoadStoreRegister(opcode)}";
        }

        internal static string STRHTImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STRHT{cond} {rt}, {LoadStoreImmediate(opcode)}";
        }

        internal static string STRHTReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rt = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));

            return $"STRHT{cond} {rt}, {LoadStoreRegister(opcode)}";
        }
    }
}
