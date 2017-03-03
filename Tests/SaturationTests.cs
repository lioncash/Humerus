using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for saturation instructions.
    /// </summary>
    [TestFixture]
    public sealed class SaturationTests
    {
        [Test]
        public void SSAT()
        {
            Assert.That(Disassembler.Disassemble(0xE6BD1012), Is.EqualTo("SSAT r1, #30, r2"));
        }

        [Test]
        public void SSAT16()
        {
            Assert.That(Disassembler.Disassemble(0xE6AF1F32), Is.EqualTo("SSAT16 r1, #16, r2"));
        }

        [Test]
        public void USAT()
        {
            Assert.That(Disassembler.Disassemble(0xE6FE1012), Is.EqualTo("USAT r1, #30, r2"));
        }

        [Test]
        public void USAT16()
        {
            Assert.That(Disassembler.Disassemble(0xE6EF1F32), Is.EqualTo("USAT16 r1, #15, r2"));
        }
    }
}
