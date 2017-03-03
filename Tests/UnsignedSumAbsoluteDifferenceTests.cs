using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for unsigned sum of absolute difference instructions.
    /// </summary>
    [TestFixture]
    public sealed class UnsignedSumAbsoluteDifferenceTests
    {
        [Test]
        public void USAD8()
        {
            Assert.That(Disassembler.Disassemble(0xE781F312), Is.EqualTo("USAD8 r1, r2, r3"));
        }

        [Test]
        public void USADA8()
        {
            Assert.That(Disassembler.Disassemble(0xE7814312), Is.EqualTo("USADA8 r1, r2, r3, r4"));
        }
    }
}
