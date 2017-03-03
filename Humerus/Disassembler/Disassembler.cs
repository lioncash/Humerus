namespace Humerus
{
    /// <summary>
    /// Main ARM disassembler. Responsible for implementing
    /// routines that provide text representation of instruction
    /// opcodes
    /// </summary>
    public static partial class Disassembler
    {
        /// <summary>
        /// Disassembles the specified opcode.
        /// </summary>
        /// <param name="opcode">ARM opcode</param>
        public static string Disassemble(uint opcode)
        {
            var result = InstructionDecoder.Decode(opcode);
            if (result.IsValid && result.DisassemblyFunction != null)
                return result.DisassemblyFunction(opcode);

            return $"Invalid opcode: 0x{opcode:X8}";
        }
    }
}
