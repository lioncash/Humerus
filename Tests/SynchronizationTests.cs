using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for synchronization primitive instructions
    /// </summary>
    [TestFixture]
    public sealed class SynchronizationTests
    {
        [Test]
        public void LDREX()
        {
            Assert.That(Disassembler.Disassemble(0xE1921F9F), Is.EqualTo("LDREX r1, [r2]"));
        }

        [Test]
        public void LDREXB()
        {
            Assert.That(Disassembler.Disassemble(0xE1D21F9F), Is.EqualTo("LDREXB r1, [r2]"));
        }

        [Test]
        public void LDREXD()
        {
            Assert.That(Disassembler.Disassemble(0xE1B31F9F), Is.EqualTo("LDREXD r1, r2, [r3]"));
        }

        [Test]
        public void LDREXH()
        {
            Assert.That(Disassembler.Disassemble(0xE1F21F9F), Is.EqualTo("LDREXH r1, [r2]"));
        }

        [Test]
        public void STREX()
        {
            Assert.That(Disassembler.Disassemble(0xE1831F92), Is.EqualTo("STREX r1, r2, [r3]"));
        }

        [Test]
        public void STREXB()
        {
            Assert.That(Disassembler.Disassemble(0xE1C31F92), Is.EqualTo("STREXB r1, r2, [r3]"));
        }

        [Test]
        public void STREXD()
        {
            Assert.That(Disassembler.Disassemble(0xE1A41F92), Is.EqualTo("STREXD r1, r2, r3, [r4]"));
        }

        [Test]
        public void STREXH()
        {
            Assert.That(Disassembler.Disassemble(0xE1E31F92), Is.EqualTo("STREXH r1, r2, [r3]"));
        }

        [Test]
        public void SWP()
        {
            Assert.That(Disassembler.Disassemble(0xE1031092), Is.EqualTo("SWP r1, r2, [r3]"));
            Assert.That(Disassembler.Disassemble(0xE1431092), Is.EqualTo("SWPB r1, r2, [r3]"));
        }
    }
}
