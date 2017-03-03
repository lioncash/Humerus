using System;
using System.Collections.Generic;
using System.Globalization;

namespace Demo
{
    /// <summary>
    /// Utility class for holding methods relating to parsing
    /// textual opcode input.
    /// </summary>
    internal static class TextParsing
    {
        /// <summary>
        /// <para>
        /// Parses opcodes from user-supplied text.
        /// </para>
        /// <para>
        /// Inputs are assumed to be valid hex strings separated by spaces.
        /// </para>
        /// </summary>
        /// <example>
        /// Demo.exe -t EA000001 EB000001 should produce:
        ///
        /// B +#0x0000000C
        /// BL +#0x0000000C
        /// </example>
        /// <returns>A read-only collection of parsed opcodes.</returns>
        /// <param name="opcodeStrings">Array consisting of individual opcodes.</param>
        internal static IReadOnlyCollection<uint> ParseOpcodes(string[] opcodeStrings)
        {
            var opcodes = new List<uint>(opcodeStrings.Length);

            foreach (var opcodeString in opcodeStrings)
            {
                uint value;
                if (!uint.TryParse(opcodeString, NumberStyles.HexNumber,
                                   CultureInfo.InvariantCulture, out value))
                {
                    Console.WriteLine($"{opcodeString} is not a valid decimal or hex string.\n" +
                                  "Note that hex values must not have 0x " +
                                  "prefixes on them.");
                    Environment.Exit(-1);
                }

                opcodes.Add(value);
            }

            return opcodes.AsReadOnly();
        }
    }
}
