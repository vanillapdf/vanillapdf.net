using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// An action to be performed when a link annotation, outline item,
    /// or other trigger is activated.
    /// </summary>
    public class PdfAction : IDisposable
    {
        internal PdfActionSafeHandle Handle { get; }

        internal PdfAction(PdfActionSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Get the type of this action.
        /// </summary>
        public PdfActionType ActionType
        {
            get
            {
                UInt32 result = NativeMethods.Action_GetActionType(Handle, out Int32 data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return EnumUtil<PdfActionType>.CheckedCast(data);
            }
        }

        /// <summary>
        /// Create an action from a dictionary object.
        /// The dictionary must contain a valid action type (S entry).
        /// </summary>
        /// <param name="dictionary">Dictionary object containing action parameters.</param>
        /// <returns>A new action instance.</returns>
        public static PdfAction CreateFromDictionary(PdfDictionaryObject dictionary)
        {
            UInt32 result = NativeMethods.Action_CreateFromDictionary(dictionary.Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfAction(data);
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
