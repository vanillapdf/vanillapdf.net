using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A remote go-to action is similar to a go-to action
    /// but jumps to a destination in another PDF file.
    /// </summary>
    public class PdfGoToRemoteAction : PdfAction
    {
        internal new PdfGoToRemoteActionSafeHandle Handle { get; }

        internal PdfGoToRemoteAction(PdfGoToRemoteActionSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// A destination to be displayed when this action is performed.
        /// </summary>
        public PdfDestination Destination
        {
            get
            {
                UInt32 result = NativeMethods.GoToRemoteAction_GetDestination(Handle, out var data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfDestination(data);
            }
        }

        /// <summary>
        /// The file in which the destination shall be found.
        /// The returned object can be a string (file path) or a file specification dictionary.
        /// Use <see cref="PdfObject.GetObjectType"/> to determine the actual type.
        /// </summary>
        public PdfObject File
        {
            get
            {
                UInt32 result = NativeMethods.GoToRemoteAction_GetFile(Handle, out var data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfObject(data);
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
