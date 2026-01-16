using System;

namespace vanillapdf.net.Utils
{
    internal static class MiscUtils
    {
        /// <summary>
        /// Convert a 32-bit integer to the platform specific unsigned size.
        /// </summary>
        /// <param name="value">Value to convert.</param>
        /// <returns>Converted value as 32 or 64 bit depending on platform.</returns>
        public static UInt64 PlatformIntegerConversion(int value)
        {
            if (Environment.Is64BitProcess) {
                return (UInt64)value;
            } else {
                return (UInt32)value;
            }
        }
    }
}
