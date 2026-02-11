using System;
using System.Collections;
using System.Collections.Generic;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfContents.Extensions;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfContents
{
    /// <summary>
    /// A sequence of content instructions to be rendered on a page
    /// </summary>
    public class PdfContentInstructionCollection : IDisposable, IEnumerable<PdfContentInstruction>
    {
        internal PdfContentInstructionCollectionSafeHandle Handle { get; }

        internal PdfContentInstructionCollection(PdfContentInstructionCollectionSafeHandle handle)
        {
            Handle = handle;
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

            if (LibraryInstance.UpgradePolicy == UpgradePolicy.None) {
                return new PdfContentInstruction(data);
            }

            using (var instruction = new PdfContentInstruction(data)) {
                return instruction.Upgrade();
            }
        }

        /// <summary>
        /// Append a new instruction to the end of the collection.
        /// </summary>
        /// <param name="instruction">Instruction to append.</param>
        public void Append(PdfContentInstruction instruction)
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_Append(Handle, instruction.InstructionHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Insert a new instruction at the given index.
        /// </summary>
        /// <param name="index">Position where the instruction is inserted.</param>
        /// <param name="instruction">Instruction to insert.</param>
        public void Insert(UInt64 index, PdfContentInstruction instruction)
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_Insert(Handle, new UIntPtr(index), instruction.InstructionHandle);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Remove the instruction at the specified index.
        /// </summary>
        /// <param name="index">Index of the instruction to remove.</param>
        public void Remove(UInt64 index)
        {
            UInt32 result = NativeMethods.ContentInstructionCollection_Remove(Handle, new UIntPtr(index));
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        /// <summary>
        /// Remove all instructions from the collection.
        /// </summary>
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

        public void Dispose()
        {
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
    }
}
