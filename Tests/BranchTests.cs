using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for branch instructions.
    /// </summary>
    [TestFixture]
    public sealed class BranchTests
    {
        [Test]
        public void B()
        {
            Assert.That(Disassembler.Disassemble(0xEA000001), Is.EqualTo("B +#0x0000000C"));
            Assert.That(Disassembler.Disassemble(0xEAFFFFFC), Is.EqualTo("B -#0x00000008"));
        }

        [Test]
        public void BL()
        {
            Assert.That(Disassembler.Disassemble(0xEB000001), Is.EqualTo("BL +#0x0000000C"));
            Assert.That(Disassembler.Disassemble(0xEBFFFFFC), Is.EqualTo("BL -#0x00000008"));
        }

        [Test]
        public void BLX()
        {
            Assert.That(Disassembler.Disassemble(0xFA000001), Is.EqualTo("BLX +#0x0000000C"));
            Assert.That(Disassembler.Disassemble(0xFAFFFFFC), Is.EqualTo("BLX -#0x00000008"));
            Assert.That(Disassembler.Disassemble(0xE12FFF31), Is.EqualTo("BLX r1"));
        }

        [Test]
        public void BX()
        {
            Assert.That(Disassembler.Disassemble(0xE12FFF11), Is.EqualTo("BX r1"));
        }

        [Test]
        public void BXJ()
        {
            Assert.That(Disassembler.Disassemble(0xE12FFF21), Is.EqualTo("BXJ r1"));
        }
    }
}
