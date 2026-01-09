using System;

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
        /// <param name="obj">The PDF object to upgrade.</param>
        /// <returns>The upgraded PDF object.</returns>
        public static PdfObject Upgrade(this PdfObject obj)
        {
            // Loop instead of recursion to resolve indirection
            while (obj.GetObjectType() == PdfObjectType.IndirectReference) {
                using (var reference = PdfIndirectReferenceObject.FromObject(obj)) {
                    obj = reference.ReferencedObject;
                }
            }

            switch (obj.GetObjectType()) {
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
                    return obj;
            }
        }

        /// <summary>
        /// Resolves indirect references without upgrading to derived type.
        /// </summary>
        /// <param name="obj">The PDF object to resolve.</param>
        /// <returns>The resolved PDF object (may be the same object if not an indirect reference).</returns>
        public static PdfObject ResolveIndirection(this PdfObject obj)
        {
            while (obj.GetObjectType() == PdfObjectType.IndirectReference) {
                using (var reference = PdfIndirectReferenceObject.FromObject(obj)) {
                    obj = reference.ReferencedObject;
                }
            }
            return obj;
        }

        // Type-specific Is/As methods (no generics, no reflection)

        /// <summary>
        /// Checks if the object is a dictionary (resolves indirect references).
        /// </summary>
        public static bool IsDictionary(this PdfObject obj)
        {
            return obj.ResolveIndirection().GetObjectType() == PdfObjectType.Dictionary;
        }

        /// <summary>
        /// Returns the object as a dictionary, or null if not a dictionary.
        /// </summary>
        public static PdfDictionaryObject AsDictionary(this PdfObject obj)
        {
            return obj.IsDictionary() ? PdfDictionaryObject.FromObject(obj.ResolveIndirection()) : null;
        }

        /// <summary>
        /// Checks if the object is an array (resolves indirect references).
        /// </summary>
        public static bool IsArray(this PdfObject obj)
        {
            return obj.ResolveIndirection().GetObjectType() == PdfObjectType.Array;
        }

        /// <summary>
        /// Returns the object as an array, or null if not an array.
        /// </summary>
        public static PdfArrayObject AsArray(this PdfObject obj)
        {
            return obj.IsArray() ? PdfArrayObject.FromObject(obj.ResolveIndirection()) : null;
        }

        /// <summary>
        /// Checks if the object is an integer (resolves indirect references).
        /// </summary>
        public static bool IsInteger(this PdfObject obj)
        {
            return obj.ResolveIndirection().GetObjectType() == PdfObjectType.Integer;
        }

        /// <summary>
        /// Returns the object as an integer, or null if not an integer.
        /// </summary>
        public static PdfIntegerObject AsInteger(this PdfObject obj)
        {
            return obj.IsInteger() ? PdfIntegerObject.FromObject(obj.ResolveIndirection()) : null;
        }

        /// <summary>
        /// Checks if the object is a string (resolves indirect references).
        /// </summary>
        public static bool IsString(this PdfObject obj)
        {
            return obj.ResolveIndirection().GetObjectType() == PdfObjectType.String;
        }

        /// <summary>
        /// Returns the object as a string, or null if not a string.
        /// </summary>
        public static PdfStringObject AsString(this PdfObject obj)
        {
            return obj.IsString() ? PdfStringObject.FromObject(obj.ResolveIndirection()) : null;
        }

        /// <summary>
        /// Checks if the object is a boolean (resolves indirect references).
        /// </summary>
        public static bool IsBoolean(this PdfObject obj)
        {
            return obj.ResolveIndirection().GetObjectType() == PdfObjectType.Boolean;
        }

        /// <summary>
        /// Returns the object as a boolean, or null if not a boolean.
        /// </summary>
        public static PdfBooleanObject AsBoolean(this PdfObject obj)
        {
            return obj.IsBoolean() ? PdfBooleanObject.FromObject(obj.ResolveIndirection()) : null;
        }

        /// <summary>
        /// Checks if the object is a name (resolves indirect references).
        /// </summary>
        public static bool IsName(this PdfObject obj)
        {
            return obj.ResolveIndirection().GetObjectType() == PdfObjectType.Name;
        }

        /// <summary>
        /// Returns the object as a name, or null if not a name.
        /// </summary>
        public static PdfNameObject AsName(this PdfObject obj)
        {
            return obj.IsName() ? PdfNameObject.FromObject(obj.ResolveIndirection()) : null;
        }

        /// <summary>
        /// Checks if the object is null (resolves indirect references).
        /// </summary>
        public static bool IsNull(this PdfObject obj)
        {
            return obj.ResolveIndirection().GetObjectType() == PdfObjectType.Null;
        }

        /// <summary>
        /// Returns the object as a null object, or null if not a null object.
        /// </summary>
        public static PdfNullObject AsNull(this PdfObject obj)
        {
            return obj.IsNull() ? PdfNullObject.FromObject(obj.ResolveIndirection()) : null;
        }

        /// <summary>
        /// Checks if the object is a real number (resolves indirect references).
        /// </summary>
        public static bool IsReal(this PdfObject obj)
        {
            return obj.ResolveIndirection().GetObjectType() == PdfObjectType.Real;
        }

        /// <summary>
        /// Returns the object as a real number, or null if not a real number.
        /// </summary>
        public static PdfRealObject AsReal(this PdfObject obj)
        {
            return obj.IsReal() ? PdfRealObject.FromObject(obj.ResolveIndirection()) : null;
        }

        /// <summary>
        /// Checks if the object is a stream (resolves indirect references).
        /// </summary>
        public static bool IsStream(this PdfObject obj)
        {
            return obj.ResolveIndirection().GetObjectType() == PdfObjectType.Stream;
        }

        /// <summary>
        /// Returns the object as a stream, or null if not a stream.
        /// </summary>
        public static PdfStreamObject AsStream(this PdfObject obj)
        {
            return obj.IsStream() ? PdfStreamObject.FromObject(obj.ResolveIndirection()) : null;
        }

        /// <summary>
        /// Checks if the object is an indirect reference.
        /// </summary>
        public static bool IsIndirectReference(this PdfObject obj)
        {
            return obj.GetObjectType() == PdfObjectType.IndirectReference;
        }

        /// <summary>
        /// Returns the object as an indirect reference, or null if not an indirect reference.
        /// </summary>
        public static PdfIndirectReferenceObject AsIndirectReference(this PdfObject obj)
        {
            return obj.IsIndirectReference() ? PdfIndirectReferenceObject.FromObject(obj) : null;
        }
    }

    /// <summary>
    /// Extension methods for PdfStringObject providing explicit type upgrading and type checking.
    /// </summary>
    public static class PdfStringObjectExtensions
    {
        /// <summary>
        /// Upgrades to PdfLiteralStringObject or PdfHexadecimalStringObject.
        /// </summary>
        /// <param name="obj">The string object to upgrade.</param>
        /// <returns>The upgraded string object.</returns>
        public static PdfStringObject UpgradeString(this PdfStringObject obj)
        {
            switch (obj.GetStringType()) {
                case PdfStringType.Literal:
                    return PdfLiteralStringObject.FromString(obj);
                case PdfStringType.Hexadecimal:
                    return PdfHexadecimalStringObject.FromString(obj);
                default:
                    return obj;
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
        /// <param name="entry">The entry to upgrade.</param>
        /// <returns>The upgraded entry.</returns>
        public static PdfXrefEntry Upgrade(this PdfXrefEntry entry)
        {
            switch (entry.GetEntryType()) {
                case PdfXrefEntryType.Free:
                    return PdfXrefFreeEntry.FromEntry(entry);
                case PdfXrefEntryType.Used:
                    return PdfXrefUsedEntry.FromEntry(entry);
                case PdfXrefEntryType.Compressed:
                    return PdfXrefCompressedEntry.FromEntry(entry);
                default:
                    return entry;
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
