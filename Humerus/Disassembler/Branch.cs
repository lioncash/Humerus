using System;

namespace Humerus
{
    // Disassembly methods for branch instructions
    public static partial class Disassembler
    {
        internal static string B(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var imm = (int)((opcode.ExtractBits(0, 23) << 2).SignExtend(26) + 8);

            return $"B{cond} {imm.SignBitToChar()}#0x{Math.Abs(imm):X8}";
        }

        internal static string BLImm(uint opcode)
        {
            var condBits = opcode & 0xF0000000;

            // Unconditional
            if (condBits == 0xF0000000)
            {
                var h     = opcode.ExtractBit(24);
                var imm24 = opcode.ExtractBits(0, 23);
                var imm32 = (int)((imm24 << 2) | (h << 1)).SignExtend(26) + 8;

                return $"BLX {imm32.SignBitToChar()}#0x{Math.Abs(imm32):X8}";
            }
            else
            {
                var cond  = DisassemblerUtils.ARMConditionCode(opcode);
                var imm32 = (int)(opcode.ExtractBits(0, 23) << 2).SignExtend(26) + 8;

                return $"BL{cond} {imm32.SignBitToChar()}#0x{Math.Abs(imm32):X8}";
            }
        }

        internal static string BLXReg(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var reg = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));

            return $"BLX{cond} {reg}";
        }

        internal static string BX(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var reg = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));

            return $"BX{cond} {reg}";
        }

        internal static string BXJ(uint opcode)
        {
            var cond = DisassemblerUtils.ARMConditionCode(opcode);
            var reg = DisassemblerUtils.RegisterName(opcode.ExtractBits(0, 3));

            return $"BXJ{cond} {reg}";
        }
    }
}
