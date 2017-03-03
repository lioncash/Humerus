using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for extension instructions
    /// </summary>
    [TestFixture]
    public sealed class ExtensionTests
    {
        [Test]
        public void SXTAB()
        {
            Assert.That(Disassembler.Disassemble(0xE6A21073), Is.EqualTo("SXTAB r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE6A21473), Is.EqualTo("SXTAB r1, r2, r3, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6A21873), Is.EqualTo("SXTAB r1, r2, r3, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6A21C73), Is.EqualTo("SXTAB r1, r2, r3, ROR #24"));
        }

        [Test]
        public void SXTB()
        {
            Assert.That(Disassembler.Disassemble(0xE6AF1072), Is.EqualTo("SXTB r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE6AF1472), Is.EqualTo("SXTB r1, r2, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6AF1872), Is.EqualTo("SXTB r1, r2, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6AF1C72), Is.EqualTo("SXTB r1, r2, ROR #24"));
        }

        [Test]
        public void SXTAB16()
        {
            Assert.That(Disassembler.Disassemble(0xE6821073), Is.EqualTo("SXTAB16 r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE6821473), Is.EqualTo("SXTAB16 r1, r2, r3, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6821873), Is.EqualTo("SXTAB16 r1, r2, r3, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6821C73), Is.EqualTo("SXTAB16 r1, r2, r3, ROR #24"));
        }

        [Test]
        public void SXTB16()
        {
            Assert.That(Disassembler.Disassemble(0xE68F1072), Is.EqualTo("SXTB16 r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE68F1472), Is.EqualTo("SXTB16 r1, r2, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE68F1872), Is.EqualTo("SXTB16 r1, r2, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE68F1C72), Is.EqualTo("SXTB16 r1, r2, ROR #24"));
        }

        [Test]
        public void SXTAH()
        {
            Assert.That(Disassembler.Disassemble(0xE6B21073), Is.EqualTo("SXTAH r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE6B21473), Is.EqualTo("SXTAH r1, r2, r3, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6B21873), Is.EqualTo("SXTAH r1, r2, r3, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6B21C73), Is.EqualTo("SXTAH r1, r2, r3, ROR #24"));
        }

        [Test]
        public void SXTH()
        {
            Assert.That(Disassembler.Disassemble(0xE6BF1072), Is.EqualTo("SXTH r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE6BF1472), Is.EqualTo("SXTH r1, r2, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6BF1872), Is.EqualTo("SXTH r1, r2, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6BF1C72), Is.EqualTo("SXTH r1, r2, ROR #24"));
        }

        [Test]
        public void UXTAB()
        {
            Assert.That(Disassembler.Disassemble(0xE6E21073), Is.EqualTo("UXTAB r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE6E21473), Is.EqualTo("UXTAB r1, r2, r3, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6E21873), Is.EqualTo("UXTAB r1, r2, r3, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6E21C73), Is.EqualTo("UXTAB r1, r2, r3, ROR #24"));
        }

        [Test]
        public void UXTB()
        {
            Assert.That(Disassembler.Disassemble(0xE6EF1072), Is.EqualTo("UXTB r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE6EF1472), Is.EqualTo("UXTB r1, r2, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6EF1872), Is.EqualTo("UXTB r1, r2, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6EF1C72), Is.EqualTo("UXTB r1, r2, ROR #24"));
        }

        [Test]
        public void UXTAB16()
        {
            Assert.That(Disassembler.Disassemble(0xE6C21073), Is.EqualTo("UXTAB16 r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE6C21473), Is.EqualTo("UXTAB16 r1, r2, r3, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6C21873), Is.EqualTo("UXTAB16 r1, r2, r3, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6C21C73), Is.EqualTo("UXTAB16 r1, r2, r3, ROR #24"));
        }

        [Test]
        public void UXTB16()
        {
            Assert.That(Disassembler.Disassemble(0xE6CF1072), Is.EqualTo("UXTB16 r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE6CF1472), Is.EqualTo("UXTB16 r1, r2, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6CF1872), Is.EqualTo("UXTB16 r1, r2, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6CF1C72), Is.EqualTo("UXTB16 r1, r2, ROR #24"));
        }

        [Test]
        public void UXTAH()
        {
            Assert.That(Disassembler.Disassemble(0xE6F21073), Is.EqualTo("UXTAH r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE6F21473), Is.EqualTo("UXTAH r1, r2, r3, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6F21873), Is.EqualTo("UXTAH r1, r2, r3, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6F21C73), Is.EqualTo("UXTAH r1, r2, r3, ROR #24"));
        }

        [Test]
        public void UXTH()
        {
            Assert.That(Disassembler.Disassemble(0xE6FF1072), Is.EqualTo("UXTH r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE6FF1472), Is.EqualTo("UXTH r1, r2, ROR #8"));
            Assert.That(Disassembler.Disassemble(0xE6FF1872), Is.EqualTo("UXTH r1, r2, ROR #16"));
            Assert.That(Disassembler.Disassemble(0xE6FF1C72), Is.EqualTo("UXTH r1, r2, ROR #24"));
        }
    }
}
