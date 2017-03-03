using Humerus;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    /// Data-processing instruction tests.
    /// </summary>
    [TestFixture]
    public sealed class DataProcessingTests
    {
        [Test, Description("ADC Immediate Tests")]
        public void ADCImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE2A21019), Is.EqualTo("ADC r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE2AEF019), Is.EqualTo("ADC pc, lr, #25"));
            Assert.That(Disassembler.Disassemble(0xE2B21019), Is.EqualTo("ADCS r1, r2, #25"));
        }

        [Test, Description("ADC Register Tests")]
        public void ADCRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0A21003), Is.EqualTo("ADC r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0B21003), Is.EqualTo("ADCS r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0A21783), Is.EqualTo("ADC r1, r2, r3, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE0A217A3), Is.EqualTo("ADC r1, r2, r3, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE0A217C3), Is.EqualTo("ADC r1, r2, r3, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE0A217E3), Is.EqualTo("ADC r1, r2, r3, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE0A21063), Is.EqualTo("ADC r1, r2, r3, RRX"));
        }

        [Test, Description("ADC Register-shifted Register Tests")]
        public void ADCRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0B21413), Is.EqualTo("ADCS r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0A21413), Is.EqualTo("ADC r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0A21433), Is.EqualTo("ADC r1, r2, r3, LSR r4"));
            Assert.That(Disassembler.Disassemble(0xE0A21453), Is.EqualTo("ADC r1, r2, r3, ASR r4"));
            Assert.That(Disassembler.Disassemble(0xE0A21473), Is.EqualTo("ADC r1, r2, r3, ROR r4"));
        }

        [Test, Description("ADD Immediate Tests")]
        public void ADDImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE2821019), Is.EqualTo("ADD r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE28EF019), Is.EqualTo("ADD pc, lr, #25"));
            Assert.That(Disassembler.Disassemble(0xE2921019), Is.EqualTo("ADDS r1, r2, #25"));
        }

        [Test, Description("ADD Register Tests")]
        public void ADDRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0821003), Is.EqualTo("ADD r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0921003), Is.EqualTo("ADDS r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0821783), Is.EqualTo("ADD r1, r2, r3, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE08217A3), Is.EqualTo("ADD r1, r2, r3, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE08217C3), Is.EqualTo("ADD r1, r2, r3, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE08217E3), Is.EqualTo("ADD r1, r2, r3, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE0821063), Is.EqualTo("ADD r1, r2, r3, RRX"));
        }

        [Test, Description("ADD Register-shifted Register Tests")]
        public void ADDRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0921413), Is.EqualTo("ADDS r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0821413), Is.EqualTo("ADD r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0821433), Is.EqualTo("ADD r1, r2, r3, LSR r4"));
            Assert.That(Disassembler.Disassemble(0xE0821453), Is.EqualTo("ADD r1, r2, r3, ASR r4"));
            Assert.That(Disassembler.Disassemble(0xE0821473), Is.EqualTo("ADD r1, r2, r3, ROR r4"));
        }

        [Test, Description("AND Immediate Tests")]
        public void ANDImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE2021019), Is.EqualTo("AND r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE20EF019), Is.EqualTo("AND pc, lr, #25"));
            Assert.That(Disassembler.Disassemble(0xE2121019), Is.EqualTo("ANDS r1, r2, #25"));
        }

        [Test, Description("AND Register Tests")]
        public void ANDRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0021003), Is.EqualTo("AND r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0121003), Is.EqualTo("ANDS r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0021783), Is.EqualTo("AND r1, r2, r3, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE00217A3), Is.EqualTo("AND r1, r2, r3, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE00217C3), Is.EqualTo("AND r1, r2, r3, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE00217E3), Is.EqualTo("AND r1, r2, r3, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE0021063), Is.EqualTo("AND r1, r2, r3, RRX"));
        }

        [Test, Description("AND Register-shifted Register Tests")]
        public void ANDRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0121413), Is.EqualTo("ANDS r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0021413), Is.EqualTo("AND r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0021433), Is.EqualTo("AND r1, r2, r3, LSR r4"));
            Assert.That(Disassembler.Disassemble(0xE0021453), Is.EqualTo("AND r1, r2, r3, ASR r4"));
            Assert.That(Disassembler.Disassemble(0xE0021473), Is.EqualTo("AND r1, r2, r3, ROR r4"));
        }

        [Test, Description("ASR Immediate Tests")]
        public void ASRImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1A01CC2), Is.EqualTo("ASR r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE1A01042), Is.EqualTo("ASR r1, r2, #32"));
            Assert.That(Disassembler.Disassemble(0xE1B01CC2), Is.EqualTo("ASRS r1, r2, #25"));
        }

        [Test, Description("ASR Register Tests")]
        public void ASRRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1A01352), Is.EqualTo("ASR r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE1B01352), Is.EqualTo("ASRS r1, r2, r3"));
        }

        [Test, Description("BIC Immediate Tests")]
        public void BICImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE3C21019), Is.EqualTo("BIC r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE3CEF019), Is.EqualTo("BIC pc, lr, #25"));
            Assert.That(Disassembler.Disassemble(0xE3D21019), Is.EqualTo("BICS r1, r2, #25"));
        }

        [Test, Description("BIC Register Tests")]
        public void BICRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1C21003), Is.EqualTo("BIC r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE1D21003), Is.EqualTo("BICS r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE1C21783), Is.EqualTo("BIC r1, r2, r3, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE1C217A3), Is.EqualTo("BIC r1, r2, r3, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE1C217C3), Is.EqualTo("BIC r1, r2, r3, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE1C217E3), Is.EqualTo("BIC r1, r2, r3, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE1C21063), Is.EqualTo("BIC r1, r2, r3, RRX"));
        }

        [Test, Description("BIC Register-shifted Register Tests")]
        public void BICRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1D21413), Is.EqualTo("BICS r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE1C21413), Is.EqualTo("BIC r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE1C21433), Is.EqualTo("BIC r1, r2, r3, LSR r4"));
            Assert.That(Disassembler.Disassemble(0xE1C21453), Is.EqualTo("BIC r1, r2, r3, ASR r4"));
            Assert.That(Disassembler.Disassemble(0xE1C21473), Is.EqualTo("BIC r1, r2, r3, ROR r4"));
        }

        [Test, Description("CMN Immediate Tests")]
        public void CMNImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE3710019), Is.EqualTo("CMN r1, #25"));
            Assert.That(Disassembler.Disassemble(0xE37F0019), Is.EqualTo("CMN pc, #25"));
        }

        [Test, Description("CMN Register Tests")]
        public void CMNRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1710002), Is.EqualTo("CMN r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE1710782), Is.EqualTo("CMN r1, r2, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE17107A2), Is.EqualTo("CMN r1, r2, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE17107C2), Is.EqualTo("CMN r1, r2, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE17107E2), Is.EqualTo("CMN r1, r2, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE1710062), Is.EqualTo("CMN r1, r2, RRX"));
        }

        [Test, Description("CMN Register-shifted Register Tests")]
        public void CMNRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1710312), Is.EqualTo("CMN r1, r2, LSL r3"));
            Assert.That(Disassembler.Disassemble(0xE1710332), Is.EqualTo("CMN r1, r2, LSR r3"));
            Assert.That(Disassembler.Disassemble(0xE1710352), Is.EqualTo("CMN r1, r2, ASR r3"));
            Assert.That(Disassembler.Disassemble(0xE1710372), Is.EqualTo("CMN r1, r2, ROR r3"));
        }

        [Test, Description("CMP Immediate Tests")]
        public void CMPImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE3510019), Is.EqualTo("CMP r1, #25"));
            Assert.That(Disassembler.Disassemble(0xE35F0019), Is.EqualTo("CMP pc, #25"));
        }

        [Test, Description("CMP Register Tests")]
        public void CMPRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1510002), Is.EqualTo("CMP r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE1510782), Is.EqualTo("CMP r1, r2, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE15107A2), Is.EqualTo("CMP r1, r2, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE15107C2), Is.EqualTo("CMP r1, r2, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE15107E2), Is.EqualTo("CMP r1, r2, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE1510062), Is.EqualTo("CMP r1, r2, RRX"));
        }

        [Test, Description("CMP Register-shifted Register Tests")]
        public void CMPRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1510312), Is.EqualTo("CMP r1, r2, LSL r3"));
            Assert.That(Disassembler.Disassemble(0xE1510332), Is.EqualTo("CMP r1, r2, LSR r3"));
            Assert.That(Disassembler.Disassemble(0xE1510352), Is.EqualTo("CMP r1, r2, ASR r3"));
            Assert.That(Disassembler.Disassemble(0xE1510372), Is.EqualTo("CMP r1, r2, ROR r3"));
        }

        [Test, Description("EOR Immediate Tests")]
        public void EORImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE2221019), Is.EqualTo("EOR r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE22EF019), Is.EqualTo("EOR pc, lr, #25"));
        }

        [Test, Description("EOR Register Tests")]
        public void EORRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0221003), Is.EqualTo("EOR r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0321003), Is.EqualTo("EORS r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0221783), Is.EqualTo("EOR r1, r2, r3, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE02217A3), Is.EqualTo("EOR r1, r2, r3, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE02217C3), Is.EqualTo("EOR r1, r2, r3, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE02217E3), Is.EqualTo("EOR r1, r2, r3, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE0221063), Is.EqualTo("EOR r1, r2, r3, RRX"));
        }

        [Test, Description("EOR Register-shifted Register Tests")]
        public void EORRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0321413), Is.EqualTo("EORS r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0221413), Is.EqualTo("EOR r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0221433), Is.EqualTo("EOR r1, r2, r3, LSR r4"));
            Assert.That(Disassembler.Disassemble(0xE0221453), Is.EqualTo("EOR r1, r2, r3, ASR r4"));
            Assert.That(Disassembler.Disassemble(0xE0221473), Is.EqualTo("EOR r1, r2, r3, ROR r4"));
        }

        [Test, Description("LSL Immediate Tests")]
        public void LSLImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1A01C82), Is.EqualTo("LSL r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE1A01F82), Is.EqualTo("LSL r1, r2, #31"));
            Assert.That(Disassembler.Disassemble(0xE1B01C82), Is.EqualTo("LSLS r1, r2, #25"));
        }

        [Test, Description("LSL Register Tests")]
        public void LSLRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1A01312), Is.EqualTo("LSL r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE1B01312), Is.EqualTo("LSLS r1, r2, r3"));
        }

        [Test, Description("LSR Immediate Tests")]
        public void LSRImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1A01CA2), Is.EqualTo("LSR r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE1A01FA2), Is.EqualTo("LSR r1, r2, #31"));
            Assert.That(Disassembler.Disassemble(0xE1B01CA2), Is.EqualTo("LSRS r1, r2, #25"));
        }

        [Test, Description("LSR Register Tests")]
        public void LSRRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1A01332), Is.EqualTo("LSR r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE1B01332), Is.EqualTo("LSRS r1, r2, r3"));
        }

        [Test, Description("MOV Immediate Tests")]
        public void MOVImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE3A01019), Is.EqualTo("MOV r1, #25"));
            Assert.That(Disassembler.Disassemble(0xE3A0F019), Is.EqualTo("MOV pc, #25"));
            Assert.That(Disassembler.Disassemble(0xE34F1FFF), Is.EqualTo("MOVT r1, #65535"));
            Assert.That(Disassembler.Disassemble(0xE30F1FFF), Is.EqualTo("MOVW r1, #65535"));
        }

        [Test, Description("MOV Register Tests")]
        public void MOVRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1A01002), Is.EqualTo("MOV r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE1B01002), Is.EqualTo("MOVS r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE1A0F00E), Is.EqualTo("MOV pc, lr"));
        }

        [Test, Description("MVN Immediate Tests")]
        public void MVNImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE3E01019), Is.EqualTo("MVN r1, #25"));
            Assert.That(Disassembler.Disassemble(0xE3F01019), Is.EqualTo("MVNS r1, #25"));
        }

        [Test, Description("MVN Register Tests")]
        public void MVNRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1E01002), Is.EqualTo("MVN r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE1F01002), Is.EqualTo("MVNS r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE1E01782), Is.EqualTo("MVN r1, r2, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE1E017A2), Is.EqualTo("MVN r1, r2, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE1E017C2), Is.EqualTo("MVN r1, r2, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE1E017E2), Is.EqualTo("MVN r1, r2, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE1E01062), Is.EqualTo("MVN r1, r2, RRX"));
        }

        [Test, Description("MVN Register-shifted Register Tests")]
        public void MVNRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1F01312), Is.EqualTo("MVNS r1, r2, LSL r3"));
            Assert.That(Disassembler.Disassemble(0xE1E01312), Is.EqualTo("MVN r1, r2, LSL r3"));
            Assert.That(Disassembler.Disassemble(0xE1E01332), Is.EqualTo("MVN r1, r2, LSR r3"));
            Assert.That(Disassembler.Disassemble(0xE1E01352), Is.EqualTo("MVN r1, r2, ASR r3"));
            Assert.That(Disassembler.Disassemble(0xE1E01372), Is.EqualTo("MVN r1, r2, ROR r3"));
        }

        [Test, Description("ORR Immediate Tests")]
        public void ORRImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE3821019), Is.EqualTo("ORR r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE3921019), Is.EqualTo("ORRS r1, r2, #25"));
        }

        [Test, Description("ORR Register Tests")]
        public void ORRRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1821003), Is.EqualTo("ORR r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE1921003), Is.EqualTo("ORRS r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE1821783), Is.EqualTo("ORR r1, r2, r3, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE18217A3), Is.EqualTo("ORR r1, r2, r3, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE18217C3), Is.EqualTo("ORR r1, r2, r3, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE18217E3), Is.EqualTo("ORR r1, r2, r3, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE1821063), Is.EqualTo("ORR r1, r2, r3, RRX"));
        }

        [Test, Description("ORR Register-shifted Register Tests")]
        public void ORRRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1921413), Is.EqualTo("ORRS r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE1821413), Is.EqualTo("ORR r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE1821433), Is.EqualTo("ORR r1, r2, r3, LSR r4"));
            Assert.That(Disassembler.Disassemble(0xE1821453), Is.EqualTo("ORR r1, r2, r3, ASR r4"));
            Assert.That(Disassembler.Disassemble(0xE1821473), Is.EqualTo("ORR r1, r2, r3, ROR r4"));
        }

        [Test, Description("ROR Immediate Tests")]
        public void RORImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1A01CE2), Is.EqualTo("ROR r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE1A01FE2), Is.EqualTo("ROR r1, r2, #31"));
            Assert.That(Disassembler.Disassemble(0xE1B01CE2), Is.EqualTo("RORS r1, r2, #25"));
        }

        [Test, Description("ROR Register Tests")]
        public void RORRegisterTests()
        {
            Assert.That(Disassembler.Disassemble(0xE1A01372), Is.EqualTo("ROR r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE1B01372), Is.EqualTo("RORS r1, r2, r3"));
        }

        [Test, Description("RRX Tests")]
        public void RRXTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1A01062), Is.EqualTo("RRX r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE1B01062), Is.EqualTo("RRXS r1, r2"));
        }

        [Test, Description("RSB Immediate Tests")]
        public void RSBImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE2621019), Is.EqualTo("RSB r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE2721019), Is.EqualTo("RSBS r1, r2, #25"));
        }

        [Test, Description("RSB Register Tests")]
        public void RSBRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0621003), Is.EqualTo("RSB r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0721003), Is.EqualTo("RSBS r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0621783), Is.EqualTo("RSB r1, r2, r3, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE06217A3), Is.EqualTo("RSB r1, r2, r3, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE06217C3), Is.EqualTo("RSB r1, r2, r3, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE06217E3), Is.EqualTo("RSB r1, r2, r3, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE0621063), Is.EqualTo("RSB r1, r2, r3, RRX"));
        }

        [Test, Description("RSB Register-shifted Register Tests")]
        public void RSBRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0721413), Is.EqualTo("RSBS r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0621413), Is.EqualTo("RSB r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0621433), Is.EqualTo("RSB r1, r2, r3, LSR r4"));
            Assert.That(Disassembler.Disassemble(0xE0621453), Is.EqualTo("RSB r1, r2, r3, ASR r4"));
            Assert.That(Disassembler.Disassemble(0xE0621473), Is.EqualTo("RSB r1, r2, r3, ROR r4"));
        }

        [Test, Description("RSC Immediate Tests")]
        public void RSCImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE2E21019), Is.EqualTo("RSC r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE2F21019), Is.EqualTo("RSCS r1, r2, #25"));
        }

        [Test, Description("RSC Register Tests")]
        public void RSCRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0E21003), Is.EqualTo("RSC r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0F21003), Is.EqualTo("RSCS r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0E21783), Is.EqualTo("RSC r1, r2, r3, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE0E217A3), Is.EqualTo("RSC r1, r2, r3, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE0E217C3), Is.EqualTo("RSC r1, r2, r3, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE0E217E3), Is.EqualTo("RSC r1, r2, r3, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE0E21063), Is.EqualTo("RSC r1, r2, r3, RRX"));
        }

        [Test, Description("RSC Register-shifted Register Test")]
        public void RSCRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0F21413), Is.EqualTo("RSCS r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0E21413), Is.EqualTo("RSC r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0E21433), Is.EqualTo("RSC r1, r2, r3, LSR r4"));
            Assert.That(Disassembler.Disassemble(0xE0E21453), Is.EqualTo("RSC r1, r2, r3, ASR r4"));
            Assert.That(Disassembler.Disassemble(0xE0E21473), Is.EqualTo("RSC r1, r2, r3, ROR r4"));
        }

        [Test, Description("SBC Immediate Tests")]
        public void SBCImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE2C21019), Is.EqualTo("SBC r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE2D21019), Is.EqualTo("SBCS r1, r2, #25"));
        }

        [Test, Description("SBC Register Tests")]
        public void SBCRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0C21003), Is.EqualTo("SBC r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0D21003), Is.EqualTo("SBCS r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0C21783), Is.EqualTo("SBC r1, r2, r3, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE0C217A3), Is.EqualTo("SBC r1, r2, r3, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE0C217C3), Is.EqualTo("SBC r1, r2, r3, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE0C217E3), Is.EqualTo("SBC r1, r2, r3, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE0C21063), Is.EqualTo("SBC r1, r2, r3, RRX"));
        }

        [Test, Description("SBC Register-shifted Register Tests")]
        public void SBCRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0D21413), Is.EqualTo("SBCS r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0C21413), Is.EqualTo("SBC r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0C21433), Is.EqualTo("SBC r1, r2, r3, LSR r4"));
            Assert.That(Disassembler.Disassemble(0xE0C21453), Is.EqualTo("SBC r1, r2, r3, ASR r4"));
            Assert.That(Disassembler.Disassemble(0xE0C21473), Is.EqualTo("SBC r1, r2, r3, ROR r4"));
        }

        [Test, Description("SUB Immediate Tests")]
        public void SUBImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE2421019), Is.EqualTo("SUB r1, r2, #25"));
            Assert.That(Disassembler.Disassemble(0xE2521019), Is.EqualTo("SUBS r1, r2, #25"));
        }

        [Test, Description("SUB Register Tests")]
        public void SUBRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0421003), Is.EqualTo("SUB r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0521003), Is.EqualTo("SUBS r1, r2, r3"));
            Assert.That(Disassembler.Disassemble(0xE0421783), Is.EqualTo("SUB r1, r2, r3, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE04217A3), Is.EqualTo("SUB r1, r2, r3, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE04217C3), Is.EqualTo("SUB r1, r2, r3, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE04217E3), Is.EqualTo("SUB r1, r2, r3, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE0421063), Is.EqualTo("SUB r1, r2, r3, RRX"));
        }

        [Test, Description("SUB Register-shifted Register Tests")]
        public void SUBRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE0521413), Is.EqualTo("SUBS r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0421413), Is.EqualTo("SUB r1, r2, r3, LSL r4"));
            Assert.That(Disassembler.Disassemble(0xE0421433), Is.EqualTo("SUB r1, r2, r3, LSR r4"));
            Assert.That(Disassembler.Disassemble(0xE0421453), Is.EqualTo("SUB r1, r2, r3, ASR r4"));
            Assert.That(Disassembler.Disassemble(0xE0421473), Is.EqualTo("SUB r1, r2, r3, ROR r4"));
        }

        [Test, Description("TEQ Immediate Tests")]
        public void TEQImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE3310019), Is.EqualTo("TEQ r1, #25"));
            Assert.That(Disassembler.Disassemble(0xE33F0019), Is.EqualTo("TEQ pc, #25"));
        }

        [Test, Description("TEQ Register Tests")]
        public void TEQRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1310002), Is.EqualTo("TEQ r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE13F000E), Is.EqualTo("TEQ pc, lr"));
            Assert.That(Disassembler.Disassemble(0xE1310782), Is.EqualTo("TEQ r1, r2, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE13107A2), Is.EqualTo("TEQ r1, r2, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE13107C2), Is.EqualTo("TEQ r1, r2, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE13107E2), Is.EqualTo("TEQ r1, r2, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE1310062), Is.EqualTo("TEQ r1, r2, RRX"));
        }

        [Test, Description("TEQ Register-shifted Register Tests")]
        public void TEQRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1310312), Is.EqualTo("TEQ r1, r2, LSL r3"));
            Assert.That(Disassembler.Disassemble(0xE1310332), Is.EqualTo("TEQ r1, r2, LSR r3"));
            Assert.That(Disassembler.Disassemble(0xE1310352), Is.EqualTo("TEQ r1, r2, ASR r3"));
            Assert.That(Disassembler.Disassemble(0xE1310372), Is.EqualTo("TEQ r1, r2, ROR r3"));
        }

        [Test, Description("TST Immediate Tests")]
        public void TSTImmediateTest()
        {
            Assert.That(Disassembler.Disassemble(0xE3110019), Is.EqualTo("TST r1, #25"));
            Assert.That(Disassembler.Disassemble(0xE31F0019), Is.EqualTo("TST pc, #25"));
        }

        [Test, Description("TST Register Tests")]
        public void TSTRegisterTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1110002), Is.EqualTo("TST r1, r2"));
            Assert.That(Disassembler.Disassemble(0xE11F000E), Is.EqualTo("TST pc, lr"));
            Assert.That(Disassembler.Disassemble(0xE1110782), Is.EqualTo("TST r1, r2, LSL #15"));
            Assert.That(Disassembler.Disassemble(0xE11107A2), Is.EqualTo("TST r1, r2, LSR #15"));
            Assert.That(Disassembler.Disassemble(0xE11107C2), Is.EqualTo("TST r1, r2, ASR #15"));
            Assert.That(Disassembler.Disassemble(0xE11107E2), Is.EqualTo("TST r1, r2, ROR #15"));
            Assert.That(Disassembler.Disassemble(0xE1110062), Is.EqualTo("TST r1, r2, RRX"));
        }

        [Test, Description("TST Register-shifted Register Tests")]
        public void TSTRsrTest()
        {
            Assert.That(Disassembler.Disassemble(0xE1110312), Is.EqualTo("TST r1, r2, LSL r3"));
            Assert.That(Disassembler.Disassemble(0xE1110332), Is.EqualTo("TST r1, r2, LSR r3"));
            Assert.That(Disassembler.Disassemble(0xE1110352), Is.EqualTo("TST r1, r2, ASR r3"));
            Assert.That(Disassembler.Disassemble(0xE1110372), Is.EqualTo("TST r1, r2, ROR r3"));
        }
    }
}
