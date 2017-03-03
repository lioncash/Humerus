using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for reversal instructions
    /// </summary>
    [TestFixture]
    public sealed class ReversalTests
    {
        [Test]
        public void RBIT()
        {
            Assert.That(Disassembler.Disassemble(0xE6FF1F32), Is.EqualTo("RBIT r1, r2"));
        }

        [Test]
        public void REV()
        {
            Assert.That(Disassembler.Disassemble(0xE6BF1F32), Is.EqualTo("REV r1, r2"));
        }

        [Test]
        public void REV16()
        {
            Assert.That(Disassembler.Disassemble(0xE6BF1FB2), Is.EqualTo("REV16 r1, r2"));
        }

        [Test]
        public void REVSH()
        {
            Assert.That(Disassembler.Disassemble(0xE6FF1FB2), Is.EqualTo("REVSH r1, r2"));
        }
    }
}
