using System;
using vanillapdf.net.Interop;
using vanillapdf.net.PdfUtils;
using vanillapdf.net.Utils;

namespace vanillapdf.net.PdfSemantics
{
    /// <summary>
    /// Class for representing specific timepoint in the calendar
    /// </summary>
    public class PdfDate : IDisposable
    {
        internal PdfDateSafeHandle Handle { get; }

        internal PdfDate(PdfDateSafeHandle handle)
        {
            Handle = handle;
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
            UInt32 result = NativeMethods.Date_SetTimezone(Handle, (Int32)data);
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

        /// <inheritdoc/>

        public void Dispose()
        {
            Handle?.Dispose();
        }
    }
}
