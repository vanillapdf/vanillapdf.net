using System;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Registers all PDF object converters via static constructor.
    /// Initialization happens on first use when <see cref="Initialized"/> is accessed.
    /// </summary>
    internal static class PdfObjectConverters
    {
        /// <summary>
        /// Field that ensures static constructor has run when accessed.
        /// </summary>
        internal static readonly bool Initialized = true;

        static PdfObjectConverters()
        {
            // Register converters for all PDF object types
            PdfObjectConverter<PdfArrayObject>.FromObject = PdfArrayObject.FromObject;
            PdfObjectConverter<PdfBooleanObject>.FromObject = PdfBooleanObject.FromObject;
            PdfObjectConverter<PdfDictionaryObject>.FromObject = PdfDictionaryObject.FromObject;
            PdfObjectConverter<PdfIndirectReferenceObject>.FromObject = PdfIndirectReferenceObject.FromObject;
            PdfObjectConverter<PdfIntegerObject>.FromObject = PdfIntegerObject.FromObject;
            PdfObjectConverter<PdfNameObject>.FromObject = PdfNameObject.FromObject;
            PdfObjectConverter<PdfNullObject>.FromObject = PdfNullObject.FromObject;
            PdfObjectConverter<PdfRealObject>.FromObject = PdfRealObject.FromObject;
            PdfObjectConverter<PdfStreamObject>.FromObject = PdfStreamObject.FromObject;
            PdfObjectConverter<PdfStringObject>.FromObject = PdfStringObject.FromObject;
        }
    }

    /// <summary>
    /// AOT-friendly type converter for PDF objects.
    /// Each closed generic type has its own static converter delegate.
    /// </summary>
    /// <typeparam name="T">The target PDF object type.</typeparam>
    internal static class PdfObjectConverter<T> where T : PdfObject
    {
        /// <summary>
        /// Converter delegate registered by PdfObjectConverters static constructor.
        /// </summary>
        public static Func<PdfObject, T> FromObject { get; set; }

        /// <summary>
        /// Converts a PdfObject to the target type using the registered converter.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <exception cref="PdfManagedException">Thrown if no converter is registered for this type.</exception>
        public static T Convert(PdfObject obj)
        {
            // Triggers PdfObjectConverters static constructor if not already initialized
            _ = PdfObjectConverters.Initialized;

            if (FromObject == null) {
                throw new PdfManagedException($"No converter registered for type {typeof(T).Name}.");
            }

            return FromObject(obj);
        }
    }
}
