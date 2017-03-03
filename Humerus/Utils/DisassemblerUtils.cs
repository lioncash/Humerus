using System.Diagnostics;

namespace Humerus
{
    /// <summary>
    /// Disassembler utility functions.
    /// </summary>
    internal static class DisassemblerUtils
    {
        // Names of registers in the ARM 32-bit architecture.
        private static readonly string[] registerNames = {
            "r0", "r1",  "r2",  "r3",  "r4", "r5", "r6", "r7",
            "r8", "r9", "r10", "r11", "r12", "sp", "lr", "pc"
        };

        // Names of condition codes that may be predicated on
        // conditional instructions. The last two strings
        // represent 'Always' and 'Unconditional', which are
        // generally omitted, as writing an instruction without
        // a condition code is the same as predicating it with 'Always'.
        //
        // Names are:
        // EQ - Equals
        // NE - Not Equals
        // CS - Carry Set (unsigned higher or same)
        // CC - Carry Cleared (unsigned lower)
        // MI - Negative (Minus)
        // PL - Positive or zero (Plus)
        // VS - Signed overflow (V Set)
        // VC - No signed overflow (V Cleared)
        // HI - Unsigned higher
        // LS - Unsigned lower or same
        // GE - Signed greater than or equal.
        // LT - Signed less than
        // GT - Signed greater than
        // LE - Signed less than or equal
        // AL - Always (represented by an empty string)
        //
        private static readonly string[] conditionCodeNames = {
            "EQ", "NE", "CS", "CC", "MI", "PL", "VS", "VC",
            "HI", "LS", "GE", "LT", "GT", "LE", "", ""
        };

        // Banked register table used for disassembling banked variants of MRS and MSR.
        private static readonly string[,] bankedRegisterTable = {
            {"r8_usr"       , "r8_fiq"       , "lr_irq", "UNPREDICTABLE"},
            {"r9_usr"       , "r9_fiq"       , "sp_irq", "UNPREDICTABLE"},
            {"r10_usr"      , "r10_fiq"      , "lr_svc", "UNPREDICTABLE"},
            {"r11_usr"      , "r11_fiq"      , "sp_svc", "UNPREDICTABLE"},
            {"r12_usr"      , "r12_fiq"      , "lr_abt", "lr_mon"       },
            {"sp_usr"       , "sp_fiq"       , "sp_abt", "sp_mon"       },
            {"lr_usr"       , "lr_fiq"       , "lr_und", "ELR_hyp"      },
            {"UNPREDICTABLE", "UNPREDICTABLE", "sp_und", "sp_hyp"       }
        };

        // Same as above, but used in the case where the bit for reading the SPSR is set.
        private static readonly string[,] bankedRegisterTableWhenReadingSPSR = {
            {"UNPREDICTABLE", "UNPREDICTABLE", "SPSR_irq"     , "UNPREDICTABLE"},
            {"UNPREDICTABLE", "UNPREDICTABLE", "UNPREDICTABLE", "UNPREDICTABLE"},
            {"UNPREDICTABLE", "UNPREDICTABLE", "SPSR_svc"     , "UNPREDICTABLE"},
            {"UNPREDICTABLE", "UNPREDICTABLE", "UNPREDICTABLE", "UNPREDICTABLE"},
            {"UNPREDICTABLE", "UNPREDICTABLE", "SPSR_abt"     , "SPSR_mon"     },
            {"UNPREDICTABLE", "UNPREDICTABLE", "UNPREDICTABLE", "UNPREDICTABLE"},
            {"UNPREDICTABLE", "SPSR_fiq"     , "SPSR_und"     , "SPSR_hyp"     },
            {"UNPREDICTABLE", "UNPREDICTABLE", "UNPREDICTABLE", "UNPREDICTABLE"}
        };

        /// <summary>
        /// Retrieves the name of a register based off its numeric representation.
        /// </summary>
        /// <returns>The name of the register</returns>
        /// <param name="index">A value between 0 to 15 representing a register.</param>
        public static string RegisterName(uint index)
        {
            Debug.Assert(index < registerNames.Length);

            return registerNames[index];
        }

