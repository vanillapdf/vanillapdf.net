﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Class for representing specific timepoint in the calendar
    /// </summary>
    public class PdfDate : PdfUnknown
    {
        internal PdfDateSafeHandle Handle { get; }

        internal PdfDate(PdfDateSafeHandle handle) : base(handle)
        {
            Handle = handle;
        }

        static PdfDate()
        {
            RuntimeHelpers.RunClassConstructor(typeof(NativeMethods).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PdfDateSafeHandle).TypeHandle);
        }

        /// <summary>
        /// Year (1970+)
        /// </summary>
        public Int32 Year
        {
            get { return GetYear(); }
            set { SetYear(value); }
        }

        /// <summary>
        /// Month (1-12)
        /// </summary>
        public Int32 Month
        {
            get { return GetMonth(); }
            set { SetMonth(value); }
        }

        /// <summary>
        /// Day (1-31)
        /// </summary>
        public Int32 Day
        {
            get { return GetDay(); }
            set { SetDay(value); }
        }

        /// <summary>
        /// Hour (0-23)
        /// </summary>
        public Int32 Hour
        {
            get { return GetHour(); }
            set { SetHour(value); }
        }

        /// <summary>
        /// Minute (0-59)
        /// </summary>
        public Int32 Minute
        {
            get { return GetMinute(); }
            set { SetMinute(value); }
        }

        /// <summary>
        /// Second (0-59)
        /// </summary>
        public Int32 Second
        {
            get { return GetSecond(); }
            set { SetSecond(value); }
        }

        /// <summary>
        /// Timezone
        /// </summary>
        public PdfTimezoneType Timezone
        {
            get { return GetTimezone(); }
            set { SetTimezone(value); }
        }

        /// <summary>
        /// Timezone offset hours (0-23)
        /// </summary>
        public Int32 HourOffset
        {
            get { return GetHourOffset(); }
            set { SetHourOffset(value); }
        }

        /// <summary>
        /// Timezone offset minutes (0-59)
        /// </summary>
        public Int32 MinuteOffset
        {
            get { return GetMinuteOffset(); }
            set { SetMinuteOffset(value); }
        }

        /// <summary>
        /// Creates a new blank instance of \ref PdfDate with default values
        /// </summary>
        /// <returns>A new \ref PdfDate instance on success, throws exception on failure</returns>
        public static PdfDate CreateEmpty()
        {
            UInt32 result = NativeMethods.Date_CreateEmpty(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDate(data);
        }

        /// <summary>
        /// Creates a new blank instance of \ref PdfDate with current date from local computer
        /// </summary>
        /// <returns>A new \ref PdfDate instance on success, throws exception on failure</returns>
        public static PdfDate CreateCurrent()
        {
            UInt32 result = NativeMethods.Date_CreateCurrent(out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return new PdfDate(data);
        }

        private Int32 GetYear()
        {
            UInt32 result = NativeMethods.Date_GetYear(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetYear(Int32 data)
        {
            UInt32 result = NativeMethods.Date_SetYear(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int32 GetMonth()
        {
            UInt32 result = NativeMethods.Date_GetMonth(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetMonth(Int32 data)
        {
            UInt32 result = NativeMethods.Date_SetMonth(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int32 GetDay()
        {
            UInt32 result = NativeMethods.Date_GetDay(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetDay(Int32 data)
        {
            UInt32 result = NativeMethods.Date_SetDay(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int32 GetHour()
        {
            UInt32 result = NativeMethods.Date_GetHour(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetHour(Int32 data)
        {
            UInt32 result = NativeMethods.Date_SetHour(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int32 GetMinute()
        {
            UInt32 result = NativeMethods.Date_GetMinute(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetMinute(Int32 data)
        {
            UInt32 result = NativeMethods.Date_SetMinute(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int32 GetSecond()
        {
            UInt32 result = NativeMethods.Date_GetSecond(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetSecond(Int32 data)
        {
            UInt32 result = NativeMethods.Date_SetSecond(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private PdfTimezoneType GetTimezone()
        {
            UInt32 result = NativeMethods.Date_GetTimezone(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return EnumUtil<PdfTimezoneType>.CheckedCast(data);
        }

        private void SetTimezone(PdfTimezoneType data)
        {
            UInt32 result = NativeMethods.Date_SetTimezone(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int32 GetHourOffset()
        {
            UInt32 result = NativeMethods.Date_GetHourOffset(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetHourOffset(Int32 data)
        {
            UInt32 result = NativeMethods.Date_SetHourOffset(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private Int32 GetMinuteOffset()
        {
            UInt32 result = NativeMethods.Date_GetMinuteOffset(Handle, out var data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }

            return data;
        }

        private void SetMinuteOffset(Int32 data)
        {
            UInt32 result = NativeMethods.Date_SetMinuteOffset(Handle, data);
            if (result != PdfReturnValues.ERROR_SUCCESS) {
                throw PdfErrors.GetLastErrorException();
            }
        }

        private protected override void DisposeCustomHandle()
        {
            base.DisposeCustomHandle();
            Handle?.Dispose();
        }

        private static class NativeMethods
        {
            public static CreateEmptyDelgate Date_CreateEmpty = LibraryInstance.GetFunction<CreateEmptyDelgate>("Date_CreateEmpty");
            public static CreateCurrentDelgate Date_CreateCurrent = LibraryInstance.GetFunction<CreateCurrentDelgate>("Date_CreateCurrent");

            public static GetYearDelgate Date_GetYear = LibraryInstance.GetFunction<GetYearDelgate>("Date_GetYear");
            public static SetYearDelgate Date_SetYear = LibraryInstance.GetFunction<SetYearDelgate>("Date_SetYear");

            public static GetMonthDelgate Date_GetMonth = LibraryInstance.GetFunction<GetMonthDelgate>("Date_GetMonth");
            public static SetMonthDelgate Date_SetMonth = LibraryInstance.GetFunction<SetMonthDelgate>("Date_SetMonth");

            public static GetDayDelgate Date_GetDay = LibraryInstance.GetFunction<GetDayDelgate>("Date_GetDay");
            public static SetDayDelgate Date_SetDay = LibraryInstance.GetFunction<SetDayDelgate>("Date_SetDay");

            public static GetHourDelgate Date_GetHour = LibraryInstance.GetFunction<GetHourDelgate>("Date_GetHour");
            public static SetHourDelgate Date_SetHour = LibraryInstance.GetFunction<SetHourDelgate>("Date_SetHour");

            public static GetMinuteDelgate Date_GetMinute = LibraryInstance.GetFunction<GetMinuteDelgate>("Date_GetMinute");
            public static SetMinuteDelgate Date_SetMinute = LibraryInstance.GetFunction<SetMinuteDelgate>("Date_SetMinute");

            public static GetSecondDelgate Date_GetSecond = LibraryInstance.GetFunction<GetSecondDelgate>("Date_GetSecond");
            public static SetSecondDelgate Date_SetSecond = LibraryInstance.GetFunction<SetSecondDelgate>("Date_SetSecond");

            public static GetTimezoneDelgate Date_GetTimezone = LibraryInstance.GetFunction<GetTimezoneDelgate>("Date_GetTimezone");
            public static SetTimezoneDelgate Date_SetTimezone = LibraryInstance.GetFunction<SetTimezoneDelgate>("Date_SetTimezone");

            public static GetHourOffsetDelgate Date_GetHourOffset = LibraryInstance.GetFunction<GetHourOffsetDelgate>("Date_GetHourOffset");
            public static SetHourOffsetDelgate Date_SetHourOffset = LibraryInstance.GetFunction<SetHourOffsetDelgate>("Date_SetHourOffset");

            public static GetMinuteOffsetDelgate Date_GetMinuteOffset = LibraryInstance.GetFunction<GetMinuteOffsetDelgate>("Date_GetMinuteOffset");
            public static SetMinuteOffsetDelgate Date_SetMinuteOffset = LibraryInstance.GetFunction<SetMinuteOffsetDelgate>("Date_SetMinuteOffset");

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateEmptyDelgate(out PdfDateSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 CreateCurrentDelgate(out PdfDateSafeHandle handle);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetYearDelgate(PdfDateSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetYearDelgate(PdfDateSafeHandle handle, Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetMonthDelgate(PdfDateSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetMonthDelgate(PdfDateSafeHandle handle, Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetDayDelgate(PdfDateSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetDayDelgate(PdfDateSafeHandle handle, Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetHourDelgate(PdfDateSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetHourDelgate(PdfDateSafeHandle handle, Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetMinuteDelgate(PdfDateSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetMinuteDelgate(PdfDateSafeHandle handle, Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetSecondDelgate(PdfDateSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetSecondDelgate(PdfDateSafeHandle handle, Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetTimezoneDelgate(PdfDateSafeHandle handle, out PdfTimezoneType data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetTimezoneDelgate(PdfDateSafeHandle handle, PdfTimezoneType data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetHourOffsetDelgate(PdfDateSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetHourOffsetDelgate(PdfDateSafeHandle handle, Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 GetMinuteOffsetDelgate(PdfDateSafeHandle handle, out Int32 data);

            [UnmanagedFunctionPointer(MiscUtils.LibraryCallingConvention)]
            public delegate UInt32 SetMinuteOffsetDelgate(PdfDateSafeHandle handle, Int32 data);
        }
    }
}
