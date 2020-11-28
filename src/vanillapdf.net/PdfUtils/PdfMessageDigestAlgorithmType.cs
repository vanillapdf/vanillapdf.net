namespace vanillapdf.net.PdfUtils
{
    /// <summary>
    /// Supported digest algorithms used as hash functions
    /// </summary>
    public enum PdfMessageDigestAlgorithmType {

        /// <summary>
        /// Default unset value, triggers error when used
        /// </summary>
        None = 0,

        /// <summary>
        /// "null" message digest that does nothing: i.e. the hash it returns is of zero length.
        /// </summary>
        MDNULL,

        /// <summary>
        /// The MD2 Message - Digest Algorithm is a cryptographic hash function developed by Ronald Rivest in 1989.
        /// </summary>
        MD2,

        /// <summary>
        /// The MD4 Message - Digest Algorithm is a cryptographic hash function developed by Ronald Rivest in 1990.
        /// </summary>
        MD4,

        /// <summary>
        /// MD5 is one in a series of message digest algorithms designed by Professor Ronald Rivest of MIT (Rivest, 1992).
        /// </summary>
        MD5,

        /// <summary>
        /// A 160-bit hash function which resembles the earlier MD5 algorithm.
        /// </summary>
        SHA1,

        /// <summary>
        /// SHA-2 (Secure Hash Algorithm 2) is a set of cryptographic hash functions designed by the United States National Security Agency (NSA).
        /// </summary>
        /// 
        SHA224,

        /// <summary>
        /// SHA-2 (Secure Hash Algorithm 2) is a set of cryptographic hash functions designed by the United States National Security Agency (NSA).
        /// </summary>
        SHA256,

        /// <summary>
        /// SHA-2 (Secure Hash Algorithm 2) is a set of cryptographic hash functions designed by the United States National Security Agency (NSA).
        /// </summary>
        SHA384,

        /// <summary>
        /// SHA-2 (Secure Hash Algorithm 2) is a set of cryptographic hash functions designed by the United States National Security Agency (NSA).
        /// </summary>
        SHA512,

        /// <summary>
        /// MDC - 2 (Modification Detection Code 2, sometimes called Meyer - Schilling) is a cryptographic hash function.
        /// </summary>
        MDC2,

        /// <summary>
        /// RIPEMD (RACE Integrity Primitives Evaluation Message Digest) is an improved,
		/// 160-bit version of the original RIPEMD, and the most common version in the family.
        /// </summary>
        RIPEMD160,

        /// <summary>
        /// WHIRLPOOL is a cryptographic hash function designed by Vincent Rijmen.
        /// </summary>
        WHIRLPOOL
    };
}
