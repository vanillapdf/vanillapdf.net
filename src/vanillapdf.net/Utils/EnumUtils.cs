using System;

namespace vanillapdf.net.Utils
{
    internal static class EnumUtil<T>
        where T : struct, IConvertible
    {
        static EnumUtil()
        {
            if (!typeof(T).IsEnum) {
                throw new PdfManagedException(typeof(T).FullName + " is not an enum type.");
            }
        }

        // Always use checked cast for parsed enum values from the interface
        // If the value could not be interpreted by the enum type
        // it throws exception as it should
        /// <summary>
        /// Cast a value to the target enumeration ensuring it is defined.
        /// </summary>
        /// <param name="enumValue">Value to cast.</param>
        /// <returns>The casted enumeration value.</returns>
        /// <exception cref="InvalidCastException">Thrown when the value is not defined for the enumeration.</exception>
        public static T CheckedCast(object enumValue)
        {
            if (!Enum.IsDefined(typeof(T), enumValue))
                throw new InvalidCastException(enumValue + " is not a defined value for enum type " +
                                               typeof(T).FullName);

            return (T)enumValue;
        }

        /// <summary>
        /// Cast a flags value to the target enumeration without requiring it to be a named single value.
        /// Use this for <see cref="System.FlagsAttribute"/> enumerations where combined bit values are valid.
        /// </summary>
        /// <param name="enumValue">Integer value to cast.</param>
        /// <returns>The casted flags enumeration value.</returns>
        public static T FlagsCast(object enumValue)
        {
            return (T)Enum.ToObject(typeof(T), enumValue);
        }

        /// <summary>
        /// Determine whether the specified enumeration value is defined.
        /// </summary>
        /// <param name="enumValue">Enumeration value.</param>
        /// <returns><c>true</c> when the value is defined.</returns>
        public static bool IsDefined(T enumValue)
        {
            return Enum.IsDefined(typeof(T), enumValue);
        }
    }

    internal static class EnumExtensions
    {
        /// <summary>
        /// Extension method to check if a value is defined for its enumeration type.
        /// </summary>
        public static bool IsDefined<T>(this T enumValue)
            where T : struct, IConvertible
        {
            return EnumUtil<T>.IsDefined(enumValue);
        }
    }
}
