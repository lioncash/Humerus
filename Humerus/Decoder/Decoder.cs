using System;
using System.Linq;

namespace Humerus
{
    /// <summary>
    /// Instruction decoder for ARM mode instructions.
    /// </summary>
    internal sealed class InstructionDecoder
    {
        #region Table entry composition

        /// <summary>
        /// Describes an entry in the decoding table.
        ///
        /// Each entry consists of a mask and an expected value as a
        /// result from the mask, and a function to execute.
        ///
        /// This will be used to make a simple decoder that operates according
        /// to the following pseudocode.:
        ///
        /// <code>
        /// <![CDATA[
        /// DecodedResult Decode(uint opcode)
        /// {
        ///     foreach (entry in table)
        ///     {
        ///         if ((opcode & entry.mask) == entry.expected)
        ///         {
        ///             return new DecodedResult(true, entry.disassembly_function);
        ///         }
        ///     }
        ///
        ///    return new DecodedResult(false, null);
        /// }
        /// ]]>
        /// </code>
        ///
        /// If the end of the table is hit with no matches, then it can be assumed
        /// that the supplied opcode is invalid and handled accordingly.
        /// </summary>
        private sealed class TableEntry
        {
            private readonly uint mask;
            private readonly uint expected;
            public Func<uint, string> DisassemblyFunction { get; }

            public TableEntry(uint mask, uint expected, Func<uint, string> disassemblyFunction)
            {
                this.mask = mask;
                this.expected = expected;
                DisassemblyFunction = disassemblyFunction;
            }

            /// <summary>
            /// Whether or not the given opcode matches the one
            /// represented by this table entry.
            /// </summary>
            /// <param name="opcode">An arbitrary opcode.</param>
            /// <returns>True if there's a match; false otherwise.</returns>
            public bool Matches(uint opcode)
            {
                return (opcode & mask) == expected;
            }

            public override string ToString()
            {
                return $"[TableEntry: Mask={mask:X8}, Expected={expected:X8}, DisassemblyFunction={DisassemblyFunction}]";
            }
        }

        #endregion

        #region ARM instruction table

