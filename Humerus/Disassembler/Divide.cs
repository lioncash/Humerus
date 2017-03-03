// Implementation of divide instructions
namespace Humerus
{
    public static partial class Disassembler
    {
        internal static string Divide(uint opcode)
        {
            var name = opcode.IsBitSet(21) ? "UDIV" : "SDIV";
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rd = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var rm = DisassemblerUtils.RegisterName(opcode.ExtractBits(8, 11));
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));

            return $"{name}{cond} {rd}, {rn}, {rm}";
        }
    }
}
