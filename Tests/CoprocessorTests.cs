using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Coprocessor instruction tests.
    /// </summary>
    [TestFixture]
    public sealed class CoprocessorTests
    {
        [Test]
        public void CDP()
        {
            Assert.That(Disassembler.Disassemble(0xEE132004), Is.EqualTo("CDP p0, 1, cr2, cr3, cr4"));
            Assert.That(Disassembler.Disassemble(0xEE1320A4), Is.EqualTo("CDP p0, 1, cr2, cr3, cr4, {5}"));
            Assert.That(Disassembler.Disassemble(0xFE1320A4), Is.EqualTo("CDP2 p0, 1, cr2, cr3, cr4, {5}"));
        }

        [Test]
        public void LDC()
        {
            Assert.That(Disassembler.Disassemble(0xED921000), Is.EqualTo("LDC p0, cr1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xEDD21000), Is.EqualTo("LDCL p0, cr1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xFD921000), Is.EqualTo("LDC2 p0, cr1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xFDD21000), Is.EqualTo("LDC2L p0, cr1, [r2]"));

            Assert.That(Disassembler.Disassemble(0xEDB21001), Is.EqualTo("LDC p0, cr1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xEDF21001), Is.EqualTo("LDCL p0, cr1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xFDB21001), Is.EqualTo("LDC2 p0, cr1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xFDF21001), Is.EqualTo("LDC2L p0, cr1, [r2, #4]!"));

            Assert.That(Disassembler.Disassemble(0xECB21001), Is.EqualTo("LDC p0, cr1, [r2], #4"));
            Assert.That(Disassembler.Disassemble(0xECF21001), Is.EqualTo("LDCL p0, cr1, [r2], #4"));
            Assert.That(Disassembler.Disassemble(0xFCB21001), Is.EqualTo("LDC2 p0, cr1, [r2], #4"));
            Assert.That(Disassembler.Disassemble(0xFCF21001), Is.EqualTo("LDC2L p0, cr1, [r2], #4"));

            Assert.That(Disassembler.Disassemble(0xEC921001), Is.EqualTo("LDC p0, cr1, [r2], {4}"));
            Assert.That(Disassembler.Disassemble(0xECD21001), Is.EqualTo("LDCL p0, cr1, [r2], {4}"));
            Assert.That(Disassembler.Disassemble(0xFC921001), Is.EqualTo("LDC2 p0, cr1, [r2], {4}"));
            Assert.That(Disassembler.Disassemble(0xFCD21001), Is.EqualTo("LDC2L p0, cr1, [r2], {4}"));
        }

        [Test]
        public void MCR()
        {
            Assert.That(Disassembler.Disassemble(0xEE232014), Is.EqualTo("MCR p0, 1, r2, cr3, cr4"));
            Assert.That(Disassembler.Disassemble(0xEE2320B4), Is.EqualTo("MCR p0, 1, r2, cr3, cr4, {5}"));
            Assert.That(Disassembler.Disassemble(0xFE2320B4), Is.EqualTo("MCR2 p0, 1, r2, cr3, cr4, {5}"));
        }

        [Test]
        public void MCRR()
        {
            Assert.That(Disassembler.Disassemble(0xEC432014), Is.EqualTo("MCRR p0, 1, r2, r3, cr4"));
            Assert.That(Disassembler.Disassemble(0xFC432014), Is.EqualTo("MCRR2 p0, 1, r2, r3, cr4"));
        }

        [Test]
        public void MRC()
        {
            Assert.That(Disassembler.Disassemble(0xEE332014), Is.EqualTo("MRC p0, 1, r2, cr3, cr4"));
            Assert.That(Disassembler.Disassemble(0xEE3320B4), Is.EqualTo("MRC p0, 1, r2, cr3, cr4, {5}"));
            Assert.That(Disassembler.Disassemble(0xFE3320B4), Is.EqualTo("MRC2 p0, 1, r2, cr3, cr4, {5}"));
        }

        [Test]
        public void MRRC()
        {
            Assert.That(Disassembler.Disassemble(0xEC532014), Is.EqualTo("MRRC p0, 1, r2, r3, cr4"));
            Assert.That(Disassembler.Disassemble(0xFC532014), Is.EqualTo("MRRC2 p0, 1, r2, r3, cr4"));
        }

        [Test]
        public void STC()
        {
            Assert.That(Disassembler.Disassemble(0xED821000), Is.EqualTo("STC p0, cr1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xEDC21000), Is.EqualTo("STCL p0, cr1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xFD821000), Is.EqualTo("STC2 p0, cr1, [r2]"));
            Assert.That(Disassembler.Disassemble(0xFDC21000), Is.EqualTo("STC2L p0, cr1, [r2]"));

            Assert.That(Disassembler.Disassemble(0xEDA21001), Is.EqualTo("STC p0, cr1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xEDE21001), Is.EqualTo("STCL p0, cr1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xFDA21001), Is.EqualTo("STC2 p0, cr1, [r2, #4]!"));
            Assert.That(Disassembler.Disassemble(0xFDE21001), Is.EqualTo("STC2L p0, cr1, [r2, #4]!"));

            Assert.That(Disassembler.Disassemble(0xECA21001), Is.EqualTo("STC p0, cr1, [r2], #4"));
            Assert.That(Disassembler.Disassemble(0xECE21001), Is.EqualTo("STCL p0, cr1, [r2], #4"));
            Assert.That(Disassembler.Disassemble(0xFCA21001), Is.EqualTo("STC2 p0, cr1, [r2], #4"));
            Assert.That(Disassembler.Disassemble(0xFCE21001), Is.EqualTo("STC2L p0, cr1, [r2], #4"));

            Assert.That(Disassembler.Disassemble(0xEC821001), Is.EqualTo("STC p0, cr1, [r2], {4}"));
            Assert.That(Disassembler.Disassemble(0xECC21001), Is.EqualTo("STCL p0, cr1, [r2], {4}"));
            Assert.That(Disassembler.Disassemble(0xFC821001), Is.EqualTo("STC2 p0, cr1, [r2], {4}"));
            Assert.That(Disassembler.Disassemble(0xFCC21001), Is.EqualTo("STC2L p0, cr1, [r2], {4}"));
        }
    }
}
