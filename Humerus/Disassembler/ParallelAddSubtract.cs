// Implementation of parallel addition and subtraction instructions

namespace Humerus
{
    public static partial class Disassembler
    {
        private static string GetParallelOpcodeNameSuffix(uint opcode)
        {
            switch (opcode.ExtractBits(5, 7))
            {
            case 0:
                return "ADD16";
            case 1:
                return "ASX";
            case 2:
                return "SAX";
            case 3:
                return "SUB16";
            case 4:
                return "ADD8";
            case 7:
                return "SUB8";
            }

            return "Invalid";
        }

        private static string ComposeParallelInstructionString(uint opcode, string prefix)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(12, 15));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var suffix = GetParallelOpcodeNameSuffix(opcode);

            return $"{prefix}{suffix}{cond} {rd}, {rn}, {rm}";
        }

        internal static string ParallelAddSubModuloSigned(uint opcode)
        {
            return ComposeParallelInstructionString(opcode, "S");
        }

        internal static string ParallelAddSubModuloUnsigned(uint opcode)
        {
            return ComposeParallelInstructionString(opcode, "U");
        }

        internal static string ParallelAddSubSaturateSigned(uint opcode)
        {
            return ComposeParallelInstructionString(opcode, "Q");
        }

        internal static string ParallelAddSubSaturateUnsigned(uint opcode)
        {
            return ComposeParallelInstructionString(opcode, "UQ");
        }

        internal static string ParallelAddSubHalvingSigned(uint opcode)
        {
            return ComposeParallelInstructionString(opcode, "SH");
        }

        internal static string ParallelAddSubHalvingUnsigned(uint opcode)
        {
            return ComposeParallelInstructionString(opcode, "UH");
        }
    }
}
