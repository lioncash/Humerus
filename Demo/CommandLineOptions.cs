using CommandLine;
using CommandLine.Text;

namespace Demo
{
    /// <summary>
    /// Describes all of the possible command-line switches
    /// that this demo program uses.
    /// </summary>
    public sealed class CommandLineOptions
    {
        /// <summary>
        /// Indicates the file used for file parsing.
        /// </summary>
        /// <remarks>
        /// This is null if file parsing is not enabled.
        /// </remarks>
        /// <value>The file path to use for file parsing.</value>
        [Option('f', "file",
                HelpText = "Parse a file.",
                MutuallyExclusiveSet = "file-parsing")]
        public string FilePath { get; set; }

        /// <summary>
        /// Indicates the offset in a file to begin disassembly at.
        /// </summary>
        /// <value>The starting offset for disassembly in bytes.</value>
        [Option('o', "start-offset",
                DefaultValue = -1,
                HelpText = "Indicates the byte position within the file to begin disassembly. " +
                           "This value must be a multiple of four " +
                           "Omitting this will cause disassembly to start from the first byte.",
                MutuallyExclusiveSet = "file-parsing")]
        public long FileStartOffset { get; set; }

        /// <summary>
        /// Indicates the ending offset for file disassembly.
        /// </summary>
        /// <value>The ending file offset in bytes.</value>
        [Option('e', "end-offset",
                DefaultValue = -1,
                HelpText = "Indicates the byte offset to finish disassembly at. " +
                           "This value must be a multiple of 4. " +
                           "Omitting this will cause disassembly to continue until the end of the file.",
                MutuallyExclusiveSet = "file-parsing")]
        public long FileEndOffset { get; set; }

        /// <summary>
        /// Contains all of the opcodes entered by a user
        /// when performing text disassembly.
        /// </summary>
        /// <value>All user-entered opcodes as individual strings.</value>
        [OptionArray('t', "text",
                     HelpText = "Parse given text as an opcode stream (each opcode must be separated by a space)",
                     MutuallyExclusiveSet = "text-parsing")]
        public string[] OpcodeText { get; set; }

        /// <summary>
        /// Retrieves the usage help text.
        /// </summary>
        /// <returns>The usage help text.</returns>
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        /// <summary>
        /// Indicating whether or not file parsing is enabled.
        /// </summary>
        /// <value><c>true</c> if file parsing is enabled; otherwise, <c>false</c>.</value>
        public bool IsFileParsingEnabled => FilePath != null;

        /// <summary>
        /// Indicates whether or not text parsing enabled.
        /// </summary>
        /// <value><c>true</c> if text parsing is enabled; otherwise, <c>false</c>.</value>
        public bool IsTextParsingEnabled => OpcodeText != null;
    }
}
