using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A go-to action changes the view to a specified destination within the same document.
    /// </summary>
    public class PdfGoToAction : PdfAction
    {
        internal new PdfGoToActionSafeHandle Handle { get; }

        internal PdfGoToAction(PdfGoToActionSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Convert from base action type.
        /// </summary>
        public static PdfGoToAction FromAction(PdfAction action)
        {
            return new PdfGoToAction(action.Handle);
        }

        /// <summary>
        /// A destination to be displayed when this action is performed.
        /// </summary>
        public PdfDestination Destination
        {
            get
            {
                UInt32 result = NativeMethods.GoToAction_GetDestination(Handle, out var data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfDestination(data);
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