        /// <summary>
        /// Converts an ARM instruction's condition code into a string.
        /// </summary>
        /// <returns>String representation of the given opcode's condition code.</returns>
        /// <param name="opcode">The instruction.</param>
        public static string ARMConditionCode(uint opcode)
        {
            var cond = opcode.ExtractBits(28, 31);

            return conditionCodeNames[cond];
        }

        /// <summary>
        /// Decodes an immediate shift operation encoded in an ARM instruction.
        /// </summary>
        /// <returns>The string representation of the immediate shift.</returns>
        /// <param name="type">Bits that represent the type of shift.</param>
        /// <param name="immediate">Bits that represent the immediate.</param>
        public static string DecodeImmediateShift(uint type, uint immediate)
        {
            Debug.Assert(type <= 3);

            switch (type)
            {
            case 0:
                if (immediate == 0)
                    return "";
                return $", LSL #{immediate}";
            case 1:
                if (immediate == 0)
                    immediate = 32;
                return $", LSR #{immediate}";
            case 2:
                if (immediate == 0)
                    immediate = 32;
                return $", ASR #{immediate}";
            case 3:
                if (immediate == 0)
                    return ", RRX";
                return $", ROR #{immediate}";
            }

            return "Invalid immediate shift";
        }

        /// <summary>
        /// Decodes a register-based shift.
        /// </summary>
        /// <returns>The register shift as a readable string.</returns>
        /// <param name="type">The type of shift.</param>
        /// <param name="register">The register used as a shift operand.</param>
        public static string DecodeRegisterShift(uint type, uint register)
        {
            Debug.Assert(type <= 3);

            var regString = RegisterName(register);

            switch (type)
            {
            case 0:
                return $", LSL {regString}";
            case 1:
                return $", LSR {regString}";
            case 2:
                return $", ASR {regString}";
            case 3:
                return $", ROR {regString}";
            }

            return "Invalid register shift.";
        }

        /// <summary>
        /// Expands an ARM immediate.
        ///
        /// Data processing instructions in ARM use a particular
        /// way of encoding their immediates in instructions
        /// in order to allow a larger range of constants to be used
        /// than would otherwise be possible if values were just
        /// encoded directly onto the instruction.
        ///
        /// The first 8 bits of the instruction store the encoded value,
        /// while the 4 bits following the encoded value indicate a rotation value.
        /// So the representation is like so:
        ///
        /// <code>
        /// <![CDATA[
        /// |         ...         | 11 10 9 8 | 7 6 5 4 3 2 1 |
        /// | rest of instruction | rotation  | value         |
        /// ]]>
        /// </code>
        ///
        /// This encoded value is rotated right by the indicated
        /// rotation value multiplied by two.
        /// </summary>
        /// <returns>The expanded ARM immediate.</returns>
        /// <param name="input">An encoded ARM immediate.</param>
        public static uint ExpandARMImmediate(uint input)
        {
            var value = input.ExtractBits(0, 7);
            var rotation = input.ExtractBits(8, 11);

            return value.RotateRight((int)(2 * rotation));
        }

