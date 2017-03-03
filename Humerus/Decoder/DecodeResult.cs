using System;

namespace Humerus
{
    /// <summary>
    /// Represents the status of an opcode decode attempt.
    /// </summary>
    internal sealed class DecodeResult
    {
        /// <summary>
        /// Indicates whether or not the decode attempt succeeded.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Functor that may be invoked to perform actual disassembly of the opcode.
        /// </summary>
        public Func<uint, string> DisassemblyFunction { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal DecodeResult(bool isValid, Func<uint, string> disassemblyFunction)
        {
            IsValid = isValid;
            DisassemblyFunction = disassemblyFunction;
        }

        public override string ToString()
        {
            return $"[DecodeResult: IsValid={IsValid}, DisassemblyFunction={DisassemblyFunction}]";
        }
    }
}
