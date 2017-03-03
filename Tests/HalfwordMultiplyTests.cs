using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for halfword multiply instructions.
    /// </summary>
    [TestFixture]
    public sealed class HalfwordMultiplyTests
    {
        [Test]
        public void SMLAXY()
        {
            Assert.That(Disassembler.Disassemble(0xE1014382), Is.EqualTo("SMLABB r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE10143C2), Is.EqualTo("SMLABT r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE10143A2), Is.EqualTo("SMLATB r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE10143E2), Is.EqualTo("SMLATT r1, r2, r3, r4"));
        }

        [Test]
        public void SMLAWY()
        {
            Assert.That(Disassembler.Disassemble(0xE1214382), Is.EqualTo("SMLAWB r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE12143C2), Is.EqualTo("SMLAWT r1, r2, r3, r4"));
        }

        [Test]
        public void SMULWY()
        {
            Assert.That(Disassembler.Disassemble(0xE12103A2), Is.EqualTo("SMULWB r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE12103E2), Is.EqualTo("SMULWT r1, r2, r3"));
        }

        [Test]
        public void SMLALXY()
        {
            Assert.That(Disassembler.Disassemble(0xE1421483), Is.EqualTo("SMLALBB r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE14214C3), Is.EqualTo("SMLALBT r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE14214A3), Is.EqualTo("SMLALTB r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE14214E3), Is.EqualTo("SMLALTT r1, r2, r3, r4"));
        }

        [Test]
        public void SMULXY()
        {
            Assert.That(Disassembler.Disassemble(0xE1610382), Is.EqualTo("SMULBB r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE16103C2), Is.EqualTo("SMULBT r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE16103A2), Is.EqualTo("SMULTB r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE16103E2), Is.EqualTo("SMULTT r1, r2, r3"));
        }
    }
}
