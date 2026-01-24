using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax.Extensions
{
    /// <summary>
    /// Extension methods for PdfObject type checking and conversion.
    /// </summary>
    public static class PdfObjectExtensions
    {
        /// <summary>
        /// Checks if the object is of the specified type (resolves indirect references).
        /// </summary>
        /// <typeparam name="T">The PDF object type to check for.</typeparam>
        /// <returns><c>true</c> if the object is of type <typeparamref name="T"/>.</returns>
        public static bool Is<T>(this PdfObject obj) where T : PdfObject
        {
            using (var upgraded = obj.Upgrade()) {
                return upgraded is T;
            }
        }

        /// <summary>
        /// Returns the object as the specified type, or null if type doesn't match.
        /// Resolves indirect references before conversion.
        /// Caller must dispose the returned object.
        /// </summary>
        /// <typeparam name="T">The PDF object type to convert to.</typeparam>
        /// <returns>The object as <typeparamref name="T"/>, or null if type doesn't match.</returns>
        public static T As<T>(this PdfObject obj) where T : PdfObject
        {
            var upgraded = obj.Upgrade();
            if (upgraded is T result) {
                return result;
            }
            upgraded.Dispose();
            return null;
        }

        /// <summary>
        /// Upgrades to most derived type. Resolves indirect references.
        /// Returns a new object that the caller must dispose.
        /// </summary>
        /// <exception cref="PdfManagedException">Thrown if the object type is unknown.</exception>
        internal static PdfObject Upgrade(this PdfObject obj)
        {
            // Resolve indirect references inline
            while (obj.GetObjectType() == PdfObjectType.IndirectReference) {
                using (var reference = PdfIndirectReferenceObject.FromObject(obj)) {
                    obj = reference.ReferencedObject;
                }
            }

            // Create typed wrapper based on object type
            var objectType = obj.GetObjectType();
            switch (objectType) {
                case PdfObjectType.Array:
                    return PdfArrayObject.FromObject(obj);
                case PdfObjectType.Boolean:
                    return PdfBooleanObject.FromObject(obj);
                case PdfObjectType.Dictionary:
                    return PdfDictionaryObject.FromObject(obj);
                case PdfObjectType.Integer:
                    return PdfIntegerObject.FromObject(obj);
                case PdfObjectType.Name:
                    return PdfNameObject.FromObject(obj);
                case PdfObjectType.Null:
                    return PdfNullObject.FromObject(obj);
                case PdfObjectType.Real:
                    return PdfRealObject.FromObject(obj);
                case PdfObjectType.Stream:
                    return PdfStreamObject.FromObject(obj);
                case PdfObjectType.String:
                    return PdfStringObject.FromObject(obj);
                default:
                    throw new PdfManagedException($"Cannot upgrade object with unknown type: {objectType}");
            }
        }
    }
}
