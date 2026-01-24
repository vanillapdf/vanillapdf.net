using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSyntax
{
    /// <summary>
    /// Extension methods for PdfObject providing explicit type upgrading and type checking.
    /// </summary>
    public static class PdfObjectExtensions
    {
        /// <summary>
        /// Upgrades to most derived type. Resolves indirect references.
        /// For strings, returns PdfStringObject (use UpgradeString() to get Literal/Hexadecimal).
        /// </summary>
        /// <remarks>
        /// Always creates a new wrapper object. Callers should check if upgrade is needed
        /// before calling (e.g., use Is* methods or check the object type directly).
        /// </remarks>
        /// <exception cref="PdfManagedException">Thrown if the object type is unknown.</exception>
        public static PdfObject Upgrade(this PdfObject obj)
        {
            var resolved = obj.ResolveIndirection();
            var objectType = resolved.GetObjectType();

            switch (objectType) {
                case PdfObjectType.Array:
                    return PdfArrayObject.FromObject(resolved);
                case PdfObjectType.Boolean:
                    return PdfBooleanObject.FromObject(resolved);
                case PdfObjectType.Dictionary:
                    return PdfDictionaryObject.FromObject(resolved);
                case PdfObjectType.Integer:
                    return PdfIntegerObject.FromObject(resolved);
                case PdfObjectType.Name:
                    return PdfNameObject.FromObject(resolved);
                case PdfObjectType.Null:
                    return PdfNullObject.FromObject(resolved);
                case PdfObjectType.Real:
                    return PdfRealObject.FromObject(resolved);
                case PdfObjectType.Stream:
                    return PdfStreamObject.FromObject(resolved);
                case PdfObjectType.String:
                    return PdfStringObject.FromObject(resolved);
                default:
                    throw new PdfManagedException($"Cannot upgrade object with unknown type: {objectType}");
            }
        }

        /// <summary>
        /// Resolves indirect references without upgrading to derived type.
        /// </summary>
        public static PdfObject ResolveIndirection(this PdfObject obj)
        {
            while (obj.GetObjectType() == PdfObjectType.IndirectReference) {
                using (var reference = PdfIndirectReferenceObject.FromObject(obj)) {
                    obj = reference.ReferencedObject;
                }
            }
            return obj;
        }

        #region Type checking (Is*)

        /// <summary>
        /// Checks if the object is a dictionary (resolves indirect references).
        /// </summary>
        public static bool IsDictionary(this PdfObject obj) =>
            obj.GetResolvedType() == PdfObjectType.Dictionary;

        /// <summary>
        /// Checks if the object is an array (resolves indirect references).
        /// </summary>
        public static bool IsArray(this PdfObject obj) =>
            obj.GetResolvedType() == PdfObjectType.Array;

        /// <summary>
        /// Checks if the object is an integer (resolves indirect references).
        /// </summary>
        public static bool IsInteger(this PdfObject obj) =>
            obj.GetResolvedType() == PdfObjectType.Integer;

        /// <summary>
        /// Checks if the object is a string (resolves indirect references).
        /// </summary>
        public static bool IsString(this PdfObject obj) =>
            obj.GetResolvedType() == PdfObjectType.String;

        /// <summary>
        /// Checks if the object is a boolean (resolves indirect references).
        /// </summary>
        public static bool IsBoolean(this PdfObject obj) =>
            obj.GetResolvedType() == PdfObjectType.Boolean;

        /// <summary>
        /// Checks if the object is a name (resolves indirect references).
        /// </summary>
        public static bool IsName(this PdfObject obj) =>
            obj.GetResolvedType() == PdfObjectType.Name;

        /// <summary>
        /// Checks if the object is null (resolves indirect references).
        /// </summary>
        public static bool IsNull(this PdfObject obj) =>
            obj.GetResolvedType() == PdfObjectType.Null;

        /// <summary>
        /// Checks if the object is a real number (resolves indirect references).
        /// </summary>
        public static bool IsReal(this PdfObject obj) =>
            obj.GetResolvedType() == PdfObjectType.Real;

        /// <summary>
        /// Checks if the object is a stream (resolves indirect references).
        /// </summary>
        public static bool IsStream(this PdfObject obj) =>
            obj.GetResolvedType() == PdfObjectType.Stream;

        /// <summary>
        /// Checks if the object is an indirect reference.
        /// </summary>
        public static bool IsIndirectReference(this PdfObject obj) =>
            obj.GetObjectType() == PdfObjectType.IndirectReference;

        #endregion

        #region Type conversion (As*)

        /// <summary>
        /// Returns the object as a dictionary, or null if not a dictionary.
        /// </summary>
        public static PdfDictionaryObject AsDictionary(this PdfObject obj)
        {
            var resolved = obj.ResolveIndirection();
            if (resolved.GetObjectType() != PdfObjectType.Dictionary)
                return null;
            return PdfDictionaryObject.FromObject(resolved);
        }

        /// <summary>
        /// Returns the object as an array, or null if not an array.
        /// </summary>
        public static PdfArrayObject AsArray(this PdfObject obj)
        {
            var resolved = obj.ResolveIndirection();
            if (resolved.GetObjectType() != PdfObjectType.Array)
                return null;
            return PdfArrayObject.FromObject(resolved);
        }

        /// <summary>
        /// Returns the object as an integer, or null if not an integer.
        /// </summary>
        public static PdfIntegerObject AsInteger(this PdfObject obj)
        {
            var resolved = obj.ResolveIndirection();
            if (resolved.GetObjectType() != PdfObjectType.Integer)
                return null;
            return PdfIntegerObject.FromObject(resolved);
        }

        /// <summary>
        /// Returns the object as a string, or null if not a string.
        /// </summary>
        public static PdfStringObject AsString(this PdfObject obj)
        {
            var resolved = obj.ResolveIndirection();
            if (resolved.GetObjectType() != PdfObjectType.String)
                return null;
            return PdfStringObject.FromObject(resolved);
        }

        /// <summary>
        /// Returns the object as a boolean, or null if not a boolean.
        /// </summary>
        public static PdfBooleanObject AsBoolean(this PdfObject obj)
        {
            var resolved = obj.ResolveIndirection();
            if (resolved.GetObjectType() != PdfObjectType.Boolean)
                return null;
            return PdfBooleanObject.FromObject(resolved);
        }

        /// <summary>
        /// Returns the object as a name, or null if not a name.
        /// </summary>
        public static PdfNameObject AsName(this PdfObject obj)
        {
            var resolved = obj.ResolveIndirection();
            if (resolved.GetObjectType() != PdfObjectType.Name)
                return null;
            return PdfNameObject.FromObject(resolved);
        }

        /// <summary>
        /// Returns the object as a null object, or null if not a null object.
        /// </summary>
        public static PdfNullObject AsNull(this PdfObject obj)
        {
            var resolved = obj.ResolveIndirection();
            if (resolved.GetObjectType() != PdfObjectType.Null)
                return null;
            return PdfNullObject.FromObject(resolved);
        }

        /// <summary>
        /// Returns the object as a real number, or null if not a real number.
        /// </summary>
        public static PdfRealObject AsReal(this PdfObject obj)
        {
            var resolved = obj.ResolveIndirection();
            if (resolved.GetObjectType() != PdfObjectType.Real)
                return null;
            return PdfRealObject.FromObject(resolved);
        }

        /// <summary>
        /// Returns the object as a stream, or null if not a stream.
        /// </summary>
        public static PdfStreamObject AsStream(this PdfObject obj)
        {
            var resolved = obj.ResolveIndirection();
            if (resolved.GetObjectType() != PdfObjectType.Stream)
                return null;
            return PdfStreamObject.FromObject(resolved);
        }

        /// <summary>
        /// Returns the object as an indirect reference, or null if not an indirect reference.
        /// </summary>
        public static PdfIndirectReferenceObject AsIndirectReference(this PdfObject obj)
        {
            if (obj.GetObjectType() != PdfObjectType.IndirectReference)
                return null;
            return PdfIndirectReferenceObject.FromObject(obj);
        }

        #endregion

        #region Private helpers

        /// <summary>
        /// Gets the object type after resolving any indirect references.
        /// </summary>
        private static PdfObjectType GetResolvedType(this PdfObject obj) =>
            obj.ResolveIndirection().GetObjectType();

        #endregion
    }

    /// <summary>
    /// Extension methods for PdfStringObject providing explicit type upgrading and type checking.
    /// </summary>
    public static class PdfStringObjectExtensions
    {
        /// <summary>
        /// Upgrades to PdfLiteralStringObject or PdfHexadecimalStringObject.
        /// </summary>
        /// <remarks>
        /// Always creates a new wrapper object. Callers should check if upgrade is needed
        /// before calling (e.g., use Is* methods or check the string type directly).
        /// </remarks>
        /// <exception cref="PdfManagedException">Thrown if the string type is unknown.</exception>
        public static PdfStringObject UpgradeString(this PdfStringObject obj)
        {
            var stringType = obj.GetStringType();

            switch (stringType) {
                case PdfStringType.Literal:
                    return PdfLiteralStringObject.FromString(obj);
                case PdfStringType.Hexadecimal:
                    return PdfHexadecimalStringObject.FromString(obj);
                default:
                    throw new PdfManagedException($"Cannot upgrade string with unknown type: {stringType}");
            }
        }

        /// <summary>
        /// Checks if the string is a literal string.
        /// </summary>
        public static bool IsLiteral(this PdfStringObject obj)
        {
            return obj.GetStringType() == PdfStringType.Literal;
        }

        /// <summary>
        /// Returns the string as a literal string, or null if not a literal string.
        /// </summary>
        public static PdfLiteralStringObject AsLiteral(this PdfStringObject obj)
        {
            return obj.IsLiteral() ? PdfLiteralStringObject.FromString(obj) : null;
        }

        /// <summary>
        /// Checks if the string is a hexadecimal string.
        /// </summary>
        public static bool IsHexadecimal(this PdfStringObject obj)
        {
            return obj.GetStringType() == PdfStringType.Hexadecimal;
        }

        /// <summary>
        /// Returns the string as a hexadecimal string, or null if not a hexadecimal string.
        /// </summary>
        public static PdfHexadecimalStringObject AsHexadecimal(this PdfStringObject obj)
        {
            return obj.IsHexadecimal() ? PdfHexadecimalStringObject.FromString(obj) : null;
        }
    }

    /// <summary>
    /// Extension methods for PdfXrefEntry providing explicit type upgrading and type checking.
    /// </summary>
    public static class PdfXrefEntryExtensions
    {
        /// <summary>
        /// Upgrades to the most derived entry type.
        /// </summary>
        /// <remarks>
        /// Always creates a new wrapper object. Callers should check if upgrade is needed
        /// before calling (e.g., use Is* methods or check the entry type directly).
        /// </remarks>
        /// <exception cref="PdfManagedException">Thrown if the entry type is unknown.</exception>
        public static PdfXrefEntry Upgrade(this PdfXrefEntry entry)
        {
            var entryType = entry.GetEntryType();

            switch (entryType) {
                case PdfXrefEntryType.Free:
                    return PdfXrefFreeEntry.FromEntry(entry);
                case PdfXrefEntryType.Used:
                    return PdfXrefUsedEntry.FromEntry(entry);
                case PdfXrefEntryType.Compressed:
                    return PdfXrefCompressedEntry.FromEntry(entry);
                default:
                    throw new PdfManagedException($"Cannot upgrade entry with unknown type: {entryType}");
            }
        }

        /// <summary>
        /// Checks if the entry is a free entry.
        /// </summary>
        public static bool IsFree(this PdfXrefEntry entry)
        {
            return entry.GetEntryType() == PdfXrefEntryType.Free;
        }

        /// <summary>
        /// Returns the entry as a free entry, or null if not a free entry.
        /// </summary>
        public static PdfXrefFreeEntry AsFree(this PdfXrefEntry entry)
        {
            return entry.IsFree() ? PdfXrefFreeEntry.FromEntry(entry) : null;
        }

        /// <summary>
        /// Checks if the entry is a used entry.
        /// </summary>
        public static bool IsUsed(this PdfXrefEntry entry)
        {
            return entry.GetEntryType() == PdfXrefEntryType.Used;
        }

        /// <summary>
        /// Returns the entry as a used entry, or null if not a used entry.
        /// </summary>
        public static PdfXrefUsedEntry AsUsed(this PdfXrefEntry entry)
        {
            return entry.IsUsed() ? PdfXrefUsedEntry.FromEntry(entry) : null;
        }

        /// <summary>
        /// Checks if the entry is a compressed entry.
        /// </summary>
        public static bool IsCompressed(this PdfXrefEntry entry)
        {
            return entry.GetEntryType() == PdfXrefEntryType.Compressed;
        }

        /// <summary>
        /// Returns the entry as a compressed entry, or null if not a compressed entry.
        /// </summary>
        public static PdfXrefCompressedEntry AsCompressed(this PdfXrefEntry entry)
        {
            return entry.IsCompressed() ? PdfXrefCompressedEntry.FromEntry(entry) : null;
        }
    }
}
