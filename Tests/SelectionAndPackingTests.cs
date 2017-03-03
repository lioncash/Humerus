using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for selection and packing instructions.
    /// </summary>
    [TestFixture]
    public sealed class SelectionAndPackingTests
    {
        [Test]
        public void SEL()
        {
            Assert.That(Disassembler.Disassemble(0xE6821FB3), Is.EqualTo("SEL r1, r2, r3"));
        }

        [Test]
        public void PKH()
        {
            Assert.That(Disassembler.Disassemble(0xE6821213), Is.EqualTo("PKHBT r1, r2, r3, LSL #4"));
            Assert.That(Disassembler.Disassemble(0xE6821253), Is.EqualTo("PKHTB r1, r2, r3, ASR #4"));
        }
    }
}
