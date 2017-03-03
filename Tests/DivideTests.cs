using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for divide instructions
    /// </summary>
    [TestFixture]
    public sealed class DivideTests
    {
        [Test]
        public void SDIV()
        {
            Assert.That(Disassembler.Disassemble(0xE711F312), Is.EqualTo("SDIV r1, r2, r3"));
        }

        [Test]
        public void UDIV()
        {
            Assert.That(Disassembler.Disassemble(0xE731F312), Is.EqualTo("UDIV r1, r2, r3"));
        }
    }
}
