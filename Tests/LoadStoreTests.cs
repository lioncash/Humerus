using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Load/store instruction tests
    /// </summary>
    [TestFixture]
    public sealed class LoadStoreTests
    {
        [Test]
        public void LDRImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE5921000), Is.EqualTo("LDR r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE5921004), Is.EqualTo("LDR r1, [r2, #4]"));
            Assert.That(Disassembler.Disassemble(0xE5921FFF), Is.EqualTo("LDR r1, [r2, #4095]"));
            Assert.That(Disassembler.Disassemble(0xE5B21004), Is.EqualTo("LDR r1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xE4921004), Is.EqualTo("LDR r1, [r2], #4"));
        }

        [Test]
        public void LDRRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE7921003), Is.EqualTo("LDR r1, [r2, r3]"));
            Assert.That(Disassembler.Disassemble(0xE7921F03), Is.EqualTo("LDR r1, [r2, r3, LSL #30]"));
            Assert.That(Disassembler.Disassemble(0xE7921F23), Is.EqualTo("LDR r1, [r2, r3, LSR #30]"));
            Assert.That(Disassembler.Disassemble(0xE7921FC3), Is.EqualTo("LDR r1, [r2, r3, ASR #31]"));
            Assert.That(Disassembler.Disassemble(0xE7921F63), Is.EqualTo("LDR r1, [r2, r3, ROR #30]"));
            Assert.That(Disassembler.Disassemble(0xE7921063), Is.EqualTo("LDR r1, [r2, r3, RRX]"));
            Assert.That(Disassembler.Disassemble(0xE6921003), Is.EqualTo("LDR r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE6921F03), Is.EqualTo("LDR r1, [r2], r3, LSL #30"));
            Assert.That(Disassembler.Disassemble(0xE6921F23), Is.EqualTo("LDR r1, [r2], r3, LSR #30"));
            Assert.That(Disassembler.Disassemble(0xE6921FC3), Is.EqualTo("LDR r1, [r2], r3, ASR #31"));
            Assert.That(Disassembler.Disassemble(0xE6921F63), Is.EqualTo("LDR r1, [r2], r3, ROR #30"));
            Assert.That(Disassembler.Disassemble(0xE6921063), Is.EqualTo("LDR r1, [r2], r3, RRX"));
        }

        [Test]
        public void LDRTImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE4B21000), Is.EqualTo("LDRT r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE4B21004), Is.EqualTo("LDRT r1, [r2], #4"));
            Assert.That(Disassembler.Disassemble(0xE4B21FFF), Is.EqualTo("LDRT r1, [r2], #4095"));
        }

        [Test]
        public void LDRTRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE6B21003), Is.EqualTo("LDRT r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE6321003), Is.EqualTo("LDRT r1, [r2], -r3"));
            Assert.That(Disassembler.Disassemble(0xE6B21F03), Is.EqualTo("LDRT r1, [r2], r3, LSL #30"));
            Assert.That(Disassembler.Disassemble(0xE6B21F23), Is.EqualTo("LDRT r1, [r2], r3, LSR #30"));
            Assert.That(Disassembler.Disassemble(0xE6B21FC3), Is.EqualTo("LDRT r1, [r2], r3, ASR #31"));
            Assert.That(Disassembler.Disassemble(0xE6B21F63), Is.EqualTo("LDRT r1, [r2], r3, ROR #30"));
            Assert.That(Disassembler.Disassemble(0xE6B21063), Is.EqualTo("LDRT r1, [r2], r3, RRX"));
        }

        [Test]
        public void LDRBImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE5D21000), Is.EqualTo("LDRB r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE5D21004), Is.EqualTo("LDRB r1, [r2, #4]"));
            Assert.That(Disassembler.Disassemble(0xE5D21FFF), Is.EqualTo("LDRB r1, [r2, #4095]"));
            Assert.That(Disassembler.Disassemble(0xE5F21004), Is.EqualTo("LDRB r1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xE4D21004), Is.EqualTo("LDRB r1, [r2], #4"));
        }

        [Test]
        public void LDRBRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE7D21003), Is.EqualTo("LDRB r1, [r2, r3]"));
            Assert.That(Disassembler.Disassemble(0xE7D21F03), Is.EqualTo("LDRB r1, [r2, r3, LSL #30]"));
            Assert.That(Disassembler.Disassemble(0xE7D21F23), Is.EqualTo("LDRB r1, [r2, r3, LSR #30]"));
            Assert.That(Disassembler.Disassemble(0xE7D21FC3), Is.EqualTo("LDRB r1, [r2, r3, ASR #31]"));
            Assert.That(Disassembler.Disassemble(0xE7D21F63), Is.EqualTo("LDRB r1, [r2, r3, ROR #30]"));
            Assert.That(Disassembler.Disassemble(0xE7D21063), Is.EqualTo("LDRB r1, [r2, r3, RRX]"));
            Assert.That(Disassembler.Disassemble(0xE6D21003), Is.EqualTo("LDRB r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE6D21F03), Is.EqualTo("LDRB r1, [r2], r3, LSL #30"));
            Assert.That(Disassembler.Disassemble(0xE6D21F23), Is.EqualTo("LDRB r1, [r2], r3, LSR #30"));
            Assert.That(Disassembler.Disassemble(0xE6D21FC3), Is.EqualTo("LDRB r1, [r2], r3, ASR #31"));
            Assert.That(Disassembler.Disassemble(0xE6D21F63), Is.EqualTo("LDRB r1, [r2], r3, ROR #30"));
            Assert.That(Disassembler.Disassemble(0xE6D21063), Is.EqualTo("LDRB r1, [r2], r3, RRX"));
        }

        [Test]
        public void LDRBTImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE4F21000), Is.EqualTo("LDRBT r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE4F21004), Is.EqualTo("LDRBT r1, [r2], #4"));
        }

        [Test]
        public void LDRBTRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE6F21003), Is.EqualTo("LDRBT r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE6F21F03), Is.EqualTo("LDRBT r1, [r2], r3, LSL #30"));
            Assert.That(Disassembler.Disassemble(0xE6F21F23), Is.EqualTo("LDRBT r1, [r2], r3, LSR #30"));
            Assert.That(Disassembler.Disassemble(0xE6F21FC3), Is.EqualTo("LDRBT r1, [r2], r3, ASR #31"));
            Assert.That(Disassembler.Disassemble(0xE6F21F63), Is.EqualTo("LDRBT r1, [r2], r3, ROR #30"));
            Assert.That(Disassembler.Disassemble(0xE6F21063), Is.EqualTo("LDRBT r1, [r2], r3, RRX"));
        }

        [Test]
        public void LDRDImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE1C200D0), Is.EqualTo("LDRD r0, r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE1C200D4), Is.EqualTo("LDRD r0, r1, [r2, #4]"));
            Assert.That(Disassembler.Disassemble(0xE1C20FDF), Is.EqualTo("LDRD r0, r1, [r2, #255]"));
            Assert.That(Disassembler.Disassemble(0xE1E200D4), Is.EqualTo("LDRD r0, r1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xE0C200D4), Is.EqualTo("LDRD r0, r1, [r2], #4"));
        }

        [Test]
        public void LDRDRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE18200D3), Is.EqualTo("LDRD r0, r1, [r2, r3]"));
            Assert.That(Disassembler.Disassemble(0xE10200D3), Is.EqualTo("LDRD r0, r1, [r2, -r3]"));
            Assert.That(Disassembler.Disassemble(0xE1A200D3), Is.EqualTo("LDRD r0, r1, [r2, r3]!"));
            Assert.That(Disassembler.Disassemble(0xE12200D3), Is.EqualTo("LDRD r0, r1, [r2, -r3]!"));

            Assert.That(Disassembler.Disassemble(0xE08200D3), Is.EqualTo("LDRD r0, r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE00200D3), Is.EqualTo("LDRD r0, r1, [r2], -r3"));
        }

        [Test]
        public void LDRHImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE1D210B0), Is.EqualTo("LDRH r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE1D210B4), Is.EqualTo("LDRH r1, [r2, #4]"));
            Assert.That(Disassembler.Disassemble(0xE1D21FBF), Is.EqualTo("LDRH r1, [r2, #255]"));
            Assert.That(Disassembler.Disassemble(0xE1F210B4), Is.EqualTo("LDRH r1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xE0D210B4), Is.EqualTo("LDRH r1, [r2], #4"));
        }

        [Test]
        public void LDRHRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE19210B3), Is.EqualTo("LDRH r1, [r2, r3]"));
            Assert.That(Disassembler.Disassemble(0xE11210B3), Is.EqualTo("LDRH r1, [r2, -r3]"));
            Assert.That(Disassembler.Disassemble(0xE1B210B3), Is.EqualTo("LDRH r1, [r2, r3]!"));
            Assert.That(Disassembler.Disassemble(0xE13210B3), Is.EqualTo("LDRH r1, [r2, -r3]!"));

            Assert.That(Disassembler.Disassemble(0xE09210B3), Is.EqualTo("LDRH r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE01210B3), Is.EqualTo("LDRH r1, [r2], -r3"));
        }

        [Test]
        public void LDRHTImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE0F210B0), Is.EqualTo("LDRHT r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE0F210B4), Is.EqualTo("LDRHT r1, [r2], #4"));
        }

        [Test]
        public void LDRHTRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE0B210B3), Is.EqualTo("LDRHT r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE03210B3), Is.EqualTo("LDRHT r1, [r2], -r3"));
        }

        [Test]
        public void LDRSBImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE1D210D0), Is.EqualTo("LDRSB r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE1D210D4), Is.EqualTo("LDRSB r1, [r2, #4]"));
            Assert.That(Disassembler.Disassemble(0xE1D21FDF), Is.EqualTo("LDRSB r1, [r2, #255]"));
            Assert.That(Disassembler.Disassemble(0xE1F210D4), Is.EqualTo("LDRSB r1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xE0D210D4), Is.EqualTo("LDRSB r1, [r2], #4"));
        }

        [Test]
        public void LDRSBRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE19210D3), Is.EqualTo("LDRSB r1, [r2, r3]"));
            Assert.That(Disassembler.Disassemble(0xE11210D3), Is.EqualTo("LDRSB r1, [r2, -r3]"));
            Assert.That(Disassembler.Disassemble(0xE1B210D3), Is.EqualTo("LDRSB r1, [r2, r3]!"));
            Assert.That(Disassembler.Disassemble(0xE13210D3), Is.EqualTo("LDRSB r1, [r2, -r3]!"));

            Assert.That(Disassembler.Disassemble(0xE09210D3), Is.EqualTo("LDRSB r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE01210D3), Is.EqualTo("LDRSB r1, [r2], -r3"));
        }

        [Test]
        public void LDRSBTImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE0F210D0), Is.EqualTo("LDRSBT r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE0F210D4), Is.EqualTo("LDRSBT r1, [r2], #4"));
        }

        [Test]
        public void LDRSBTRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE0B210D3), Is.EqualTo("LDRSBT r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE03210D3), Is.EqualTo("LDRSBT r1, [r2], -r3"));
        }

        [Test]
        public void LDRSHImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE1D210F0), Is.EqualTo("LDRSH r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE1D210F4), Is.EqualTo("LDRSH r1, [r2, #4]"));
            Assert.That(Disassembler.Disassemble(0xE1D21FFF), Is.EqualTo("LDRSH r1, [r2, #255]"));
            Assert.That(Disassembler.Disassemble(0xE1F210F4), Is.EqualTo("LDRSH r1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xE0D210F4), Is.EqualTo("LDRSH r1, [r2], #4"));
        }

        [Test]
        public void LDRSHRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE19210F3), Is.EqualTo("LDRSH r1, [r2, r3]"));
            Assert.That(Disassembler.Disassemble(0xE11210F3), Is.EqualTo("LDRSH r1, [r2, -r3]"));
            Assert.That(Disassembler.Disassemble(0xE1B210F3), Is.EqualTo("LDRSH r1, [r2, r3]!"));
            Assert.That(Disassembler.Disassemble(0xE13210F3), Is.EqualTo("LDRSH r1, [r2, -r3]!"));

            Assert.That(Disassembler.Disassemble(0xE09210F3), Is.EqualTo("LDRSH r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE01210F3), Is.EqualTo("LDRSH r1, [r2], -r3"));
        }

        [Test]
        public void LDRSHTImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE0F210F0), Is.EqualTo("LDRSHT r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE0F210F4), Is.EqualTo("LDRSHT r1, [r2], #4"));
        }

        [Test]
        public void LDRSHTRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE0B210F3), Is.EqualTo("LDRSHT r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE03210F3), Is.EqualTo("LDRSHT r1, [r2], -r3"));
        }

        [Test]
        public void STRImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE5821000), Is.EqualTo("STR r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE5821004), Is.EqualTo("STR r1, [r2, #4]"));
            Assert.That(Disassembler.Disassemble(0xE5821FFF), Is.EqualTo("STR r1, [r2, #4095]"));
            Assert.That(Disassembler.Disassemble(0xE5A21004), Is.EqualTo("STR r1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xE4821004), Is.EqualTo("STR r1, [r2], #4"));
        }

        [Test]
        public void STRRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE7821003), Is.EqualTo("STR r1, [r2, r3]"));
            Assert.That(Disassembler.Disassemble(0xE7021003), Is.EqualTo("STR r1, [r2, -r3]"));
            Assert.That(Disassembler.Disassemble(0xE7A21003), Is.EqualTo("STR r1, [r2, r3]!"));
            Assert.That(Disassembler.Disassemble(0xE7221003), Is.EqualTo("STR r1, [r2, -r3]!"));
            Assert.That(Disassembler.Disassemble(0xE7821F03), Is.EqualTo("STR r1, [r2, r3, LSL #30]"));
            Assert.That(Disassembler.Disassemble(0xE7821F23), Is.EqualTo("STR r1, [r2, r3, LSR #30]"));
            Assert.That(Disassembler.Disassemble(0xE7821FC3), Is.EqualTo("STR r1, [r2, r3, ASR #31]"));
            Assert.That(Disassembler.Disassemble(0xE7821F63), Is.EqualTo("STR r1, [r2, r3, ROR #30]"));
            Assert.That(Disassembler.Disassemble(0xE7821063), Is.EqualTo("STR r1, [r2, r3, RRX]"));
            Assert.That(Disassembler.Disassemble(0xE6821003), Is.EqualTo("STR r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE6021003), Is.EqualTo("STR r1, [r2], -r3"));
            Assert.That(Disassembler.Disassemble(0xE6821F03), Is.EqualTo("STR r1, [r2], r3, LSL #30"));
            Assert.That(Disassembler.Disassemble(0xE6821F23), Is.EqualTo("STR r1, [r2], r3, LSR #30"));
            Assert.That(Disassembler.Disassemble(0xE6821FC3), Is.EqualTo("STR r1, [r2], r3, ASR #31"));
            Assert.That(Disassembler.Disassemble(0xE6821F63), Is.EqualTo("STR r1, [r2], r3, ROR #30"));
            Assert.That(Disassembler.Disassemble(0xE6821063), Is.EqualTo("STR r1, [r2], r3, RRX"));
        }

        [Test]
        public void STRTImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE4A21000), Is.EqualTo("STRT r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE4A21FFF), Is.EqualTo("STRT r1, [r2], #4095"));
        }

        [Test]
        public void STRTRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE6A21003), Is.EqualTo("STRT r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE6221003), Is.EqualTo("STRT r1, [r2], -r3"));
            Assert.That(Disassembler.Disassemble(0xE6A21F03), Is.EqualTo("STRT r1, [r2], r3, LSL #30"));
            Assert.That(Disassembler.Disassemble(0xE6A21F23), Is.EqualTo("STRT r1, [r2], r3, LSR #30"));
            Assert.That(Disassembler.Disassemble(0xE6A21FC3), Is.EqualTo("STRT r1, [r2], r3, ASR #31"));
            Assert.That(Disassembler.Disassemble(0xE6A21F63), Is.EqualTo("STRT r1, [r2], r3, ROR #30"));
            Assert.That(Disassembler.Disassemble(0xE6A21063), Is.EqualTo("STRT r1, [r2], r3, RRX"));
        }

        [Test]
        public void STRBImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE5C21000), Is.EqualTo("STRB r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE5C21004), Is.EqualTo("STRB r1, [r2, #4]"));
            Assert.That(Disassembler.Disassemble(0xE5C21FFF), Is.EqualTo("STRB r1, [r2, #4095]"));
            Assert.That(Disassembler.Disassemble(0xE5E21004), Is.EqualTo("STRB r1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xE4C21004), Is.EqualTo("STRB r1, [r2], #4"));
        }

        [Test]
        public void STRBRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE7C21003), Is.EqualTo("STRB r1, [r2, r3]"));
            Assert.That(Disassembler.Disassemble(0xE7E21003), Is.EqualTo("STRB r1, [r2, r3]!"));
            Assert.That(Disassembler.Disassemble(0xE7421003), Is.EqualTo("STRB r1, [r2, -r3]"));
            Assert.That(Disassembler.Disassemble(0xE7621003), Is.EqualTo("STRB r1, [r2, -r3]!"));
            Assert.That(Disassembler.Disassemble(0xE7C21F03), Is.EqualTo("STRB r1, [r2, r3, LSL #30]"));
            Assert.That(Disassembler.Disassemble(0xE7C21F23), Is.EqualTo("STRB r1, [r2, r3, LSR #30]"));
            Assert.That(Disassembler.Disassemble(0xE7C21FC3), Is.EqualTo("STRB r1, [r2, r3, ASR #31]"));
            Assert.That(Disassembler.Disassemble(0xE7C21F63), Is.EqualTo("STRB r1, [r2, r3, ROR #30]"));
            Assert.That(Disassembler.Disassemble(0xE7C21063), Is.EqualTo("STRB r1, [r2, r3, RRX]"));
            Assert.That(Disassembler.Disassemble(0xE6C21003), Is.EqualTo("STRB r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE6421003), Is.EqualTo("STRB r1, [r2], -r3"));
            Assert.That(Disassembler.Disassemble(0xE6C21F03), Is.EqualTo("STRB r1, [r2], r3, LSL #30"));
            Assert.That(Disassembler.Disassemble(0xE6C21F23), Is.EqualTo("STRB r1, [r2], r3, LSR #30"));
            Assert.That(Disassembler.Disassemble(0xE6C21FC3), Is.EqualTo("STRB r1, [r2], r3, ASR #31"));
            Assert.That(Disassembler.Disassemble(0xE6C21F63), Is.EqualTo("STRB r1, [r2], r3, ROR #30"));
            Assert.That(Disassembler.Disassemble(0xE6C21063), Is.EqualTo("STRB r1, [r2], r3, RRX"));
        }

        [Test]
        public void STRBTImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE4E21000), Is.EqualTo("STRBT r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE4E21FFF), Is.EqualTo("STRBT r1, [r2], #4095"));
        }

        [Test]
        public void STRBTRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE6E21003), Is.EqualTo("STRBT r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE6621003), Is.EqualTo("STRBT r1, [r2], -r3"));
            Assert.That(Disassembler.Disassemble(0xE6E21F03), Is.EqualTo("STRBT r1, [r2], r3, LSL #30"));
            Assert.That(Disassembler.Disassemble(0xE6E21F23), Is.EqualTo("STRBT r1, [r2], r3, LSR #30"));
            Assert.That(Disassembler.Disassemble(0xE6E21FC3), Is.EqualTo("STRBT r1, [r2], r3, ASR #31"));
            Assert.That(Disassembler.Disassemble(0xE6E21F63), Is.EqualTo("STRBT r1, [r2], r3, ROR #30"));
            Assert.That(Disassembler.Disassemble(0xE6E21063), Is.EqualTo("STRBT r1, [r2], r3, RRX"));
        }

        [Test]
        public void STRDImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE1C200F0), Is.EqualTo("STRD r0, r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE1C200F4), Is.EqualTo("STRD r0, r1, [r2, #4]"));
            Assert.That(Disassembler.Disassemble(0xE1C20FFF), Is.EqualTo("STRD r0, r1, [r2, #255]"));
            Assert.That(Disassembler.Disassemble(0xE1E200F4), Is.EqualTo("STRD r0, r1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xE0C200F4), Is.EqualTo("STRD r0, r1, [r2], #4"));
        }

        [Test]
        public void STRDRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE18200F3), Is.EqualTo("STRD r0, r1, [r2, r3]"));
            Assert.That(Disassembler.Disassemble(0xE10200F3), Is.EqualTo("STRD r0, r1, [r2, -r3]"));
            Assert.That(Disassembler.Disassemble(0xE1A200F3), Is.EqualTo("STRD r0, r1, [r2, r3]!"));
            Assert.That(Disassembler.Disassemble(0xE12200F3), Is.EqualTo("STRD r0, r1, [r2, -r3]!"));

            Assert.That(Disassembler.Disassemble(0xE08200F3), Is.EqualTo("STRD r0, r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE00200F3), Is.EqualTo("STRD r0, r1, [r2], -r3"));
        }

        [Test]
        public void STRHImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE1C210B0), Is.EqualTo("STRH r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE1C210B4), Is.EqualTo("STRH r1, [r2, #4]"));
            Assert.That(Disassembler.Disassemble(0xE1C21FBF), Is.EqualTo("STRH r1, [r2, #255]"));
            Assert.That(Disassembler.Disassemble(0xE1E210B4), Is.EqualTo("STRH r1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xE0C210B4), Is.EqualTo("STRH r1, [r2], #4"));
        }

        [Test]
        public void STRHRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE18210B3), Is.EqualTo("STRH r1, [r2, r3]"));
            Assert.That(Disassembler.Disassemble(0xE10210B3), Is.EqualTo("STRH r1, [r2, -r3]"));
            Assert.That(Disassembler.Disassemble(0xE1A210B3), Is.EqualTo("STRH r1, [r2, r3]!"));
            Assert.That(Disassembler.Disassemble(0xE12210B3), Is.EqualTo("STRH r1, [r2, -r3]!"));

            Assert.That(Disassembler.Disassemble(0xE08210B3), Is.EqualTo("STRH r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE00210B3), Is.EqualTo("STRH r1, [r2], -r3"));
        }

        [Test]
        public void STRHTImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE0E210B0), Is.EqualTo("STRHT r1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xE0E21FBF), Is.EqualTo("STRHT r1, [r2], #255"));
        }

        [Test]
        public void STRHTRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE0A210B3), Is.EqualTo("STRHT r1, [r2], r3"));
            Assert.That(Disassembler.Disassemble(0xE02210B3), Is.EqualTo("STRHT r1, [r2], -r3"));
        }
    }
}
