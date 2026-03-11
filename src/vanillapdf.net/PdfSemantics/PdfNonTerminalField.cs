using System;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Represents a non-terminal form field (field group) in a PDF AcroForm.
    /// Non-terminal fields serve as parent containers for other fields and have no associated value.
    /// </summary>
    public class PdfNonTerminalField : PdfField
    {
        internal PdfNonTerminalField(PdfFieldSafeHandle handle) : base(handle)
        {
        }

        /// <summary>
        /// Convert a generic field to a non-terminal field.
        /// </summary>
        /// <param name="field">The field to convert. Must have type <see cref="PdfFieldType.NonTerminal"/>.</param>
        /// <returns>A <see cref="PdfNonTerminalField"/> wrapping the same field.</returns>
        public static PdfNonTerminalField FromField(PdfField field)
        {
            if (field == null) throw new ArgumentNullException(nameof(field));

            return new PdfNonTerminalField(field.Handle);
        }

        /// <inheritdoc/>
        /// <remarks>Non-terminal fields do not own a separate native handle; disposal is a no-op here.</remarks>
        public override void Dispose()
        {
            // base owns the PdfFieldSafeHandle — do not double-dispose
        }
    }
}