        // Instruction table for ARM mode instructions only. This
        // does not include NEON or VFP ARM mode instructions.
        private static readonly TableEntry[] armInstructionTable = {
            // Unconditional instructions
            new TableEntry(0xFFF1FE20, 0xF1000000, Disassembler.CPS),    // CPS
            new TableEntry(0xFFFFFDFF, 0xF1010000, Disassembler.SETEND), // SETEND
            new TableEntry(0xFFFFFFFF, 0xF57FF01F, Disassembler.CLREX),  // CLREX
            new TableEntry(0xFE5FFFF0, 0xF6A00010, Disassembler.SRS),    // SRS
            new TableEntry(0xFE500000, 0xF8100000, Disassembler.RFE),    // RFE

            // Data processing instructions (register)
            new TableEntry(0x0FE00010, 0x00000000, Disassembler.ANDReg), // AND (reg)
            new TableEntry(0x0FE00010, 0x00200000, Disassembler.EORReg), // EOR (reg)
            new TableEntry(0x0FE00010, 0x00400000, Disassembler.SUBReg), // SUB (reg)
            new TableEntry(0x0FE00010, 0x00600000, Disassembler.RSBReg), // RSB (reg)
            new TableEntry(0x0FE00010, 0x00800000, Disassembler.ADDReg), // ADD (reg)
            new TableEntry(0x0FE00010, 0x00A00000, Disassembler.ADCReg), // ADC (reg)
            new TableEntry(0x0FE00010, 0x00C00000, Disassembler.SBCReg), // SBC (reg)
            new TableEntry(0x0FE00010, 0x00E00000, Disassembler.RSCReg), // RSC (reg)
            new TableEntry(0x0FF0F010, 0x01100000, Disassembler.TSTReg), // TST (reg)
            new TableEntry(0x0FF0F010, 0x01300000, Disassembler.TEQReg), // TEQ (reg)
            new TableEntry(0x0FF0F010, 0x01500000, Disassembler.CMPReg), // CMP (reg)
            new TableEntry(0x0FF0F010, 0x01700000, Disassembler.CMNReg), // CMN (reg)
            new TableEntry(0x0FE00010, 0x01800000, Disassembler.ORRReg), // ORR (reg)
            new TableEntry(0x0FEF0FF0, 0x01A00000, Disassembler.MOVReg), // MOV (reg)
            new TableEntry(0x0FEF0070, 0x01A00000, Disassembler.LSLImm), // LSL (imm)
            new TableEntry(0x0FEF0070, 0x01A00020, Disassembler.LSRImm), // LSR (imm)
            new TableEntry(0x0FEF0070, 0x01A00040, Disassembler.ASRImm), // ASR (imm)
            new TableEntry(0x0FEF0FF0, 0x01A00060, Disassembler.RRX),    // RRX
            new TableEntry(0x0FEF0070, 0x01A00060, Disassembler.RORImm), // ROR (imm)
            new TableEntry(0x0FE00010, 0x01C00000, Disassembler.BICReg), // BIC (reg)
            new TableEntry(0x0FEF0010, 0x01E00000, Disassembler.MVNReg), // MVN (reg)

            // Data processing instructions (register-shifted register)
            new TableEntry(0x0FE00090, 0x00000010, Disassembler.ANDRsr), // AND (rsr)
            new TableEntry(0x0FE00090, 0x00200010, Disassembler.EORRsr), // EOR (rsr)
            new TableEntry(0x0FE00090, 0x00400010, Disassembler.SUBRsr), // SUB (rsr)
            new TableEntry(0x0FE00090, 0x00600010, Disassembler.RSBRsr), // RSB (rsr)
            new TableEntry(0x0FE00090, 0x00800010, Disassembler.ADDRsr), // ADD (rsr)
            new TableEntry(0x0FE00090, 0x00A00010, Disassembler.ADCRsr), // ADC (rsr)
            new TableEntry(0x0FE00090, 0x00C00010, Disassembler.SBCRsr), // SBC (rsr)
            new TableEntry(0x0FE00090, 0x00E00010, Disassembler.RSCRsr), // RSC (rsr)
            new TableEntry(0x0FF0F090, 0x01100010, Disassembler.TSTRsr), // TST (rsr)
            new TableEntry(0x0FF0F090, 0x01300010, Disassembler.TEQRsr), // TEQ (rsr)
            new TableEntry(0x0FF0F090, 0x01500010, Disassembler.CMPRsr), // CMP (rsr)
            new TableEntry(0x0FF0F090, 0x01700010, Disassembler.CMNRsr), // CMN (rsr)
            new TableEntry(0x0FE00090, 0x01800010, Disassembler.ORRRsr), // ORR (rsr)
            new TableEntry(0x0FEF00F0, 0x01A00010, Disassembler.LSLReg), // LSL (reg)
            new TableEntry(0x0FEF00F0, 0x01A00030, Disassembler.LSRReg), // LSR (reg)
            new TableEntry(0x0FEF00F0, 0x01A00050, Disassembler.ASRReg), // ASR (reg)
            new TableEntry(0x0FEF00F0, 0x01A00070, Disassembler.RORReg), // ROR (reg)
            new TableEntry(0x0FE00090, 0x01C00010, Disassembler.BICRsr), // BIC (rsr)
            new TableEntry(0x0FEF0090, 0x01E00010, Disassembler.MVNRsr), // MVN (rsr)

            // Miscellaneous instructions (basically ARM being lazy with labeling)
            new TableEntry(0x0FB00EFF, 0x01000200, Disassembler.MRSBanked), // MRS (banked)
            new TableEntry(0x0FB0FEF0, 0x0120F200, Disassembler.MSRBanked), // MSR (banked)
            new TableEntry(0x0FBF0FFF, 0x010F0000, Disassembler.MRS),       // MRS
            new TableEntry(0x0FF3FFF0, 0x0120F000, Disassembler.MSRReg),    // MSR (reg)
            new TableEntry(0x0FF3FFF0, 0x0121F000, Disassembler.MSRReg),    // MSR (reg)
            new TableEntry(0x0FF2FFF0, 0x0122F000, Disassembler.MSRReg),    // MSR (reg)
            new TableEntry(0x0FF0FFF0, 0x0160F000, Disassembler.MSRReg),    // MSR (reg)
            new TableEntry(0x0FFFFFF0, 0x012FFF10, Disassembler.BX),        // BX (reg)
            new TableEntry(0x0FFF0FF0, 0x016F0F10, Disassembler.CLZ),       // CLZ
            new TableEntry(0x0FFFFFF0, 0x012FFF20, Disassembler.BXJ),       // BXJ
            new TableEntry(0x0FFFFFF0, 0x012FFF30, Disassembler.BLXReg),    // BLX (reg)
            new TableEntry(0x0FF00FF0, 0x01000050, Disassembler.QADD),      // QADD
            new TableEntry(0x0FF00FF0, 0x01200050, Disassembler.QSUB),      // QSUB
            new TableEntry(0x0FF00FF0, 0x01400050, Disassembler.QDADD),     // QDADD
            new TableEntry(0x0FF00FF0, 0x01600050, Disassembler.QDSUB),     // QDSUB
            new TableEntry(0x0FFFFFFF, 0x0160006E, Disassembler.ERET),      // ERET
            new TableEntry(0x0FF000F0, 0x01200070, Disassembler.BKPT),      // BKPT
            new TableEntry(0x0FF000F0, 0x01400070, Disassembler.HVC),       // HVC
            new TableEntry(0x0FFFFFF0, 0x01600070, Disassembler.SMC),       // SMC

            // Halfword multiply and multiply accumulate instructions.
            new TableEntry(0x0FF00090, 0x01000080, Disassembler.HalfwordMultiply), // SMLAXY
            new TableEntry(0x0FF000B0, 0x01200080, Disassembler.HalfwordMultiply), // SMLAWY
            new TableEntry(0x0FF0F0B0, 0x012000A0, Disassembler.HalfwordMultiply), // SMULWY
            new TableEntry(0x0FF00090, 0x01400080, Disassembler.HalfwordMultiply), // SMLALXY
            new TableEntry(0x0FF0F090, 0x01600080, Disassembler.HalfwordMultiply), // SMULXY

            // Multiply and multiply accumulate instructions
            new TableEntry(0x0FE0F0F0, 0x00000090, Disassembler.Multiply),     // MUL
            new TableEntry(0x0FE000F0, 0x00200090, Disassembler.Multiply),     // MLA
            new TableEntry(0x0FF000F0, 0x00400090, Disassembler.UMAAL),        // UMAAL
            new TableEntry(0x0FF000F0, 0x00500090, Disassembler.UDF),          // UDF
            new TableEntry(0x0FF000F0, 0x00600090, Disassembler.Multiply),     // MLS
            new TableEntry(0x0FF000F0, 0x00700090, Disassembler.UDF),          // UDF
            new TableEntry(0x0FE000F0, 0x00800090, Disassembler.MultiplyLong), // UMULL
            new TableEntry(0x0FE000F0, 0x00A00090, Disassembler.MultiplyLong), // UMLAL
            new TableEntry(0x0FE000F0, 0x00C00090, Disassembler.MultiplyLong), // SMULL
            new TableEntry(0x0FE000F0, 0x00E00090, Disassembler.MultiplyLong), // SMLAL

            // Synchronization primitives
            new TableEntry(0x0FB00FF0, 0x01000090, Disassembler.SWP),            // SWP/SWPB
            new TableEntry(0x0FF00FF0, 0x01800F90, Disassembler.StoreExclusive), // STREX
            new TableEntry(0x0FF00FFF, 0x01900F9F, Disassembler.LoadExclusive),  // LDREX
            new TableEntry(0x0FF00FF0, 0x01A00F90, Disassembler.StoreExclusive), // STREXD
            new TableEntry(0x0FF00FFF, 0x01B00F9F, Disassembler.LoadExclusive),  // LDREXD
            new TableEntry(0x0FF00FF0, 0x01C00F90, Disassembler.StoreExclusive), // STREXB
            new TableEntry(0x0FF00FFF, 0x01D00F9F, Disassembler.LoadExclusive),  // LDREXB
            new TableEntry(0x0FF00FF0, 0x01E00F90, Disassembler.StoreExclusive), // STREXH
            new TableEntry(0x0FF00FFF, 0x01F00F9F, Disassembler.LoadExclusive),  // LDREXH

            // Extra load/store unprivileged instructions
            new TableEntry(0x0F700FF0, 0x002000B0, Disassembler.STRHTReg),  // STRHT (reg)
            new TableEntry(0x0F7000F0, 0x006000B0, Disassembler.STRHTImm),  // STRHT (imm)
            new TableEntry(0x0F700FF0, 0x003000B0, Disassembler.LDRHTReg),  // LDRHT (reg)
            new TableEntry(0x0F7000F0, 0x007000B0, Disassembler.LDRHTImm),  // LDRHT (imm)
            new TableEntry(0x0F700FF0, 0x003000D0, Disassembler.LDRSBTReg), // LDRSBT (reg)
            new TableEntry(0x0F7000F0, 0x007000D0, Disassembler.LDRSBTImm), // LDRSBT (imm)
            new TableEntry(0x0F700FF0, 0x003000F0, Disassembler.LDRSHTReg), // LDRSHT (reg)
            new TableEntry(0x0F7000F0, 0x007000F0, Disassembler.LDRSHTImm), // LDRSHT (imm)

            // Extra load/store instructions
            new TableEntry(0x0E500FF0, 0x000000B0, Disassembler.STRHReg),  // STRH (reg)
            new TableEntry(0x0E500FF0, 0x001000B0, Disassembler.LDRHReg),  // LDRH (reg)
            new TableEntry(0x0E5000F0, 0x004000B0, Disassembler.STRHImm),  // STRH (imm)
            new TableEntry(0x0E5000F0, 0x005000B0, Disassembler.LDRHImm),  // LDRH (imm)
            new TableEntry(0x0E500FF0, 0x000000D0, Disassembler.LDRDReg),  // LDRD (reg)
            new TableEntry(0x0E500FF0, 0x001000D0, Disassembler.LDRSBReg), // LDRSB (reg)
            new TableEntry(0x0E5000F0, 0x004000D0, Disassembler.LDRDImm),  // LDRD (imm)
            new TableEntry(0x0E5000F0, 0x005000D0, Disassembler.LDRSBImm), // LDRSB (imm)
            new TableEntry(0x0E500FF0, 0x000000F0, Disassembler.STRDReg),  // STRD (reg)
            new TableEntry(0x0E500FF0, 0x001000F0, Disassembler.LDRSHReg), // LDRSH (reg)
            new TableEntry(0x0E5000F0, 0x004000F0, Disassembler.STRDImm),  // STRD (imm)
            new TableEntry(0x0E5000F0, 0x005000F0, Disassembler.LDRSHImm), // LDRSH (imm)

            // Data processing instructions (immediate)
            new TableEntry(0x0FE00000, 0x02000000, Disassembler.ANDImm), // AND (imm)
            new TableEntry(0x0FE00000, 0x02200000, Disassembler.EORImm), // EOR (imm)
            new TableEntry(0x0FE00000, 0x02400000, Disassembler.SUBImm), // SUB (imm)
            new TableEntry(0x0FE00000, 0x02600000, Disassembler.RSBImm), // RSB (imm)
            new TableEntry(0x0FE00000, 0x02800000, Disassembler.ADDImm), // ADD (imm)
            new TableEntry(0x0FE00000, 0x02A00000, Disassembler.ADCImm), // ADC (imm)
            new TableEntry(0x0FE00000, 0x02C00000, Disassembler.SBCImm), // SBC (imm)
            new TableEntry(0x0FE00000, 0x02E00000, Disassembler.RSCImm), // RSC (imm)
            new TableEntry(0x0FF0F000, 0x03100000, Disassembler.TSTImm), // TST (imm)
            new TableEntry(0x0FF0F000, 0x03300000, Disassembler.TEQImm), // TEQ (imm)
            new TableEntry(0x0FF0F000, 0x03500000, Disassembler.CMPImm), // CMP (imm)
            new TableEntry(0x0FF0F000, 0x03700000, Disassembler.CMNImm), // CMN (imm)
            new TableEntry(0x0FE00000, 0x03800000, Disassembler.ORRImm), // ORR (imm)
            new TableEntry(0x0FEF0000, 0x03A00000, Disassembler.MOVImm), // MOV (imm)
            new TableEntry(0x0FE00000, 0x03C00000, Disassembler.BICImm), // BIC (imm)
            new TableEntry(0x0FEF0000, 0x03E00000, Disassembler.MVNImm), // MVN (imm)
            new TableEntry(0x0FF00000, 0x03000000, Disassembler.MOVW),   // MOVW
            new TableEntry(0x0FF00000, 0x03400000, Disassembler.MOVT),   // MOVT

            // Hint instructions
            new TableEntry(0x0FFFFFFF, 0x0320F000, Disassembler.NOP),   // NOP
            new TableEntry(0x0FFFFFFF, 0x0320F001, Disassembler.YIELD), // YIELD
            new TableEntry(0x0FFFFFFF, 0x0320F002, Disassembler.WFE),   // WFE
            new TableEntry(0x0FFFFFFF, 0x0320F003, Disassembler.WFI),   // WFI
            new TableEntry(0x0FFFFFFF, 0x0320F004, Disassembler.SEV),   // SEV
            new TableEntry(0x0FFFFFF0, 0x0320F0F0, Disassembler.DBG),   // DBG

            // MSR immediate instructions
            new TableEntry(0x0FFF0000, 0x03240000, Disassembler.MSRImm),
            new TableEntry(0x0FFB0000, 0x03280000, Disassembler.MSRImm),
            new TableEntry(0x0FF30000, 0x03210000, Disassembler.MSRImm),
            new TableEntry(0x0FF20000, 0x03220000, Disassembler.MSRImm),
            new TableEntry(0x0FF00000, 0x03600000, Disassembler.MSRImm),

            // Memory hints
            new TableEntry(0xFF70F000, 0xF450F000, Disassembler.PLIImm), // PLI (imm lit)
            new TableEntry(0xFF70F000, 0xF550F000, Disassembler.PLDImm), // PLD (imm)
            new TableEntry(0xFF70F000, 0xF510F000, Disassembler.PLDImm), // PLDW (imm)
            new TableEntry(0xFFFFF000, 0xF55FF000, Disassembler.PLDImm), // PLD (lit)
            new TableEntry(0xFF70F010, 0xF650F000, Disassembler.PLDReg), // PLI (reg)
            new TableEntry(0xFF70F010, 0xF710F000, Disassembler.PLDReg), // PLD (reg)
            new TableEntry(0xFF70F010, 0xF750F000, Disassembler.PLDReg), // PLDW (reg)

            // Barrier instructions
            new TableEntry(0xFFFFFFF0, 0xF57FF040, Disassembler.Barrier), // DSB
            new TableEntry(0xFFFFFFF0, 0xF57FF050, Disassembler.Barrier), // DMB
            new TableEntry(0xFFFFFFF0, 0xF57FF060, Disassembler.Barrier), // ISB

            // Load/store instructions
            new TableEntry(0x0F700000, 0x04200000, Disassembler.STRTImm),  // STRT (imm)
            new TableEntry(0x0F700010, 0x06200000, Disassembler.STRTReg),  // STRT (reg)
            new TableEntry(0x0FF00000, 0x04000000, Disassembler.STRImm),   // STR (imm)
            new TableEntry(0x0FF00000, 0x04800000, Disassembler.STRImm),   // STR (imm)
            new TableEntry(0x0FF00000, 0x05000000, Disassembler.STRImm),   // STR (imm)
            new TableEntry(0x0FF00000, 0x05200000, Disassembler.STRImm),   // STR (imm)
            new TableEntry(0x0FF00000, 0x05800000, Disassembler.STRImm),   // STR (imm)
            new TableEntry(0x0FF00000, 0x05A00000, Disassembler.STRImm),   // STR (imm)
            new TableEntry(0x0FF00010, 0x06000000, Disassembler.STRReg),   // STR (reg)
            new TableEntry(0x0FF00010, 0x06800000, Disassembler.STRReg),   // STR (reg)
            new TableEntry(0x0FF00010, 0x07000000, Disassembler.STRReg),   // STR (reg)
            new TableEntry(0x0FF00010, 0x07200000, Disassembler.STRReg),   // STR (reg)
            new TableEntry(0x0FF00010, 0x07800000, Disassembler.STRReg),   // STR (reg)
            new TableEntry(0x0FF00010, 0x07A00000, Disassembler.STRReg),   // STR (reg)
            new TableEntry(0x0F700000, 0x04300000, Disassembler.LDRTImm),  // LDRT (imm)
            new TableEntry(0x0F700010, 0x06300000, Disassembler.LDRTReg),  // LDRT (reg)
            new TableEntry(0x0FF00000, 0x04100000, Disassembler.LDRImm),   // LDR (imm)
            new TableEntry(0x0FF00000, 0x04900000, Disassembler.LDRImm),   // LDR (imm)
            new TableEntry(0x0FF00000, 0x05100000, Disassembler.LDRImm),   // LDR (imm)
            new TableEntry(0x0FF00000, 0x05300000, Disassembler.LDRImm),   // LDR (imm)
            new TableEntry(0x0FF00000, 0x05900000, Disassembler.LDRImm),   // LDR (imm)
            new TableEntry(0x0FF00000, 0x05B00000, Disassembler.LDRImm),   // LDR (imm)
            new TableEntry(0x0FF00010, 0x06100000, Disassembler.LDRReg),   // LDR (reg)
            new TableEntry(0x0FF00010, 0x06900000, Disassembler.LDRReg),   // LDR (reg)
            new TableEntry(0x0FF00010, 0x07100000, Disassembler.LDRReg),   // LDR (reg)
            new TableEntry(0x0FF00010, 0x07300000, Disassembler.LDRReg),   // LDR (reg)
            new TableEntry(0x0FF00010, 0x07900000, Disassembler.LDRReg),   // LDR (reg)
            new TableEntry(0x0FF00010, 0x07B00000, Disassembler.LDRReg),   // LDR (reg)
            new TableEntry(0x0F700000, 0x04600000, Disassembler.STRBTImm), // STRBT (imm)
            new TableEntry(0x0F700010, 0x06600000, Disassembler.STRBTReg), // STRBT (reg)
            new TableEntry(0x0FF00000, 0x04400000, Disassembler.STRBImm),  // STRB (imm)
            new TableEntry(0x0FF00000, 0x04C00000, Disassembler.STRBImm),  // STRB (imm)
            new TableEntry(0x0FF00000, 0x05400000, Disassembler.STRBImm),  // STRB (imm)
            new TableEntry(0x0FF00000, 0x05600000, Disassembler.STRBImm),  // STRB (imm)
            new TableEntry(0x0FF00000, 0x05C00000, Disassembler.STRBImm),  // STRB (imm)
            new TableEntry(0x0FF00000, 0x05E00000, Disassembler.STRBImm),  // STRB (imm)
            new TableEntry(0x0FF00010, 0x06400000, Disassembler.STRBReg),  // STRB (reg)
            new TableEntry(0x0FF00010, 0x06C00000, Disassembler.STRBReg),  // STRB (reg)
            new TableEntry(0x0FF00010, 0x07400000, Disassembler.STRBReg),  // STRB (reg)
            new TableEntry(0x0FF00010, 0x07600000, Disassembler.STRBReg),  // STRB (reg)
            new TableEntry(0x0FF00010, 0x07C00000, Disassembler.STRBReg),  // STRB (reg)
            new TableEntry(0x0FF00010, 0x07E00000, Disassembler.STRBReg),  // STRB (reg)
            new TableEntry(0x0F700000, 0x04700000, Disassembler.LDRBTImm), // LDRBT (imm)
            new TableEntry(0x0F700010, 0x06700000, Disassembler.LDRBTReg), // LDRBT (reg)
            new TableEntry(0x0FF00000, 0x04500000, Disassembler.LDRBImm),  // LDRB (imm)
            new TableEntry(0x0FF00000, 0x04D00000, Disassembler.LDRBImm),  // LDRB (imm)
            new TableEntry(0x0FF00000, 0x05500000, Disassembler.LDRBImm),  // LDRB (imm)
            new TableEntry(0x0FF00000, 0x05700000, Disassembler.LDRBImm),  // LDRB (imm)
            new TableEntry(0x0FF00000, 0x05D00000, Disassembler.LDRBImm),  // LDRB (imm)
            new TableEntry(0x0FF00000, 0x05F00000, Disassembler.LDRBImm),  // LDRB (imm)
            new TableEntry(0x0FF00010, 0x06500000, Disassembler.LDRBReg),  // LDRB (reg)
            new TableEntry(0x0FF00010, 0x06D00000, Disassembler.LDRBReg),  // LDRB (reg)
            new TableEntry(0x0FF00010, 0x07500000, Disassembler.LDRBReg),  // LDRB (reg)
            new TableEntry(0x0FF00010, 0x07700000, Disassembler.LDRBReg),  // LDRB (reg)
            new TableEntry(0x0FF00010, 0x07D00000, Disassembler.LDRBReg),  // LDRB (reg)
            new TableEntry(0x0FF00010, 0x07F00000, Disassembler.LDRBReg),  // LDRB (reg)

            // Parallel add and subtract (modulo) instructions
            new TableEntry(0x0FF00FF0, 0x06100F10, Disassembler.ParallelAddSubModuloSigned),     // SADD16
            new TableEntry(0x0FF00FF0, 0x06100F30, Disassembler.ParallelAddSubModuloSigned),     // SASX
            new TableEntry(0x0FF00FF0, 0x06100F50, Disassembler.ParallelAddSubModuloSigned),     // SSAX
            new TableEntry(0x0FF00FF0, 0x06100F70, Disassembler.ParallelAddSubModuloSigned),     // SSUB16
            new TableEntry(0x0FF00FF0, 0x06100F90, Disassembler.ParallelAddSubModuloSigned),     // SADD8
            new TableEntry(0x0FF00FF0, 0x06100FF0, Disassembler.ParallelAddSubModuloSigned),     // SSUB8
            new TableEntry(0x0FF00FF0, 0x06500F10, Disassembler.ParallelAddSubModuloUnsigned),   // UADD16
            new TableEntry(0x0FF00FF0, 0x06500F30, Disassembler.ParallelAddSubModuloUnsigned),   // UASX
            new TableEntry(0x0FF00FF0, 0x06500F50, Disassembler.ParallelAddSubModuloUnsigned),   // USAX
            new TableEntry(0x0FF00FF0, 0x06500F70, Disassembler.ParallelAddSubModuloUnsigned),   // USUB16
            new TableEntry(0x0FF00FF0, 0x06500F90, Disassembler.ParallelAddSubModuloUnsigned),   // UADD8
            new TableEntry(0x0FF00FF0, 0x06500FF0, Disassembler.ParallelAddSubModuloUnsigned),   // USUB8

            // Parallel add and subtract (saturating) instructions
            new TableEntry(0x0FF00FF0, 0x06200F10, Disassembler.ParallelAddSubSaturateSigned),   // QADD16
            new TableEntry(0x0FF00FF0, 0x06200F30, Disassembler.ParallelAddSubSaturateSigned),   // QASX
            new TableEntry(0x0FF00FF0, 0x06200F50, Disassembler.ParallelAddSubSaturateSigned),   // QSAX
            new TableEntry(0x0FF00FF0, 0x06200F70, Disassembler.ParallelAddSubSaturateSigned),   // QSUB16
            new TableEntry(0x0FF00FF0, 0x06200F90, Disassembler.ParallelAddSubSaturateSigned),   // QADD8
            new TableEntry(0x0FF00FF0, 0x06200FF0, Disassembler.ParallelAddSubSaturateSigned),   // QSUB8
            new TableEntry(0x0FF00FF0, 0x06600F10, Disassembler.ParallelAddSubSaturateUnsigned), // UQADD16
            new TableEntry(0x0FF00FF0, 0x06600F30, Disassembler.ParallelAddSubSaturateUnsigned), // UQASX
            new TableEntry(0x0FF00FF0, 0x06600F50, Disassembler.ParallelAddSubSaturateUnsigned), // UQSAX
            new TableEntry(0x0FF00FF0, 0x06600F70, Disassembler.ParallelAddSubSaturateUnsigned), // UQSUB16
            new TableEntry(0x0FF00FF0, 0x06600F90, Disassembler.ParallelAddSubSaturateUnsigned), // UQADD8
            new TableEntry(0x0FF00FF0, 0x06600FF0, Disassembler.ParallelAddSubSaturateUnsigned), // UQSUB8

            // Parallel add and subtract (halving) instructions
            new TableEntry(0x0FF00FF0, 0x06300F10, Disassembler.ParallelAddSubHalvingSigned),    // SHADD16
            new TableEntry(0x0FF00FF0, 0x06300F30, Disassembler.ParallelAddSubHalvingSigned),    // SHASX
            new TableEntry(0x0FF00FF0, 0x06300F50, Disassembler.ParallelAddSubHalvingSigned),    // SHSAX
            new TableEntry(0x0FF00FF0, 0x06300F70, Disassembler.ParallelAddSubHalvingSigned),    // SHSUB16
            new TableEntry(0x0FF00FF0, 0x06300F90, Disassembler.ParallelAddSubHalvingSigned),    // SHADD8
            new TableEntry(0x0FF00FF0, 0x06300FF0, Disassembler.ParallelAddSubHalvingSigned),    // SHSUB8
            new TableEntry(0x0FF00FF0, 0x06700F10, Disassembler.ParallelAddSubHalvingUnsigned),  // UHADD16
            new TableEntry(0x0FF00FF0, 0x06700F30, Disassembler.ParallelAddSubHalvingUnsigned),  // UHASX
            new TableEntry(0x0FF00FF0, 0x06700F50, Disassembler.ParallelAddSubHalvingUnsigned),  // UHSAX
            new TableEntry(0x0FF00FF0, 0x06700F70, Disassembler.ParallelAddSubHalvingUnsigned),  // UHSUB16
            new TableEntry(0x0FF00FF0, 0x06700F90, Disassembler.ParallelAddSubHalvingUnsigned),  // UHADD8
            new TableEntry(0x0FF00FF0, 0x06700FF0, Disassembler.ParallelAddSubHalvingUnsigned),  // UHSUB8

            // Extension instructions
            new TableEntry(0x0FFF03F0, 0x06AF0070, Disassembler.Extend),       // SXTB
            new TableEntry(0x0FFF03F0, 0x068F0070, Disassembler.Extend),       // SXTB16
            new TableEntry(0x0FFF03F0, 0x06BF0070, Disassembler.Extend),       // SXTH
            new TableEntry(0x0FF003F0, 0x06A00070, Disassembler.ExtendAndAdd), // SXTAB
            new TableEntry(0x0FF003F0, 0x06800070, Disassembler.ExtendAndAdd), // SXTAB16
            new TableEntry(0x0FF003F0, 0x06B00070, Disassembler.ExtendAndAdd), // SXTAH
            new TableEntry(0x0FFF03F0, 0x06EF0070, Disassembler.Extend),       // UXTB
            new TableEntry(0x0FFF03F0, 0x06CF0070, Disassembler.Extend),       // UXTB16
            new TableEntry(0x0FFF03F0, 0x06FF0070, Disassembler.Extend),       // UXTH
            new TableEntry(0x0FF003F0, 0x06E00070, Disassembler.ExtendAndAdd), // UXTAB
            new TableEntry(0x0FF003F0, 0x06C00070, Disassembler.ExtendAndAdd), // UXTAB16
            new TableEntry(0x0FF003F0, 0x06F00070, Disassembler.ExtendAndAdd), // UXTAH

            // Selection instruction
            new TableEntry(0x0FF00FF0, 0x06800FB0, Disassembler.SEL), // SEL

            // Packing instructions
            new TableEntry(0x0FF00030, 0x06800010, Disassembler.PKH), // PKH

            // Reversal instructions
            new TableEntry(0x0FFF0FF0, 0x06FF0F30, Disassembler.RBIT),        // RBIT
            new TableEntry(0x0FFF0FF0, 0x06BF0F30, Disassembler.ByteReverse), // REV
            new TableEntry(0x0FFF0FF0, 0x06BF0FB0, Disassembler.ByteReverse), // REV16
            new TableEntry(0x0FFF0FF0, 0x06FF0FB0, Disassembler.ByteReverse), // REVSH

            // Saturation instructions
            new TableEntry(0x0FE00030, 0x06A00010, Disassembler.Saturate),   // SSAT
            new TableEntry(0x0FF00FF0, 0x06A00F30, Disassembler.Saturate16), // SSAT16
            new TableEntry(0x0FE00030, 0x06E00010, Disassembler.Saturate),   // USAT
            new TableEntry(0x0FF00FF0, 0x06E00F30, Disassembler.Saturate16), // USAT16

            // Signed multiply
            new TableEntry(0x0FF0F0D0, 0x0700F010, Disassembler.SignedDualMultiply),                // SMUAD
            new TableEntry(0x0FF0F0D0, 0x0700F050, Disassembler.SignedDualMultiply),                // SMUSD
            new TableEntry(0x0FF000D0, 0x07000010, Disassembler.SignedDualMultiply),                // SMLAD
            new TableEntry(0x0FF000D0, 0x07000050, Disassembler.SignedDualMultiply),                // SMLSD
            new TableEntry(0x0FF000D0, 0x07400010, Disassembler.SignedLongDualMultiply),            // SMLALD
            new TableEntry(0x0FF000D0, 0x07400050, Disassembler.SignedLongDualMultiply),            // SMLSLD
            new TableEntry(0x0FF0F0D0, 0x0750F010, Disassembler.SignedMostSignificantWordMultiply), // SMMUL
            new TableEntry(0x0FF000D0, 0x07500010, Disassembler.SignedMostSignificantWordMultiply), // SMMLA
            new TableEntry(0x0FF000D0, 0x075000D0, Disassembler.SignedMostSignificantWordMultiply), // SMMLS

            // Divide
            new TableEntry(0x0FF0F0F0, 0x0730F010, Disassembler.Divide), // UDIV
            new TableEntry(0x0FF0F0F0, 0x0710F010, Disassembler.Divide), // SDIV

            // Unsigned sum of absolute difference instructions
            new TableEntry(0x0FF0F0F0, 0x0780F010, Disassembler.UnsignedSumAbsoluteDifference), // USAD8
            new TableEntry(0x0FF000F0, 0x07800010, Disassembler.UnsignedSumAbsoluteDifference), // USASA8

            // Bit field instructions
            new TableEntry(0x0FE00070, 0x07A00050, Disassembler.BitFieldExtract),       // SBFX
            new TableEntry(0x0FE0007F, 0x07C0001F, Disassembler.BitFieldClearOrInsert), // BFC
            new TableEntry(0x0FE00070, 0x07C00010, Disassembler.BitFieldClearOrInsert), // BFI
            new TableEntry(0x0FE00070, 0x07E00050, Disassembler.BitFieldExtract),       // UBFX

            // Undefined instruction
            new TableEntry(0xFFF000F0, 0xE7F000F0, Disassembler.UDF), // UDF

            // Load/store multiple instructions
            new TableEntry(0x0FD00000, 0x08000000, Disassembler.STMDA), // STMDA/STMED
            new TableEntry(0x0FD00000, 0x08100000, Disassembler.LDMDA), // LDMDA/LDMFA
            new TableEntry(0x0FD00000, 0x08800000, Disassembler.STMIA), // STMIA/STMEA
            new TableEntry(0x0FD00000, 0x08900000, Disassembler.LDMIA), // LDMIA/LDMFD
            new TableEntry(0x0FD00000, 0x09000000, Disassembler.STMDB), // STMDB/STMFD
            new TableEntry(0x0FD00000, 0x09100000, Disassembler.LDMDB), // LDMDB/LDMEA
            new TableEntry(0x0FD00000, 0x09800000, Disassembler.STMIB), // STMIB/STMFA
            new TableEntry(0x0FD00000, 0x09900000, Disassembler.LDMIB), // LDMIB/LDMED
            new TableEntry(0x0E700000, 0x08400000, null), // STM (user reg)
            new TableEntry(0x0E508000, 0x08500000, null), // LDM (user reg)
            new TableEntry(0x0E508000, 0x08508000, null), // LDM (exc ret)

            // Branch instructions (ARM separates these from other branch instructions)
            new TableEntry(0xFE000000, 0xFA000000, Disassembler.BLImm), // BLX (imm)
            new TableEntry(0x0F000000, 0x0A000000, Disassembler.B),     // B
            new TableEntry(0x0F000000, 0x0B000000, Disassembler.BLImm), // BL (imm)

            // Coprocessor instructions and supervisor call
            new TableEntry(0x0F000000, 0x0F000000, Disassembler.SVC), // SVC
            new TableEntry(0xFFF00000, 0xFC400000, Disassembler.MoveTwoCoprocessorRegisters), // MCRR2
            new TableEntry(0x0FF00000, 0x0C400000, Disassembler.MoveTwoCoprocessorRegisters), // MCRR
            new TableEntry(0xFFF00000, 0xFC500000, Disassembler.MoveTwoCoprocessorRegisters), // MRRC2
            new TableEntry(0x0FF00000, 0x0C500000, Disassembler.MoveTwoCoprocessorRegisters), // MRRC
            new TableEntry(0xFE100000, 0xFC000000, Disassembler.LoadStoreCoprocessor),        // STC2
            new TableEntry(0x0E100000, 0x0C000000, Disassembler.LoadStoreCoprocessor),        // STC
            new TableEntry(0xFE100000, 0xFC100000, Disassembler.LoadStoreCoprocessor),        // LDC2
            new TableEntry(0x0E100000, 0x0C100000, Disassembler.LoadStoreCoprocessor),        // LDC
            new TableEntry(0xFF100010, 0xFE000010, Disassembler.MoveOneCoprocessorRegister),  // MCR2
            new TableEntry(0x0F100010, 0x0E000010, Disassembler.MoveOneCoprocessorRegister),  // MCR
            new TableEntry(0xFF100010, 0xFE100010, Disassembler.MoveOneCoprocessorRegister),  // MRC2
            new TableEntry(0x0F100010, 0x0E100010, Disassembler.MoveOneCoprocessorRegister),  // MRC
            new TableEntry(0xFF000010, 0xFE000010, Disassembler.CDP), // CDP2
            new TableEntry(0x0F000010, 0x0E000000, Disassembler.CDP), // CDP
        };

        #endregion

        #region Constructors

        // Intended to be uninstantiable.
        private InstructionDecoder()
        {
        }

        #endregion

        #region Decoding Routine

        /// <summary>
        /// Attempts to disassemble an arbitrary ARM instruction opcode.
        /// </summary>
        /// <returns><c>true</c>, if opcode was decoded, <c>false</c> otherwise.</returns>
        /// <param name="opcode">Opcode.</param>
        public static DecodeResult Decode(uint opcode)
        {
            var decodedOpcode = armInstructionTable.FirstOrDefault(entry => entry.Matches(opcode));
            if (decodedOpcode == null)
                return new DecodeResult(false, null);

            return new DecodeResult(true, decodedOpcode.DisassemblyFunction);
        }

        #endregion
    }
}
