namespace Humerus
{
    public static partial class Disassembler
    {
        // Disassembles arithmetic immediate ARM data processing instructions.
        private static string DataProcessingArithmeticImmediate(string name, uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var imm = DisassemblerUtils.ExpandARMImmediate(opcode.ExtractBits(0, 11));
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var update = opcode.IsBitSet(20) ? "S" : "";

            return $"{name}{update}{cond} {rd}, {rn}, #{imm}";
        }

        // Disassembles immediate comparison ARM data processing instructions.
        private static string DataProcessingCompareImmediate(string name, uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var imm = DisassemblerUtils.ExpandARMImmediate(opcode.ExtractBits(0, 11));

            return $"{name}{cond} {rn}, #{imm}";
        }

        // Disassembles wide move (MOVT and MOVW) ARM instructions.
        private static string DataProcessingWideMove(string name, uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var imm = opcode.ExtractBits(16, 19) << 12 | opcode.ExtractBits(0, 11);

            return $"{name}{cond} {rd}, #{imm}";
        }

        // Disassembles arithmetic register data processing instructions.
        private static string DataProcessingArithmeticRegister(string name, uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var update = opcode.IsBitSet(20) ? "S" : "";
            var shiftImm = opcode.ExtractBits(7, 11);
            var shiftType = opcode.ExtractBits(5, 6);
            var shift = DisassemblerUtils.DecodeImmediateShift(shiftType, shiftImm);

            return $"{name}{update}{cond} {rd}, {rn}, {rm}{shift}";
        }

        // Disassembles comparison register data processing instructions.
        private static string DataProcessingCompareRegister(string name, uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var shiftImm = opcode.ExtractBits(7, 11);
            var shiftType = opcode.ExtractBits(5, 6);
            var shift = DisassemblerUtils.DecodeImmediateShift(shiftType, shiftImm);

            return $"{name}{cond} {rn}, {rm}{shift}";
        }

        // Disassembles arithmetic register-shifted register data processing instructions.
        private static string DataProcessingArithmeticRegisterShifted(string name, uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var update = opcode.IsBitSet(20) ? "S" : "";

            var shiftReg = opcode.ExtractBits(8, 11);
            var shiftType = opcode.ExtractBits(5, 6);
            var shift = DisassemblerUtils.DecodeRegisterShift(shiftType, shiftReg);

            return $"{name}{update}{cond} {rd}, {rn}, {rm}{shift}";
        }

        // Disassembles comparison register-shifted register data processing instructions.
        private static string DataProcessingCompareRegisterShifted(string name, uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));

            var shiftReg = opcode.ExtractBits(8, 11);
            var shiftType = opcode.ExtractBits(5, 6);
            var shift = DisassemblerUtils.DecodeRegisterShift(shiftType, shiftReg);

