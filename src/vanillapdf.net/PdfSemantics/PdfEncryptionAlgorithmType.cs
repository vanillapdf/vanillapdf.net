namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Algorithm to be used for document encryption
    /// </summary>
    public enum PdfEncryptionAlgorithmType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The application shall not decrypt data but shall direct the input stream to the security handler for decryption.
        /// </summary>
        None,

        /// <summary>
        /// A proprietary encryption algorithm known as RC4. RC4 is a symmetric stream cipher:
        /// the same algorithm shall be used for both encryption and decryption,
        /// and the algorithm does not change the length of the data. RC4 is a copyrighted,
        /// proprietary algorithm of RSA Security, Inc. Independent software vendors may be required to license RC4
        /// to develop software that encrypts or decrypts PDF documents.
        /// For further information, visit the RSA Web site at http://www.rsasecurity.com or send e-mail to products@rsasecurity.com.
        /// </summary>
        RC4,

        /// <summary>
        /// The AES (Advanced Encryption Standard) algorithm (beginning with PDF 1.6).
        /// AES is a symmetric block cipher: the same algorithm shall be used for both encryption and decryption,
        /// AES and the length of the data when encrypted is rounded up to a multiple of the block size, which is fixed to always be 16 bytes,
        /// as specified in FIPS 197, Advanced Encryption Standard (AES); see the Bibliography).
        /// </summary>
        AES
    }
}
