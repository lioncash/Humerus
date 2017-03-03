using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for bitfield instructions.
    /// </summary>
    [TestFixture]
    public sealed class BitFieldTests
    {
        [Test]
        public void BFC()
        {
            Assert.That(Disassembler.Disassemble(0xE7DD001F), Is.EqualTo("BFC r0, #0, #30"));
        }

        [Test]
        public void BFI()
        {
            Assert.That(Disassembler.Disassemble(0xE7DD0011), Is.EqualTo("BFI r0, r1, #0, #30"));
        }

        [Test]
        public void SBFX()
        {
            Assert.That(Disassembler.Disassemble(0xE7BD00D1), Is.EqualTo("SBFX r0, r1, #1, #30"));
        }

        [Test]
        public void UBFX()
        {
            Assert.That(Disassembler.Disassemble(0xE7FD00D1), Is.EqualTo("UBFX r0, r1, #1, #30"));
        }
    }
}
