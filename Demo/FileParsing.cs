using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Demo
{
    /// <summary>
    /// Utility class for file parsing handling.
    /// </summary>
    internal static class FileParsing
    {
        // ARM mode instructions are 32 bits wide (i.e. 4 bytes)
        // as this disassembler doesn't support Thumb, 2-byte
        // variants don't need to be handled.
        private const int ARMInstructionSizeInBytes = 4;

        /// <summary>
        /// Reads opcodes from a file.
        /// </summary>
        /// <returns>A read-only collection of opcodes.</returns>
        /// <param name="filePath">Path to the file to disassemble.</param>
        /// <param name="startOffset">Starting byte offset to begin disassembly at.</param>
        /// <param name="endOffset">Ending byte offset to finish disassembly at.</param>
        /// <remarks>
        /// If <paramref name="startOffset"/> is -1, disassembly will assume it should start
        /// from the first byte in the file.
        ///
        /// If <paramref name="endOffset"/> is -1, disassembly will assume it should continue
        /// until the end of the file is reached.
        /// </remarks>
        internal static IReadOnlyCollection<uint> ParseFile(string filePath, long startOffset, long endOffset)
        {
            // No work to do
            if (endOffset == 0)
                return new ReadOnlyCollection<uint>(new List<uint>());

            using (var reader = new BinaryReader(File.OpenRead(filePath)))
            {
                // User didn't specify an end, so read the whole file.
                if (endOffset <= -1)
                    endOffset = reader.BaseStream.Length;

                if (startOffset > 0)
                    reader.BaseStream.Seek(startOffset, SeekOrigin.Begin);

                // Approximate a rough amount of expected opcode entries
                var opcodes = new List<uint>((int)((endOffset - startOffset) / ARMInstructionSizeInBytes));

                while (CanReadAnotherOpcode(reader, endOffset))
                {
                    opcodes.Add(reader.ReadUInt32());
                } 

                return opcodes;
            }
        }

        // Ensure at minimum that 4 bytes can be read from the underlying stream.
        private static bool CanReadAnotherOpcode(BinaryReader reader, long endOffset)
        {
            var streamPosition = reader.BaseStream.Position;

            if (streamPosition >= endOffset)
                return false;

            var streamLength = reader.BaseStream.Length;
            return streamLength - streamPosition >= ARMInstructionSizeInBytes;
        }
    }
}
