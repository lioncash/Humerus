using System;
using System.Collections.Generic;
using System.IO;

namespace Demo
{
    /// <summary>
    /// Houses the main entry point.
    ///
    /// Provides a basic demo application that allows
    /// a user to enter instructions via console input
    /// and receive disassembled output.
    /// </summary>
    internal static class MainClass
    {
        private static readonly CommandLineOptions options = new CommandLineOptions();

        // Main entry point
        public static void Main(string[] args)
        {
            if (!CommandLine.Parser.Default.ParseArgumentsStrict(args, options))
                return;

            PerformDisassembly();
        }

        // Performs the actual disassembly routine,
        // handling any problems with integer formatting
        // or overflow.
        private static void PerformDisassembly()
        {
            if (options.IsFileParsingEnabled)
            {
                DisassembleFile();
            }
            else if (options.IsTextParsingEnabled)
            {
                DisassembleText();
            }
            else
            {
                Console.WriteLine("Unspecified disassembly mode. Ensure that -f or -t is specified.");
                Console.WriteLine("Exiting...");
            }
        }

        private static void DisassembleFile()
        {
            if (!File.Exists(options.FilePath))
            {
                Console.WriteLine($"File {options.FilePath} does not exist.");
                Console.WriteLine("Exiting...");
                return;
            }

            var info = new FileInfo(options.FilePath);
            if (options.FileStartOffset > info.Length)
            {
                Console.WriteLine("Cannot start at an offset greater than the given file's size.");
                Console.WriteLine($"Start offset: {options.FileStartOffset} | File size: {info.Length}");
                Console.WriteLine("Exiting...");
            }

            if (options.FileEndOffset > info.Length)
            {
                Console.WriteLine("Cannot end at an offset greater than the given file's size.");
                Console.WriteLine($"End offset: {options.FileEndOffset} | File size: {info.Length}");
                Console.WriteLine("Exiting...");
                return;
            }

            try
            {
                var opcodes = FileParsing.ParseFile(options.FilePath,
                                                    options.FileStartOffset,
                                                    options.FileEndOffset);
                DisassembleOpcodes(opcodes);
            }
            catch (IOException ioe)
            {
                Console.WriteLine($"IOException occurred: {ioe} | inner: {ioe.InnerException}");
                Console.WriteLine("Exiting...");
            }
        }

        private static void DisassembleText()
        {
            var opcodes = TextParsing.ParseOpcodes(options.OpcodeText);
            DisassembleOpcodes(opcodes);
        }

        private static void DisassembleOpcodes(IEnumerable<uint> opcodes)
        {
            foreach (var opcode in opcodes)
            {
                Console.WriteLine(Humerus.Disassembler.Disassemble(opcode));
            }
        }
    }
}