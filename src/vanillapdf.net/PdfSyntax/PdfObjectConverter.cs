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
            PdfObjectConverter<PdfArrayObject>.TryFromObject = PdfArrayObject.TryFromObject;
            PdfObjectConverter<PdfBooleanObject>.FromObject = PdfBooleanObject.FromObject;
            PdfObjectConverter<PdfBooleanObject>.TryFromObject = PdfBooleanObject.TryFromObject;
            PdfObjectConverter<PdfDictionaryObject>.FromObject = PdfDictionaryObject.FromObject;
            PdfObjectConverter<PdfDictionaryObject>.TryFromObject = PdfDictionaryObject.TryFromObject;
            PdfObjectConverter<PdfIndirectReferenceObject>.FromObject = PdfIndirectReferenceObject.FromObject;
            PdfObjectConverter<PdfIndirectReferenceObject>.TryFromObject = PdfIndirectReferenceObject.TryFromObject;
            PdfObjectConverter<PdfIntegerObject>.FromObject = PdfIntegerObject.FromObject;
            PdfObjectConverter<PdfIntegerObject>.TryFromObject = PdfIntegerObject.TryFromObject;
            PdfObjectConverter<PdfNameObject>.FromObject = PdfNameObject.FromObject;
            PdfObjectConverter<PdfNameObject>.TryFromObject = PdfNameObject.TryFromObject;
            PdfObjectConverter<PdfNullObject>.FromObject = PdfNullObject.FromObject;
            PdfObjectConverter<PdfNullObject>.TryFromObject = PdfNullObject.TryFromObject;
            PdfObjectConverter<PdfRealObject>.FromObject = PdfRealObject.FromObject;
            PdfObjectConverter<PdfRealObject>.TryFromObject = PdfRealObject.TryFromObject;
            PdfObjectConverter<PdfStreamObject>.FromObject = PdfStreamObject.FromObject;
            PdfObjectConverter<PdfStreamObject>.TryFromObject = PdfStreamObject.TryFromObject;
            PdfObjectConverter<PdfStringObject>.FromObject = PdfStringObject.FromObject;
            PdfObjectConverter<PdfStringObject>.TryFromObject = PdfStringObject.TryFromObject;
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
        /// Does not validate type - use for performance when type is known.
        /// </summary>
        public static Func<PdfObject, T> FromObject { get; set; }

        /// <summary>
        /// Safe converter delegate that validates type and returns null on mismatch.
        /// </summary>
        public static Func<PdfObject, T> TryFromObject { get; set; }

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

        /// <summary>
        /// Tries to convert a PdfObject to the target type, returning null if type doesn't match.
        /// </summary>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted object, or null if type doesn't match.</returns>
        /// <exception cref="PdfManagedException">Thrown if no converter is registered for this type.</exception>
        public static T TryConvert(PdfObject obj)
        {
            // Triggers PdfObjectConverters static constructor if not already initialized
            _ = PdfObjectConverters.Initialized;

            if (TryFromObject == null) {
                throw new PdfManagedException($"No converter registered for type {typeof(T).Name}.");
            }

            return TryFromObject(obj);
        }
    }
}
