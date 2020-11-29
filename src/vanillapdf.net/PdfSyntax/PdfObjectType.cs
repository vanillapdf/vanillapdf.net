namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Derived types of \ref PdfObject
    /// </summary>
    public enum PdfObjectType
    {
        /// <summary>
        /// Undefined unitialized default value, triggers error when used
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// The null object has a type and value that are unequal to those of any other object
        /// </summary>
        Null,

        /// <summary>
        /// An array object is a one-dimensional collection of objects arranged sequentially
        /// </summary>
        Array,

        /// <summary>
        /// Boolean objects represent the logical values of true and false
        /// </summary>
        Boolean,

        /// <summary>
        /// A dictionary object is an associative table containing pairs of objects
        /// </summary>
        Dictionary,

        /// <summary>
        /// Integer objects represent mathematical integers
        /// </summary>
        Integer,

        /// <summary>
        /// A name object is an atomic symbol uniquely defined by a sequence of characters
        /// </summary>
        Name,

        /// <summary>
        /// Real objects represent mathematical real numbers
        /// </summary>
        Real,

        /// <summary>
        /// Stream object represents compressed data inside document
        /// </summary>
        Stream,

        /// <summary>
        /// Reprsents human readable text
        /// </summary>
        String,

        /// <summary>
        /// Represents reference to another object
        /// </summary>
        IndirectReference
    };
}
