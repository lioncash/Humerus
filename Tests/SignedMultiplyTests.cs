using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Signed multiply instruction tests.
    /// </summary>
    [TestFixture]
    public sealed class SignedMultiplyTests
    {
        [Test]
        public void SMLAD()
        {
            Assert.That(Disassembler.Disassemble(0xE7014312), Is.EqualTo("SMLAD r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE7014332), Is.EqualTo("SMLADX r1, r2, r3, r4"));
        }

        [Test]
        public void SMLSD()
        {
            Assert.That(Disassembler.Disassemble(0xE7014352), Is.EqualTo("SMLSD r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE7014372), Is.EqualTo("SMLSDX r1, r2, r3, r4"));
        }

        [Test]
        public void SMUAD()
        {
            Assert.That(Disassembler.Disassemble(0xE701F312), Is.EqualTo("SMUAD r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE701F332), Is.EqualTo("SMUADX r1, r2, r3"));
        }

        [Test]
        public void SMUSD()
        {
            Assert.That(Disassembler.Disassemble(0xE701F352), Is.EqualTo("SMUSD r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE701F372), Is.EqualTo("SMUSDX r1, r2, r3"));
        }

        [Test]
        public void SMLALD()
        {
            Assert.That(Disassembler.Disassemble(0xE7421413), Is.EqualTo("SMLALD r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE7421433), Is.EqualTo("SMLALDX r1, r2, r3, r4"));
        }

        [Test]
        public void SMLSLD()
        {
            Assert.That(Disassembler.Disassemble(0xE7421453), Is.EqualTo("SMLSLD r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE7421473), Is.EqualTo("SMLSLDX r1, r2, r3, r4"));
        }

        [Test]
        public void SMMUL()
        {
            Assert.That(Disassembler.Disassemble(0xE751F312), Is.EqualTo("SMMUL r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE751F332), Is.EqualTo("SMMULR r1, r2, r3"));
        }

        [Test]
        public void SMMLA()
        {
            Assert.That(Disassembler.Disassemble(0xE7514312), Is.EqualTo("SMMLA r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE7514332), Is.EqualTo("SMMLAR r1, r2, r3, r4"));
        }

        [Test]
        public void SMMLS()
        {
            Assert.That(Disassembler.Disassemble(0xE75143D2), Is.EqualTo("SMMLS r1, r2, r3, r4"));
            Assert.That(Disassembler.Disassemble(0xE75143F2), Is.EqualTo("SMMLSR r1, r2, r3, r4"));
        }
    }
}
