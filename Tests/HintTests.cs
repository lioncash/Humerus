using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for hint instructions.
    /// </summary>
    [TestFixture]
    public sealed class HintTests
    {
        [Test]
        public void DBG()
        {
            Assert.That(Disassembler.Disassemble(0xE320F0FF), Is.EqualTo("DBG #15"));
        }

        [Test]
        public void NOP()
        {
            Assert.That(Disassembler.Disassemble(0xE320F000), Is.EqualTo("NOP"));
        }

        [Test]
        public void PLD()
        {
            Assert.That(Disassembler.Disassemble(0xF5D1F00F), Is.EqualTo("PLD [r1, #+0x00F]"));
            Assert.That(Disassembler.Disassemble(0xF7D1F002), Is.EqualTo("PLD [r1, +r2]"));
            Assert.That(Disassembler.Disassemble(0xF7D1F1E2), Is.EqualTo("PLD [r1, +r2, ROR #3]"));
        }

        [Test]
        public void PLI()
        {
            Assert.That(Disassembler.Disassemble(0xF4D1F004), Is.EqualTo("PLI [r1, #+0x004]"));
        }

        [Test]
        public void SEV()
        {
            Assert.That(Disassembler.Disassemble(0xE320F004), Is.EqualTo("SEV"));
        }

        [Test]
        public void WFE()
        {
            Assert.That(Disassembler.Disassemble(0xE320F002), Is.EqualTo("WFE"));
        }

        [Test]
        public void WFI()
        {
            Assert.That(Disassembler.Disassemble(0xE320F003), Is.EqualTo("WFI"));
        }

        [Test]
        public void YIELD()
        {
            Assert.That(Disassembler.Disassemble(0xE320F001), Is.EqualTo("YIELD"));
        }
    }
}