            return $"{name}{cond} {rn}, {rm}{shift}";
        }

        // Disassembles immediate shift instructions (excluding RRX).
        private static string DataProcessingImmediateShift(string name, uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var type = opcode.ExtractBits(5, 6);
            var amount = opcode.ExtractBits(7, 11);
            var update = opcode.IsBitSet(20) ? "S" : "";

            // ASR encodes shifts by 32 as zero.
            if (type == 2 && amount == 0)
                amount = 32;

            return $"{name}{update}{cond} {rd}, {rm}, #{amount}";
        }

        // Disassembles a register shift
        private static string DataProcessingRegisterShift(string name, uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(8, 11));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var update = opcode.IsBitSet(20) ? "S" : "";

            return $"{name}{update}{cond} {rd}, {rn}, {rm}";
        }

        internal static string ADCImm(uint opcode)
        {
            return DataProcessingArithmeticImmediate("ADC", opcode);
        }

        internal static string ADCReg(uint opcode)
        {
            return DataProcessingArithmeticRegister("ADC", opcode);
        }

        internal static string ADCRsr(uint opcode)
        {
            return DataProcessingArithmeticRegisterShifted("ADC", opcode);
        }

        internal static string ADDImm(uint opcode)
        {
            return DataProcessingArithmeticImmediate("ADD", opcode);
        }

        internal static string ADDReg(uint opcode)
        {
            return DataProcessingArithmeticRegister("ADD", opcode);
        }

        internal static string ADDRsr(uint opcode)
        {
            return DataProcessingArithmeticRegisterShifted("ADD", opcode);
        }

        internal static string ANDImm(uint opcode)
        {
            return DataProcessingArithmeticImmediate("AND", opcode);
        }

        internal static string ANDReg(uint opcode)
        {
            return DataProcessingArithmeticRegister("AND", opcode);
        }

        internal static string ANDRsr(uint opcode)
        {
            return DataProcessingArithmeticRegisterShifted("AND", opcode);
        }

        internal static string ASRImm(uint opcode)
        {
            return DataProcessingImmediateShift("ASR", opcode);
        }

        internal static string ASRReg(uint opcode)
        {
            return DataProcessingRegisterShift("ASR", opcode);
        }

        internal static string BICImm(uint opcode)
        {
            return DataProcessingArithmeticImmediate("BIC", opcode);
        }

        internal static string BICReg(uint opcode)
        {
            return DataProcessingArithmeticRegister("BIC", opcode);
        }

        internal static string BICRsr(uint opcode)
        {
            return DataProcessingArithmeticRegisterShifted("BIC", opcode);
        }

        internal static string CMNImm(uint opcode)
        {
            return DataProcessingCompareImmediate("CMN", opcode);
        }

        internal static string CMNReg(uint opcode)
        {
            return DataProcessingCompareRegister("CMN", opcode);
        }

        internal static string CMNRsr(uint opcode)
        {
            return DataProcessingCompareRegisterShifted("CMN", opcode);
        }

        internal static string CMPImm(uint opcode)
        {
            return DataProcessingCompareImmediate("CMP", opcode);
        }

        internal static string CMPReg(uint opcode)
        {
            return DataProcessingCompareRegister("CMP", opcode);
        }

        internal static string CMPRsr(uint opcode)
        {
            return DataProcessingCompareRegisterShifted("CMP", opcode);
        }

        internal static string EORImm(uint opcode)
        {
            return DataProcessingArithmeticImmediate("EOR", opcode);
        }

        internal static string EORReg(uint opcode)
        {
            return DataProcessingArithmeticRegister("EOR", opcode);
        }

        internal static string EORRsr(uint opcode)
        {
            return DataProcessingArithmeticRegisterShifted("EOR", opcode);
        }

        internal static string LSLImm(uint opcode)
        {
            return DataProcessingImmediateShift("LSL", opcode);
        }

        internal static string LSLReg(uint opcode)
        {
            return DataProcessingRegisterShift("LSL", opcode);
        }

        internal static string LSRImm(uint opcode)
        {
            return DataProcessingImmediateShift("LSR", opcode);
        }

        internal static string LSRReg(uint opcode)
        {
            return DataProcessingRegisterShift("LSR", opcode);
        }

        internal static string MOVImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var update = opcode.IsBitSet(20) ? "S" : "";
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var imm = DisassemblerUtils.ExpandARMImmediate(opcode.ExtractBits(0, 11));

            return $"MOV{update}{cond} {rd}, #{imm}";
        }

        internal static string MOVReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var update = opcode.IsBitSet(20) ? "S" : "";

            return $"MOV{update}{cond} {rd}, {rm}";
        }

        internal static string MOVT(uint opcode)
        {
            return DataProcessingWideMove("MOVT", opcode);
        }

        internal static string MOVW(uint opcode)
        {
            return DataProcessingWideMove("MOVW", opcode);
        }

        internal static string MVNImm(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var imm = DisassemblerUtils.ExpandARMImmediate(opcode.ExtractBits(0, 11));
            var update = opcode.IsBitSet(20) ? "S" : "";

            return $"MVN{update}{cond} {rd}, #{imm}";
        }

        internal static string MVNReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var shiftImm = opcode.ExtractBits(7, 11);
            var shiftType = opcode.ExtractBits(5, 6);
            var shift = DisassemblerUtils.DecodeImmediateShift(shiftType, shiftImm);
            var update = opcode.IsBitSet(20) ? "S" : "";

            return $"MVN{update}{cond} {rd}, {rm}{shift}";
        }

        internal static string MVNRsr(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var update = opcode.IsBitSet(20) ? "S" : "";

            var shiftReg = opcode.ExtractBits(8, 11);
            var shiftType = opcode.ExtractBits(5, 6);
            var shift = DisassemblerUtils.DecodeRegisterShift(shiftType, shiftReg);

            return $"MVN{update}{cond} {rd}, {rm}{shift}";
        }

        internal static string ORRImm(uint opcode)
        {
            return DataProcessingArithmeticImmediate("ORR", opcode);
        }

        internal static string ORRReg(uint opcode)
        {
            return DataProcessingArithmeticRegister("ORR", opcode);
        }

        internal static string ORRRsr(uint opcode)
        {
            return DataProcessingArithmeticRegisterShifted("ORR", opcode);
        }

        internal static string RORImm(uint opcode)
        {
            return DataProcessingImmediateShift("ROR", opcode);
        }

        internal static string RORReg(uint opcode)
        {
            return DataProcessingRegisterShift("ROR", opcode);
        }

        internal static string RRX(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var update = opcode.IsBitSet(20) ? "S" : "";

            return $"RRX{update}{cond} {rd}, {rm}";
        }

        internal static string RSBImm(uint opcode)
        {
            return DataProcessingArithmeticImmediate("RSB", opcode);
        }

        internal static string RSBReg(uint opcode)
        {
            return DataProcessingArithmeticRegister("RSB", opcode);
        }

        internal static string RSBRsr(uint opcode)
        {
            return DataProcessingArithmeticRegisterShifted("RSB", opcode);
        }

        internal static string RSCImm(uint opcode)
        {
            return DataProcessingArithmeticImmediate("RSC", opcode);
        }

        internal static string RSCReg(uint opcode)
        {
            return DataProcessingArithmeticRegister("RSC", opcode);
        }

        internal static string RSCRsr(uint opcode)
        {
            return DataProcessingArithmeticRegisterShifted("RSC", opcode);
        }

        internal static string SBCImm(uint opcode)
        {
            return DataProcessingArithmeticImmediate("SBC", opcode);
        }

        internal static string SBCReg(uint opcode)
        {
            return DataProcessingArithmeticRegister("SBC", opcode);
        }

        internal static string SBCRsr(uint opcode)
        {
            return DataProcessingArithmeticRegisterShifted("SBC", opcode);
        }

        internal static string SUBImm(uint opcode)
        {
            return DataProcessingArithmeticImmediate("SUB", opcode);
        }

        internal static string SUBReg(uint opcode)
        {
            return DataProcessingArithmeticRegister("SUB", opcode);
        }

        internal static string SUBRsr(uint opcode)
        {
            return DataProcessingArithmeticRegisterShifted("SUB", opcode);
        }

        internal static string TEQImm(uint opcode)
        {
            return DataProcessingCompareImmediate("TEQ", opcode);
        }

        internal static string TEQReg(uint opcode)
        {
            return DataProcessingCompareRegister("TEQ", opcode);
        }

        internal static string TEQRsr(uint opcode)
        {
            return DataProcessingCompareRegisterShifted("TEQ", opcode);
        }

        internal static string TSTImm(uint opcode)
        {
            return DataProcessingCompareImmediate("TST", opcode);
        }

        internal static string TSTReg(uint opcode)
        {
            return DataProcessingCompareRegister("TST", opcode);
        }

        internal static string TSTRsr(uint opcode)
        {
            return DataProcessingCompareRegisterShifted("TST", opcode);
        }
    }
}
