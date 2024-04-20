using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// A sequence of content instructions to be rendered on a page
    /// </summary>
    public class PdfContentInstructionCollection : PdfUnknown, IEnumerable<PdfContentInstruction>
    {
        internal PdfContentInstructionCollectionSafeHandle Handle { get; }

        internal PdfContentInstructionCollection(PdfContentInstructionCollectionSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfContentInstructionCollection()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentInstructionCollectionSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get number of \ref PdfContents.PdfContentInstruction in the current collection
        /// </summary>
        /// <returns>Number of \ref PdfContents.PdfContentInstruction on success, throws exception on failure</returns>
        public UInt64 GetInstructionsSize()
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_GetSize(Handle, out UIntPtr data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data.ToUInt64();
        }

        /// <summary>
        /// Get \ref PdfContents.PdfContentInstruction at index in the current collection
        /// </summary>
        /// <param name="index">Index of \ref PdfContents.PdfContentInstruction to be returned</param>
        /// <returns>Handle to \ref PdfContents.PdfContentInstruction object at <p>index</p> on success, throws exception on failure</returns>
        public PdfContentInstruction At(UInt64 index)
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_At(Handle, new UIntPtr(index), out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentInstruction(data);
        }

        public void Append(PdfContentInstruction instruction)
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_Append(Handle, instruction.InstructionHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Insert(UInt64 index, PdfContentInstruction instruction)
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_Insert(Handle, new UIntPtr(index), instruction.InstructionHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Remove(UInt64 index)
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_Remove(Handle, new UIntPtr(index));
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        public void Clear()
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_Clear(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Get contant instruction iterator
        /// </summary>
        /// <returns>Handle to iterator for enumerating content instructions on success, throws exception on failure</returns>
        public PdfContentInstructionCollectionIterator GetIterator()
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_GetIterator(Handle, out var value);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentInstructionCollectionIterator(value);
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #region IEnumerable<PdfXref>

        /// <inheritdoc cref="IEnumerable.GetEnumerator" />
        public IEnumerator<PdfContentInstruction> GetEnumerator()
        {
            return GetIterator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetIterator();
        }

        #endregion

        private static class NativeMethods
        {
            public static GetSizeDelgate ContentInstructionCollection_GetSize = LibraryInstance.GetFunction<GetSizeDelgate>("ContentInstructionCollection_GetSize");
            public static AtDelgate ContentInstructionCollection_At = LibraryInstance.GetFunction<AtDelgate>("ContentInstructionCollection_At");
            public static AppendDelgate ContentInstructionCollection_Append = LibraryInstance.GetFunction<AppendDelgate>("ContentInstructionCollection_Append");
            public static InsertDelgate ContentInstructionCollection_Insert = LibraryInstance.GetFunction<InsertDelgate>("ContentInstructionCollection_Insert");
            public static RemoveDelgate ContentInstructionCollection_Remove = LibraryInstance.GetFunction<RemoveDelgate>("ContentInstructionCollection_Remove");
            public static ClearDelgate ContentInstructionCollection_Clear = LibraryInstance.GetFunction<ClearDelgate>("ContentInstructionCollection_Clear");
            public static GetIteratorDelgate ContentInstructionCollection_GetIterator = LibraryInstance.GetFunction<GetIteratorDelgate>("ContentInstructionCollection_GetIterator");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetSizeDelgate(PdfContentInstructionCollectionSafeHandle handle, out UIntPtr data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AtDelgate(PdfContentInstructionCollectionSafeHandle handle, UIntPtr at, out PdfContentInstructionSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 AppendDelgate(PdfContentInstructionCollectionSafeHandle handle, PdfContentInstructionSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 InsertDelgate(PdfContentInstructionCollectionSafeHandle handle, UIntPtr at, PdfContentInstructionSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 RemoveDelgate(PdfContentInstructionCollectionSafeHandle handle, UIntPtr at);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 ClearDelgate(PdfContentInstructionCollectionSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetIteratorDelgate(PdfContentInstructionCollectionSafeHandle handle, out PdfContentInstructionCollectionIteratorSafeHandle data);
        }
    }
}
