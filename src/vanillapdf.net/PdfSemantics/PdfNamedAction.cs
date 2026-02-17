using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// A named action provides a way to execute a predefined action
    /// such as NextPage, PrevPage, FirstPage, or LastPage.
    /// </summary>
    public class PdfNamedAction : PdfAction
    {
        internal new PdfNamedActionSafeHandle Handle { get; }

        internal PdfNamedAction(PdfNamedActionSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// The name of the action that shall be performed.
        /// </summary>
        public PdfNameObject Name
        {
            get
            {
                UInt32 result = NativeMethods.NamedAction_GetName(Handle, out var data);
                if (result != PdfReturnValues.ERROR_SUCCESS) {
                    throw PdfErrors.GetLastErrorException();
                }

                return new PdfNameObject(data);
            }
        }

        /// <inheritdoc/>
        public override void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
