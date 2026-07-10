using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfSyntax;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Controls the way the document shall be presented on the
    /// screen or in print. Retrieved via <see cref="PdfCatalog.GetViewerPreferences"/>.
    /// </summary>
    /// <remarks>
    /// Each entry is optional. A property returns <c>null</c> when the corresponding
    /// entry is absent from the viewer preferences dictionary.
    /// </remarks>
    public class PdfViewerPreferences : IDisposable
    {
        internal PdfViewerPreferencesSafeHandle Handle { get; }

        internal PdfViewerPreferences(PdfViewerPreferencesSafeHandle handle)
        {
            Handle = handle;
        }

        /// <summary>
        /// Whether to hide the conforming reader's tool bars when the document is active.
        /// </summary>
        public PdfBooleanObject HideToolbar => GetHideToolbar();

        /// <summary>
        /// Whether to hide the conforming reader's menu bar when the document is active.
        /// </summary>
        public PdfBooleanObject HideMenubar => GetHideMenubar();

        /// <summary>
        /// Whether to hide user interface elements in the document's window
        /// (such as scroll bars and navigation controls), leaving only the
        /// document's contents displayed.
        /// </summary>
        public PdfBooleanObject HideWindowUI => GetHideWindowUI();

        /// <summary>
        /// Whether to resize the document's window to fit the size of the first displayed page.
        /// </summary>
        public PdfBooleanObject FitWindow => GetFitWindow();

        /// <summary>
        /// Whether to position the document's window in the center of the screen.
        /// </summary>
        public PdfBooleanObject CenterWindow => GetCenterWindow();

        /// <summary>
        /// Whether the window's title bar should display the document title taken
        /// from the Title entry of the document information dictionary. If false, the
        /// title bar should instead display the name of the PDF file.
        /// </summary>
        public PdfBooleanObject DisplayDocTitle => GetDisplayDocTitle();

        /// <summary>
        /// The document's page mode, specifying how to display the document
        /// on exiting full-screen mode.
        /// </summary>
        public PdfNonFullScreenPageMode? NonFullScreenPageMode => GetNonFullScreenPageMode();

        /// <summary>
        /// The predominant reading order for text.
        /// </summary>
        public PdfReadingOrder? Direction => GetDirection();

        /// <summary>
        /// The name of the page boundary representing the area of a page that shall
        /// be displayed when viewing the document on the screen.
        /// </summary>
        public PdfNameObject ViewArea => GetViewArea();

        /// <summary>
        /// The name of the page boundary to which the contents of a page shall be
        /// clipped when viewing the document on the screen.
        /// </summary>
        public PdfNameObject ViewClip => GetViewClip();

        /// <summary>
        /// The name of the page boundary representing the area of a page that shall
        /// be rendered when printing the document.
        /// </summary>
        public PdfNameObject PrintArea => GetPrintArea();

        /// <summary>
        /// The name of the page boundary to which the contents of a page shall be
        /// clipped when printing the document.
        /// </summary>
        public PdfNameObject PrintClip => GetPrintClip();

        /// <summary>
        /// The page scaling option that shall be selected when a print dialog
        /// is displayed for this document.
        /// </summary>
        public PdfPrintScaling? PrintScaling => GetPrintScaling();

        /// <summary>
        /// The paper handling option that shall be used when printing the file
        /// from the print dialog.
        /// </summary>
        public PdfDuplex? Duplex => GetDuplex();

        /// <summary>
        /// Whether the PDF page size shall be used to select the input paper tray.
        /// </summary>
        public PdfBooleanObject PickTrayByPDFSize => GetPickTrayByPDFSize();

        /// <summary>
        /// The number of copies that shall be printed when the print dialog
        /// is opened for this file.
        /// </summary>
        public PdfIntegerObject NumCopies => GetNumCopies();

        #region Private Methods

        private PdfBooleanObject GetHideToolbar()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetHideToolbar(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfBooleanObject(data);
        }

        private PdfBooleanObject GetHideMenubar()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetHideMenubar(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfBooleanObject(data);
        }

        private PdfBooleanObject GetHideWindowUI()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetHideWindowUI(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfBooleanObject(data);
        }

        private PdfBooleanObject GetFitWindow()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetFitWindow(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfBooleanObject(data);
        }

        private PdfBooleanObject GetCenterWindow()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetCenterWindow(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfBooleanObject(data);
        }

        private PdfBooleanObject GetDisplayDocTitle()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetDisplayDocTitle(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfBooleanObject(data);
        }

        private PdfNonFullScreenPageMode? GetNonFullScreenPageMode()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetNonFullScreenPageMode(Handle, out int data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return EnumUtil<PdfNonFullScreenPageMode>.CheckedCast(data);
        }

        private PdfReadingOrder? GetDirection()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetDirection(Handle, out int data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return EnumUtil<PdfReadingOrder>.CheckedCast(data);
        }

        private PdfNameObject GetViewArea()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetViewArea(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfNameObject(data);
        }

        private PdfNameObject GetViewClip()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetViewClip(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfNameObject(data);
        }

        private PdfNameObject GetPrintArea()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetPrintArea(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfNameObject(data);
        }

        private PdfNameObject GetPrintClip()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetPrintClip(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfNameObject(data);
        }

        private PdfPrintScaling? GetPrintScaling()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetPrintScaling(Handle, out int data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return EnumUtil<PdfPrintScaling>.CheckedCast(data);
        }

        private PdfDuplex? GetDuplex()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetDuplex(Handle, out int data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return EnumUtil<PdfDuplex>.CheckedCast(data);
        }

        private PdfBooleanObject GetPickTrayByPDFSize()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetPickTrayByPDFSize(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfBooleanObject(data);
        }

        private PdfIntegerObject GetNumCopies()
        {
            UInt32 result = NativeMethods.ViewerPreferences_GetNumCopies(Handle, out var data);
            if (result == PdfReturnValues.ERROR_OBJECT_MISSING) {
                return null;
            }
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
            return new PdfIntegerObject(data);
        }

        #endregion

        /// <inheritdoc/>
        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
