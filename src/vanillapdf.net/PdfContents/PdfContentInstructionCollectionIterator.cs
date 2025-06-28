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
    /// Base class for all content objects and operations.
    /// </summary>
    public class PdfContentInstructionCollectionIterator : PdfUnknown, IEnumerator<PdfContentInstruction>
    {
        internal PdfContentInstructionCollectionIteratorSafeHandle Handle { get; }

        internal PdfContentInstructionCollectionIterator(PdfContentInstructionCollectionIteratorSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfContentInstructionCollectionIterator()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfContentInstructionCollectionIteratorSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Get the current instruction pointed to by the iterator.
        /// </summary>
        /// <returns>The current <see cref="PdfContentInstruction"/>.</returns>
        public PdfContentInstruction GetValue()
        {
            UInt32 result = NativeMethods.ContentInstructionCollectionIterator_GetValue(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfContentInstruction(data);
        }

        /// <summary>
        /// Advance the iterator to the next instruction.
        /// </summary>
        public void Next()
        {
            UInt32 result = NativeMethods.ContentInstructionCollectionIterator_Next(Handle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Determine whether the iterator points to a valid instruction.
        /// </summary>
        /// <returns><c>true</c> if valid.</returns>
        public bool IsValid()
        {
            UInt32 result = NativeMethods.ContentInstructionCollectionIterator_IsValid(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        #region IEnumerator

        private bool isFirst = true;

        object IEnumerator.Current => GetValue();
        public PdfContentInstruction Current => GetValue();

        /// <inheritdoc />
        public bool MoveNext()
        {
            if (!IsValid()) {
                return false;
            }

            // HACK: Skip Next() for the first item
            if (isFirst) {
                isFirst = false;
                return true;
            }

            Next();
            return IsValid();
        }

        /// <inheritdoc />
        public void Reset()
        {
            throw new NotImplementedException();
        }

        #endregion

        private static class NativeMethods
        {
            public static GetValueDelgate ContentInstructionCollectionIterator_GetValue = LibraryInstance.GetFunction<GetValueDelgate>("ContentInstructionCollectionIterator_GetValue");
            public static NextDelgate ContentInstructionCollectionIterator_Next = LibraryInstance.GetFunction<NextDelgate>("ContentInstructionCollectionIterator_Next");
            public static IsValidDelgate ContentInstructionCollectionIterator_IsValid = LibraryInstance.GetFunction<IsValidDelgate>("ContentInstructionCollectionIterator_IsValid");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetValueDelgate(PdfContentInstructionCollectionIteratorSafeHandle handle, out PdfContentInstructionSafeHandle data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 NextDelgate(PdfContentInstructionCollectionIteratorSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 IsValidDelgate(PdfContentInstructionCollectionIteratorSafeHandle handle, out bool data);
        }
    }
}
