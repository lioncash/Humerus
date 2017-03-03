using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for regular multiply instructions.
    /// </summary>
    [TestFixture]
    public sealed class MultiplyTests
    {
        [Test]
        public void MLA()
        {
            Assert.That(Disassembler.Disassemble(0xE0214392), Is.EqualTo("MLA r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE0314392), Is.EqualTo("MLAS r1, r2, r3, r4"));
        }

        [Test]
        public void MLS()
        {
            Assert.That(Disassembler.Disassemble(0xE0614392), Is.EqualTo("MLS r1, r2, r3, r4"));
        }

        [Test]
        public void MUL()
        {
            Assert.That(Disassembler.Disassemble(0xE0010392), Is.EqualTo("MUL r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0110392), Is.EqualTo("MULS r1, r2, r3"));
        }

        [Test]
        public void SMLAL()
        {
            Assert.That(Disassembler.Disassemble(0xE0E21493), Is.EqualTo("SMLAL r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE0F21493), Is.EqualTo("SMLALS r1, r2, r3, r4"));
        }

        [Test]
        public void SMULL()
        {
            Assert.That(Disassembler.Disassemble(0xE0C21493), Is.EqualTo("SMULL r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE0D21493), Is.EqualTo("SMULLS r1, r2, r3, r4"));
        }

        [Test]
        public void UMAAL()
        {
            Assert.That(Disassembler.Disassemble(0xE0421493), Is.EqualTo("UMAAL r1, r2, r3, r4"));
        }

        [Test]
        public void UMLAL()
        {
            Assert.That(Disassembler.Disassemble(0xE0A21493), Is.EqualTo("UMLAL r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE0B21493), Is.EqualTo("UMLALS r1, r2, r3, r4"));
        }

        [Test]
        public void UMULL()
        {
            Assert.That(Disassembler.Disassemble(0xE0821493), Is.EqualTo("UMULL r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE0921493), Is.EqualTo("UMULLS r1, r2, r3, r4"));
        }
    }
}
