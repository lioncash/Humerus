namespace Humerus
{
    // Disassembly methods for miscellaneous-class ARM instructions
    public static partial class Disassembler
    {
        internal static string BKPT(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var imm32 = opcode.ExtractBits(8, 19) << 4 | opcode.ExtractBits(0, 3);

            return $"BKPT{cond} #0x{imm32:X4}";
        }

        internal static string CLREX(uint opcode)
        {
            return "CLREX";
        }

        internal static string CLZ(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));

            return $"CLZ{cond} {rd}, {rm}";
        }

        internal static string CPS(uint opcode)
        {
            var a = opcode.IsBitSet(8) ? "A" : "";
            var i = opcode.IsBitSet(7) ? "I" : "";
            var f = opcode.IsBitSet(6) ? "F" : "";
            var modeChange = opcode.IsBitSet(17);
            var imod = opcode.ExtractBits(18, 19);

            var imodStr = "";
            if (imod == 2)
                imodStr = "IE";
            else if (imod == 3)
                imodStr = "ID";

            var mode = opcode.ExtractBits(0, 4);
            var modeStr = "";

            // Mode change, but no interrupt changes
            if (modeChange && imod == 0)
                modeStr = $"#{mode}";
            else if (!modeChange)
                modeStr = "";
            else // Mode change and interrupt changes.
                modeStr = $", #{mode}";

            return $"CPS{imodStr} {a}{i}{f}{modeStr}";
        }

        internal static string ERET(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);

            return $"ERET{cond}";
        }

        internal static string HVC(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var imm32 = opcode.ExtractBits(8, 19) << 4 | opcode.ExtractBits(0, 3);

            return $"HVC{cond} #0x{imm32:X4}";
        }

        internal static string MRS(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var readSPSR = opcode.IsBitSet(22);
            var psr = readSPSR ? "SPSR" : "CPSR";

            return $"MRS{cond} {rd}, {psr}";
        }

        internal static string MRSBanked(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var readSPSR = opcode.IsBitSet(22);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var modeBits = opcode.ExtractBit(8) << 4 | opcode.ExtractBits(16, 19);
            var bankedRegister = DisassemblerUtils.DecodeBankedRegister(modeBits, readSPSR);

            return $"MRS{cond} {rd}, {bankedRegister}";
        }

        internal static string MSRImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var imm12 = DisassemblerUtils.ExpandARMImmediate(opcode.ExtractBits(0, 11));
            var mask = DisassemblerUtils.DecodeMSRMask(opcode);

            return $"MSR{cond} {mask}, #0x{imm12:X}";
        }

        internal static string MSRReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var psr = DisassemblerUtils.DecodeMSRMask(opcode);

            return $"MSR{cond} {psr}, {rn}";
        }

        internal static string MSRBanked(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var readSPSR = opcode.IsBitSet(22);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var modeBits = opcode.ExtractBit(8) << 4 | opcode.ExtractBits(16, 19);
            var bankedRegister = DisassemblerUtils.DecodeBankedRegister(modeBits, readSPSR);

            return $"MSR{cond} {bankedRegister}, {rn}";
        }

        internal static string QADD(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));

            return $"QADD{cond} {rd}, {rm}, {rn}";
        }

        internal static string QDADD(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));

            return $"QDADD{cond} {rd}, {rm}, {rn}";
        }

        internal static string QSUB(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));

            return $"QSUB{cond} {rd}, {rm}, {rn}";
        }

        internal static string QDSUB(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));

            return $"QDSUB{cond} {rd}, {rm}, {rn}";
        }

        internal static string RFE(uint opcode)
        {
            var writeback = opcode.IsBitSet(21) ? "!" : "";
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var accessMode = DisassemblerUtils.DecodeRFSOrSRSAccessMode(opcode);

            return $"RFE{accessMode} {rn}{writeback}";
        }

        internal static string SETEND(uint opcode)
        {
            var endianness = opcode.IsBitSet(9) ? "BE" : "LE";

            return $"SETEND {endianness}";
        }

        internal static string SMC(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var imm4 = opcode.ExtractBits(0, 3);

            return $"SMC{cond} #0x{imm4:X}";
        }

        internal static string SRS(uint opcode)
        {
            var writeback = opcode.IsBitSet(21) ? "1" : "";
            var accessMode = DisassemblerUtils.DecodeRFSOrSRSAccessMode(opcode);
            var processorMode = opcode.ExtractBits(0, 3);

            return $"SRS{accessMode}, sp{writeback}, #{processorMode}";
        }

        internal static string SVC(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var imm = opcode.ExtractBits(0, 23);

            return $"SVC{cond} #0x{imm:X}";
        }

        internal static string UDF(uint opcode)
        {
            var imm16 = opcode.ExtractBits(8, 19) << 4 | opcode.ExtractBits(0, 3);

            return $"UDF #{imm16}";
        }
    }
}
