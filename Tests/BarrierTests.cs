using System;
using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for barrier instructions.
    /// </summary>
    [TestFixture]
    public sealed class BarrierTests
    {
        [Test]
        public void DMB()
        {
            var dataset = new[] {
                new Tuple<uint, string>(0xF57FF05F, "DMB SY"),
                new Tuple<uint, string>(0xF57FF05E, "DMB ST"),
                new Tuple<uint, string>(0xF57FF05B, "DMB ISH"),
                new Tuple<uint, string>(0xF57FF05A, "DMB ISHST"),
                new Tuple<uint, string>(0xF57FF057, "DMB NSH"),
                new Tuple<uint, string>(0xF57FF056, "DMB NSHST"),
                new Tuple<uint, string>(0xF57FF053, "DMB OSH"),
                new Tuple<uint, string>(0xF57FF052, "DMB OSHST")
            };

            foreach (var entry in dataset)
                Assert.That(Disassembler.Disassemble(entry.Item1), Is.EqualTo(entry.Item2));
        }

        [Test]
        public void DSB()
        {
            var dataset = new[] {
                new Tuple<uint, string>(0xF57FF04F, "DSB SY"),
                new Tuple<uint, string>(0xF57FF04E, "DSB ST"),
                new Tuple<uint, string>(0xF57FF04B, "DSB ISH"),
                new Tuple<uint, string>(0xF57FF04A, "DSB ISHST"),
                new Tuple<uint, string>(0xF57FF047, "DSB NSH"),
                new Tuple<uint, string>(0xF57FF046, "DSB NSHST"),
                new Tuple<uint, string>(0xF57FF043, "DSB OSH"),
                new Tuple<uint, string>(0xF57FF042, "DSB OSHST")
            };

            foreach (var entry in dataset)
                Assert.That(Disassembler.Disassemble(entry.Item1), Is.EqualTo(entry.Item2));
        }

        [Test]
        public void ISB()
        {
            Assert.That(Disassembler.Disassemble(0xF57FF06F), Is.EqualTo("ISB SY"));
        }
    }
}