        /// <summary>
        /// Decodes the mask from an MSR instruction.
        ///
        /// MSR has two notations that are used for representing flags
        /// in disassembly.
        ///
        /// If a write is done to any of the lower 16 bits of the PSR the 'fsxc'
        /// notation is used. This is a slightly older notation, where the
        /// respective entries are defined as follows:
        ///
        /// <list type="table">
        ///   <listheader>
        ///     <term>Flag</term>
        ///     <term>Description</term>
        ///   </listheader>
        ///   <item>
        ///     <term>f</term>
        ///     <term>Flags — Represents bits 24–31 in the PSR.</term>
        ///   </item>
        ///   <item>
        ///     <term>s</term>
        ///     <term>Status — Represents bits 16–23 in the PSR.</term>
        ///   </item>
        ///   <item>
        ///     <term>x</term>
        ///     <term>Extension — Represents bits 8–15 in the PSR.</term>
        ///   </item>
        ///   <item>
        ///     <term>c</term>
        ///     <term>Condition — Represents bits 0–7 in the PSR.</term>
        ///   </item>
        /// </list>
        ///
        /// If only writes to the top 16 bits are performed, then the slightly
        /// newer notation will be used, as it modifies bits that are visible
        /// through the APSR view of the PSR. The notation is listed as follows:
        ///
        /// <list type="table">
        ///   <listheader>
        ///     <term>Flag</term>
        ///     <term>Description</term>
        ///   </listheader>
        ///   <item>
        ///     <term>nzcvq</term>
        ///     <term>
        ///       Indicates a write to bits 24–31 in the PSR—the location where the NZCVQ bits are.
        ///     </term>
        ///     <term>g</term>
        ///     <term>
        ///       Indicates a write to bits 24–31 in the PSR—the location where the GE bits are.
        ///     </term>
        ///   </item>
        /// </list>
        /// </summary>
        /// <returns>The MSR mask as a readable string.</returns>
        /// <param name="opcode">The opcode repesenting an MSR instruction.</param>
        public static string DecodeMSRMask(uint opcode)
        {
            var altersSPSR = opcode.IsBitSet(22);
            var writesFlags = opcode.IsBitSet(19);
            var writesStatus = opcode.IsBitSet(18);
            var writesExtension = opcode.IsBitSet(17);
            var writesControl = opcode.IsBitSet(16);

            if (altersSPSR || writesControl || writesExtension)
            {
                var f = writesFlags ? "f" : "";
                var s = writesStatus ? "s" : "";
                var x = writesExtension ? "x" : "";
                var c = writesControl ? "c" : "";
                var psr = altersSPSR ? "SPSR" : "CPSR";

                return $"{psr}_{f}{s}{x}{c}";
            }

            var nzcvq = writesFlags ? "nzcvq" : "";
            var g = writesStatus ? "g" : "";

            return $"APSR_{nzcvq}{g}";
        }

        /// <summary>
        /// Decodes a register list encoded on Load/Store Multiple instructions.
        ///
        /// Register lists are encoded as a series of bits, with the lowest
        /// bit corresponding to r0, and the highest bit corresponding to
        /// the program counter.
        /// </summary>
        /// <example>
        /// A typical load store instruction list encoding looks like the following:
        ///
        /// Bits:
        /// ... | 15 14 13 12 11 10  9  8  7  6  5  4  3  2  1  0 |
        /// -------------------------------------------------------
        /// ... |                  register list                  |
        ///
        /// If bit 0 and 1 are set, then it means r0 and r1 are used
        /// in the load or store operation. 
        /// </example>
        /// <returns>The decoded register list.</returns>
        /// <param name="input">The encoded register list.</param>
        public static string DecodeRegisterList(uint input)
        {
            var registers = "";

            for (int i = 0; i <= 15; i++)
            {
                if (input.IsBitSet(i))
                    registers += $"{RegisterName((uint)i)}, ";
            }

            registers = registers.TrimEnd(',', ' ' );

            return $"{{{registers}}}";
        }

        /// <summary>
        /// Decodes a banked register.
        /// </summary>
        /// <returns>The banked register as a readable string.</returns>
        /// <param name="sysm">System mode bits</param>
        /// <param name="readSPSR">Whether or not the SPSR is being read.</param>
        /// <remarks>Used for decoding banked MRS and MSR variants.</remarks>
        public static string DecodeBankedRegister(uint sysm, bool readSPSR)
        {
            var x = sysm.ExtractBits(0, 2);
            var y = sysm.ExtractBits(3, 4);

            if (readSPSR)
                return bankedRegisterTableWhenReadingSPSR[x, y];

            return bankedRegisterTable[x, y];
        }

        /// <summary>
        /// Decodes the access mode of an RFE or SRS instruction.
        /// </summary>
        /// <returns>The access mode as a readable string.</returns>
        /// <param name="opcode">A valid RFE or SRS opcode.</param>
        public static string DecodeRFSOrSRSAccessMode(uint opcode)
        {
            var indexed = opcode.IsBitSet(24);
            var add = opcode.IsBitSet(23);

            if (indexed)
            {
                if (add)
                    return "IB";

                return "DB";
            }

            // Would be "IA", but this is allowed to be omitted.
            if (add)
                return "";

            return "DA";
        }
    }
}
