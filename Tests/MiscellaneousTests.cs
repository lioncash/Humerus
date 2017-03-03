using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Tests for miscellaneous class instructions
    /// </summary>
    [TestFixture]
    public sealed class MiscellaneousTests
    {
        [Test]
        public void BKPT()
        {
            Assert.That(Disassembler.Disassemble(0xE12FFF7F), Is.EqualTo("BKPT #0xFFFF"));
        }

        [Test]
        public void CLREX()
        {
            Assert.That(Disassembler.Disassemble(0xF57FF01F), Is.EqualTo("CLREX"));
        }

        [Test]
        public void CLZ()
        {
            Assert.That(Disassembler.Disassemble(0xE16F1F12), Is.EqualTo("CLZ r1, r2"));
        }

        [Test]
        public void CPS()
        {
            Assert.That(Disassembler.Disassemble(0xF10E01CF), Is.EqualTo("CPSID AIF, #15"));
            Assert.That(Disassembler.Disassemble(0xF10A01CF), Is.EqualTo("CPSIE AIF, #15"));
            Assert.That(Disassembler.Disassemble(0xF10C01C0), Is.EqualTo("CPSID AIF"));
            Assert.That(Disassembler.Disassemble(0xF10801C0), Is.EqualTo("CPSIE AIF"));
            Assert.That(Disassembler.Disassemble(0xF102000F), Is.EqualTo("CPS #15"));
        }

        [Test]
        public void ERET()
        {
            Assert.That(Disassembler.Disassemble(0xE160006E), Is.EqualTo("ERET"));
        }

        [Test]
        public void HVC()
        {
            Assert.That(Disassembler.Disassemble(0xE140007F), Is.EqualTo("HVC #0x000F"));
        }

        [Test]
        public void MRS()
        {
            Assert.That(Disassembler.Disassemble(0xE10F1000), Is.EqualTo("MRS r1, CPSR"));
            Assert.That(Disassembler.Disassemble(0xE14F1000), Is.EqualTo("MRS r1, SPSR"));
        }

        [Test]
        public void MRSBanked()
        {
            Assert.That(Disassembler.Disassemble(0xE1051300), Is.EqualTo("MRS r1, sp_abt"));
            Assert.That(Disassembler.Disassemble(0xE1041300), Is.EqualTo("MRS r1, lr_abt"));
            Assert.That(Disassembler.Disassemble(0xE1441300), Is.EqualTo("MRS r1, SPSR_abt"));

            Assert.That(Disassembler.Disassemble(0xE1081200), Is.EqualTo("MRS r1, r8_fiq"));
            Assert.That(Disassembler.Disassemble(0xE1091200), Is.EqualTo("MRS r1, r9_fiq"));
            Assert.That(Disassembler.Disassemble(0xE10A1200), Is.EqualTo("MRS r1, r10_fiq"));
            Assert.That(Disassembler.Disassemble(0xE10B1200), Is.EqualTo("MRS r1, r11_fiq"));
            Assert.That(Disassembler.Disassemble(0xE10C1200), Is.EqualTo("MRS r1, r12_fiq"));
            Assert.That(Disassembler.Disassemble(0xE10D1200), Is.EqualTo("MRS r1, sp_fiq"));
            Assert.That(Disassembler.Disassemble(0xE10E1200), Is.EqualTo("MRS r1, lr_fiq"));
            Assert.That(Disassembler.Disassemble(0xE14E1200), Is.EqualTo("MRS r1, SPSR_fiq"));

            Assert.That(Disassembler.Disassemble(0xE10F1300), Is.EqualTo("MRS r1, sp_hyp"));
            Assert.That(Disassembler.Disassemble(0xE10E1300), Is.EqualTo("MRS r1, ELR_hyp"));
            Assert.That(Disassembler.Disassemble(0xE14E1300), Is.EqualTo("MRS r1, SPSR_hyp"));

            Assert.That(Disassembler.Disassemble(0xE1011300), Is.EqualTo("MRS r1, sp_irq"));
            Assert.That(Disassembler.Disassemble(0xE1001300), Is.EqualTo("MRS r1, lr_irq"));
            Assert.That(Disassembler.Disassemble(0xE1401300), Is.EqualTo("MRS r1, SPSR_irq"));

            Assert.That(Disassembler.Disassemble(0xE10D1300), Is.EqualTo("MRS r1, sp_mon"));
            Assert.That(Disassembler.Disassemble(0xE10C1300), Is.EqualTo("MRS r1, lr_mon"));
            Assert.That(Disassembler.Disassemble(0xE14C1300), Is.EqualTo("MRS r1, SPSR_mon"));

            Assert.That(Disassembler.Disassemble(0xE1031300), Is.EqualTo("MRS r1, sp_svc"));
            Assert.That(Disassembler.Disassemble(0xE1021300), Is.EqualTo("MRS r1, lr_svc"));
            Assert.That(Disassembler.Disassemble(0xE1421300), Is.EqualTo("MRS r1, SPSR_svc"));

            Assert.That(Disassembler.Disassemble(0xE1071300), Is.EqualTo("MRS r1, sp_und"));
            Assert.That(Disassembler.Disassemble(0xE1061300), Is.EqualTo("MRS r1, lr_und"));
            Assert.That(Disassembler.Disassemble(0xE1461300), Is.EqualTo("MRS r1, SPSR_und"));

            Assert.That(Disassembler.Disassemble(0xE1001200), Is.EqualTo("MRS r1, r8_usr"));
            Assert.That(Disassembler.Disassemble(0xE1011200), Is.EqualTo("MRS r1, r9_usr"));
            Assert.That(Disassembler.Disassemble(0xE1021200), Is.EqualTo("MRS r1, r10_usr"));
            Assert.That(Disassembler.Disassemble(0xE1031200), Is.EqualTo("MRS r1, r11_usr"));
            Assert.That(Disassembler.Disassemble(0xE1041200), Is.EqualTo("MRS r1, r12_usr"));
            Assert.That(Disassembler.Disassemble(0xE1051200), Is.EqualTo("MRS r1, sp_usr"));
            Assert.That(Disassembler.Disassemble(0xE1061200), Is.EqualTo("MRS r1, lr_usr"));
        }

        [Test]
        public void MSRImmediate()
        {
            Assert.That(Disassembler.Disassemble(0xE32CF01F), Is.EqualTo("MSR APSR_nzcvqg, #0x1F"));
            Assert.That(Disassembler.Disassemble(0xE32FF0FF), Is.EqualTo("MSR CPSR_fsxc, #0xFF"));
            Assert.That(Disassembler.Disassemble(0xE32FF102), Is.EqualTo("MSR CPSR_fsxc, #0x80000000"));
            Assert.That(Disassembler.Disassemble(0xE36FF01F), Is.EqualTo("MSR SPSR_fsxc, #0x1F"));
        }

        [Test]
        public void MSRRegister()
        {
            Assert.That(Disassembler.Disassemble(0xE129F001), Is.EqualTo("MSR CPSR_fc, r1"));
            Assert.That(Disassembler.Disassemble(0xE12FF001), Is.EqualTo("MSR CPSR_fsxc, r1"));
            Assert.That(Disassembler.Disassemble(0xE123F001), Is.EqualTo("MSR CPSR_xc, r1"));
            Assert.That(Disassembler.Disassemble(0xE128F001), Is.EqualTo("MSR APSR_nzcvq, r1"));
            Assert.That(Disassembler.Disassemble(0xE124F001), Is.EqualTo("MSR APSR_g, r1"));
            Assert.That(Disassembler.Disassemble(0xE12CF001), Is.EqualTo("MSR APSR_nzcvqg, r1"));
        }

        [Test]
        public void MSRBanked()
        {
            Assert.That(Disassembler.Disassemble(0xE125F301), Is.EqualTo("MSR sp_abt, r1"));
            Assert.That(Disassembler.Disassemble(0xE124F301), Is.EqualTo("MSR lr_abt, r1"));
            Assert.That(Disassembler.Disassemble(0xE164F301), Is.EqualTo("MSR SPSR_abt, r1"));

            Assert.That(Disassembler.Disassemble(0xE128F201), Is.EqualTo("MSR r8_fiq, r1"));
            Assert.That(Disassembler.Disassemble(0xE129F201), Is.EqualTo("MSR r9_fiq, r1"));
            Assert.That(Disassembler.Disassemble(0xE12AF201), Is.EqualTo("MSR r10_fiq, r1"));
            Assert.That(Disassembler.Disassemble(0xE12BF201), Is.EqualTo("MSR r11_fiq, r1"));
            Assert.That(Disassembler.Disassemble(0xE12CF201), Is.EqualTo("MSR r12_fiq, r1"));
            Assert.That(Disassembler.Disassemble(0xE12DF201), Is.EqualTo("MSR sp_fiq, r1"));
            Assert.That(Disassembler.Disassemble(0xE12EF201), Is.EqualTo("MSR lr_fiq, r1"));
            Assert.That(Disassembler.Disassemble(0xE16EF201), Is.EqualTo("MSR SPSR_fiq, r1"));

            Assert.That(Disassembler.Disassemble(0xE12FF301), Is.EqualTo("MSR sp_hyp, r1"));
            Assert.That(Disassembler.Disassemble(0xE12EF301), Is.EqualTo("MSR ELR_hyp, r1"));
            Assert.That(Disassembler.Disassemble(0xE16EF301), Is.EqualTo("MSR SPSR_hyp, r1"));

            Assert.That(Disassembler.Disassemble(0xE121F301), Is.EqualTo("MSR sp_irq, r1"));
            Assert.That(Disassembler.Disassemble(0xE120F301), Is.EqualTo("MSR lr_irq, r1"));
            Assert.That(Disassembler.Disassemble(0xE160F301), Is.EqualTo("MSR SPSR_irq, r1"));

            Assert.That(Disassembler.Disassemble(0xE12DF301), Is.EqualTo("MSR sp_mon, r1"));
            Assert.That(Disassembler.Disassemble(0xE12CF301), Is.EqualTo("MSR lr_mon, r1"));
            Assert.That(Disassembler.Disassemble(0xE16CF301), Is.EqualTo("MSR SPSR_mon, r1"));

            Assert.That(Disassembler.Disassemble(0xE123F301), Is.EqualTo("MSR sp_svc, r1"));
            Assert.That(Disassembler.Disassemble(0xE122F301), Is.EqualTo("MSR lr_svc, r1"));
            Assert.That(Disassembler.Disassemble(0xE162F301), Is.EqualTo("MSR SPSR_svc, r1"));

            Assert.That(Disassembler.Disassemble(0xE127F301), Is.EqualTo("MSR sp_und, r1"));
            Assert.That(Disassembler.Disassemble(0xE126F301), Is.EqualTo("MSR lr_und, r1"));
            Assert.That(Disassembler.Disassemble(0xE166F301), Is.EqualTo("MSR SPSR_und, r1"));

            Assert.That(Disassembler.Disassemble(0xE120F201), Is.EqualTo("MSR r8_usr, r1"));
            Assert.That(Disassembler.Disassemble(0xE121F201), Is.EqualTo("MSR r9_usr, r1"));
            Assert.That(Disassembler.Disassemble(0xE122F201), Is.EqualTo("MSR r10_usr, r1"));
            Assert.That(Disassembler.Disassemble(0xE123F201), Is.EqualTo("MSR r11_usr, r1"));
            Assert.That(Disassembler.Disassemble(0xE124F201), Is.EqualTo("MSR r12_usr, r1"));
            Assert.That(Disassembler.Disassemble(0xE125F201), Is.EqualTo("MSR sp_usr, r1"));
            Assert.That(Disassembler.Disassemble(0xE126F201), Is.EqualTo("MSR lr_usr, r1"));
        }

        [Test]
        public void QADD()
        {
            Assert.That(Disassembler.Disassemble(0xE1031052), Is.EqualTo("QADD r1, r2, r3"));
        }

        [Test]
        public void QSUB()
        {
            Assert.That(Disassembler.Disassemble(0xE1231052), Is.EqualTo("QSUB r1, r2, r3"));
        }

        [Test]
        public void QDADD()
        {
            Assert.That(Disassembler.Disassemble(0xE1431052), Is.EqualTo("QDADD r1, r2, r3"));
        }

        [Test]
        public void QDSUB()
        {
            Assert.That(Disassembler.Disassemble(0xE1631052), Is.EqualTo("QDSUB r1, r2, r3"));
        }

        [Test]
        public void RFE()
        {
            Assert.That(Disassembler.Disassemble(0xF8910A00), Is.EqualTo("RFE r1"));
            Assert.That(Disassembler.Disassemble(0xF8B10A00), Is.EqualTo("RFE r1!"));
            Assert.That(Disassembler.Disassemble(0xF9910A00), Is.EqualTo("RFEIB r1"));
            Assert.That(Disassembler.Disassemble(0xF9B10A00), Is.EqualTo("RFEIB r1!"));
            Assert.That(Disassembler.Disassemble(0xF8110A00), Is.EqualTo("RFEDA r1"));
            Assert.That(Disassembler.Disassemble(0xF8310A00), Is.EqualTo("RFEDA r1!"));
            Assert.That(Disassembler.Disassemble(0xF9110A00), Is.EqualTo("RFEDB r1"));
            Assert.That(Disassembler.Disassemble(0xF9310A00), Is.EqualTo("RFEDB r1!"));
        }

        [Test]
        public void SETEND()
        {
            Assert.That(Disassembler.Disassemble(0xF1010000), Is.EqualTo("SETEND LE"));
            Assert.That(Disassembler.Disassemble(0xF1010200), Is.EqualTo("SETEND BE"));
        }

        [Test]
        public void SMC()
        {
            Assert.That(Disassembler.Disassemble(0xE160007F), Is.EqualTo("SMC #0xF"));
        }

        [Test]
        public void SVC()
        {
            Assert.That(Disassembler.Disassemble(0xEFFFFFFF), Is.EqualTo("SVC #0xFFFFFF"));
        }

        [Test]
        public void UDF()
        {
            Assert.That(Disassembler.Disassemble(0xE7F000FF), Is.EqualTo("UDF #15"));
            Assert.That(Disassembler.Disassemble(0xE7FFFFFF), Is.EqualTo("UDF #65535"));
        }
    }
}
