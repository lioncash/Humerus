using System;
using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for parallel add/subtract instructions
    /// </summary>
    [TestFixture]
    public sealed class ParallelAddSubTests
    {
        [Test]
        public void ModuloSignedTest()
        {
            var dataset = new[]
            {
                new Tuple<uint, string>(0xE6121F13, "SADD16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6121F33, "SASX r1, r2, r3"),
                new Tuple<uint, string>(0xE6121F53, "SSAX r1, r2, r3"),
                new Tuple<uint, string>(0xE6121F73, "SSUB16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6121F93, "SADD8 r1, r2, r3"),
                new Tuple<uint, string>(0xE6121FF3, "SSUB8 r1, r2, r3")
            };

            foreach (var entry in dataset)
            {
                Assert.That(Disassembler.Disassemble(entry.Item1), Is.EqualTo(entry.Item2));
            }
        }

        [Test]
        public void ModuloUnsignedTest()
        {
            var dataset = new[]
            {
                new Tuple<uint, string>(0xE6521F13, "UADD16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6521F33, "UASX r1, r2, r3"),
                new Tuple<uint, string>(0xE6521F53, "USAX r1, r2, r3"),
                new Tuple<uint, string>(0xE6521F73, "USUB16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6521F93, "UADD8 r1, r2, r3"),
                new Tuple<uint, string>(0xE6521FF3, "USUB8 r1, r2, r3")
            };

            foreach (var entry in dataset)
            {
                Assert.That(Disassembler.Disassemble(entry.Item1), Is.EqualTo(entry.Item2));
            }
        }

        [Test]
        public void SaturatingSignedTest()
        {
            var dataset = new[]
            {
                new Tuple<uint, string>(0xE6221F13, "QADD16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6221F33, "QASX r1, r2, r3"),
                new Tuple<uint, string>(0xE6221F53, "QSAX r1, r2, r3"),
                new Tuple<uint, string>(0xE6221F73, "QSUB16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6221F93, "QADD8 r1, r2, r3"),
                new Tuple<uint, string>(0xE6221FF3, "QSUB8 r1, r2, r3")
            };

            foreach (var entry in dataset)
            {
                Assert.That(Disassembler.Disassemble(entry.Item1), Is.EqualTo(entry.Item2));
            }
        }

        [Test]
        public void SaturatingUnsignedTest()
        {
            var dataset = new[]
            {
                new Tuple<uint, string>(0xE6621F13, "UQADD16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6621F33, "UQASX r1, r2, r3"),
                new Tuple<uint, string>(0xE6621F53, "UQSAX r1, r2, r3"),
                new Tuple<uint, string>(0xE6621F73, "UQSUB16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6621F93, "UQADD8 r1, r2, r3"),
                new Tuple<uint, string>(0xE6621FF3, "UQSUB8 r1, r2, r3")
            };

            foreach (var entry in dataset)
            {
                Assert.That(Disassembler.Disassemble(entry.Item1), Is.EqualTo(entry.Item2));
            }
        }

        [Test]
        public void HalvingSignedTest()
        {
            var dataset = new[]
            {
                new Tuple<uint, string>(0xE6321F13, "SHADD16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6321F33, "SHASX r1, r2, r3"),
                new Tuple<uint, string>(0xE6321F53, "SHSAX r1, r2, r3"),
                new Tuple<uint, string>(0xE6321F73, "SHSUB16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6321F93, "SHADD8 r1, r2, r3"),
                new Tuple<uint, string>(0xE6321FF3, "SHSUB8 r1, r2, r3")
            };

            foreach (var entry in dataset)
            {
                Assert.That(Disassembler.Disassemble(entry.Item1), Is.EqualTo(entry.Item2));
            }
        }

        [Test]
        public void HalvingUnsignedTest()
        {
            var dataset = new[]
            {
                new Tuple<uint, string>(0xE6721F13, "UHADD16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6721F33, "UHASX r1, r2, r3"),
                new Tuple<uint, string>(0xE6721F53, "UHSAX r1, r2, r3"),
                new Tuple<uint, string>(0xE6721F73, "UHSUB16 r1, r2, r3"),
                new Tuple<uint, string>(0xE6721F93, "UHADD8 r1, r2, r3"),
                new Tuple<uint, string>(0xE6721FF3, "UHSUB8 r1, r2, r3")
            };

            foreach (var entry in dataset)
            {
                Assert.That(Disassembler.Disassemble(entry.Item1), Is.EqualTo(entry.Item2));
            }
        }
    }
}
