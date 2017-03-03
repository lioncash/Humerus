using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Load/Store multiple instruction tests.
    /// </summary>
    [TestFixture]
    public sealed class LoadStoreMultipleTests
    {
        [Test]
        public void LDM()
        {
            Assert.That(Disassembler.Disassemble(0xE811000C), Is.EqualTo("LDMDA r1, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE831000C), Is.EqualTo("LDMDA r1!, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE831C00C), Is.EqualTo("LDMDA r1!, {r2, r3, lr, pc}"));

            Assert.That(Disassembler.Disassemble(0xE911000C), Is.EqualTo("LDMDB r1, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE931000C), Is.EqualTo("LDMDB r1!, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE931C00C), Is.EqualTo("LDMDB r1!, {r2, r3, lr, pc}"));

            Assert.That(Disassembler.Disassemble(0xE891000C), Is.EqualTo("LDMIA r1, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE8B1000C), Is.EqualTo("LDMIA r1!, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE8B1C00C), Is.EqualTo("LDMIA r1!, {r2, r3, lr, pc}"));

            Assert.That(Disassembler.Disassemble(0xE991000C), Is.EqualTo("LDMIB r1, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE9B1000C), Is.EqualTo("LDMIB r1!, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE9B1C00C), Is.EqualTo("LDMIB r1!, {r2, r3, lr, pc}"));
        }

        [Test]
        public void STM()
        {
            Assert.That(Disassembler.Disassemble(0xE801000C), Is.EqualTo("STMDA r1, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE821000C), Is.EqualTo("STMDA r1!, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE821C00C), Is.EqualTo("STMDA r1!, {r2, r3, lr, pc}"));

            Assert.That(Disassembler.Disassemble(0xE901000C), Is.EqualTo("STMDB r1, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE921000C), Is.EqualTo("STMDB r1!, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE921C00C), Is.EqualTo("STMDB r1!, {r2, r3, lr, pc}"));

            Assert.That(Disassembler.Disassemble(0xE881000C), Is.EqualTo("STMIA r1, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE8A1000C), Is.EqualTo("STMIA r1!, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE8A1C00C), Is.EqualTo("STMIA r1!, {r2, r3, lr, pc}"));

            Assert.That(Disassembler.Disassemble(0xE981000C), Is.EqualTo("STMIB r1, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE9A1000C), Is.EqualTo("STMIB r1!, {r2, r3}"));
            Assert.That(Disassembler.Disassemble(0xE9A1C00C), Is.EqualTo("STMIB r1!, {r2, r3, lr, pc}"));
        }
    }
}
