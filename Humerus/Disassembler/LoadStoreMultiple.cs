// Load/Store multiple instruction disassembly.
namespace Humerus
{
    public static partial class Disassembler
    {
        // Disassembles basic load/store multiple instructions.
        private static string LoadStoreMultiple(string name, uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var rn = DisassemblerUtils.RegisterName(opcode.ExtractBits(16, 19));
            var reglist = DisassemblerUtils.DecodeRegisterList(opcode);
            var writeback = opcode.IsBitSet(21) ? "!" : "";

            return $"{name}{cond} {rn}{writeback}, {reglist}";
        }

        internal static string LDMDA(uint opcode)
        {
            return LoadStoreMultiple("LDMDA", opcode);
        }

        internal static string LDMDB(uint opcode)
        {
            return LoadStoreMultiple("LDMDB", opcode);
        }

        internal static string LDMIA(uint opcode)
        {
            return LoadStoreMultiple("LDMIA", opcode);
        }

        internal static string LDMIB(uint opcode)
        {
            return LoadStoreMultiple("LDMIB", opcode);
        }

        internal static string STMDA(uint opcode)
        {
            return LoadStoreMultiple("STMDA", opcode);
        }

        internal static string STMDB(uint opcode)
        {
            return LoadStoreMultiple("STMDB", opcode);
        }

        internal static string STMIA(uint opcode)
        {
            return LoadStoreMultiple("STMIA", opcode);
        }

        internal static string STMIB(uint opcode)
        {
            return LoadStoreMultiple("STMIB", opcode);
        }
    }
}
